using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationService.Backend.Logic;
using System.Collections.Generic;
using System.Data.Entity;
using ReservationService.Backend.DAL;
using System.IO;
using System.Threading;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Transactions;

namespace ReservationService.Backend.Tests {
	[TestClass]
	public class RoomReserverTests {
		readonly static Guid RoomTypeId = Guid.NewGuid();

		[TestMethod]
		public void when_booking_a_fully_booked_room_an_exception_is_thrown() {
			var reserver = new RoomReserver(GetContext(), new Dictionary<Guid,int> { {RoomTypeId, 0 }});
			try {
				reserver.ReserveRoom(RoomTypeId, DateTime.Today,DateTime.Today.AddDays(1));
			}
			catch(RoomTypeNotAvailableException) {
				return;
			}
			Assert.Fail("Exception not thrown");
		}

		[TestMethod]
		public void when_a_room_is_booked_it_cant_be_reserved_again() {
			var reserver = new RoomReserver(GetContext(),new Dictionary<Guid,int> {{ RoomTypeId, 1}});
			reserver.ReserveRoom(RoomTypeId,DateTime.Today,DateTime.Today.AddDays(1));
			try {
				reserver.ReserveRoom(RoomTypeId,DateTime.Today,DateTime.Today.AddDays(1));
			}
			catch(RoomTypeNotAvailableException) {
				return;
			}
			Assert.Fail("Was still able to reserve the room");
		}

		void ReserveARoom(Dictionary<Guid,int> config) {
			var reserver = new RoomReserver(GetContext(),config);
			reserver.ReserveRoom(RoomTypeId,DateTime.Today,DateTime.Today.AddDays(1));
		}

		[TestMethod]
		public void LoopATaskInParallel(int loopCount, int threadCount, Action code) {
			var tasks = new List<Task>();

			for(var i=0;i<threadCount;i++) {
				var task = Task.Factory.StartNew(()=> {
					for(var j=0;j<loopCount;j++) {
						code();
					}
				});
				
				tasks.Add(task);
			}
			
			Task.WaitAll(tasks.ToArray());
		}

		[TestMethod]
		public void when_booking_rooms_concurrently_an_optimistic_locking_exception_is_thrown() {
			var config = new Dictionary<Guid,int> {{RoomTypeId,100}};
			
			var hadConcurrencyException = false;

			//insert one record so we don't get duplicate PK exceptions
			ReserveARoom(config);
			LoopATaskInParallel(10,5,() => {
				try {
					ReserveARoom(config);
				}
				catch(DbUpdateConcurrencyException) {
					hadConcurrencyException = true;
				}
			});

			Assert.IsTrue(hadConcurrencyException, "ConcurrencyException was not thrown");
		}

		[TestMethod]
		public void when_booking_rooms_exception_on_duplicate_pk_is_thrown() {
			var hadException = false;
			var config = new Dictionary<Guid,int>{{RoomTypeId,100}};
			LoopATaskInParallel(10,5,()=> {
				try {
					ReserveARoom(config);
				}
				catch(DbUpdateConcurrencyException) {}
				catch(DbUpdateException ex) {
					if(((SqlException) ex.InnerException.InnerException).Number!=2627) {
						throw;
					}
					hadException = true;
				}
			});
			Assert.IsTrue(hadException, "Duplicate PK Exception not thrown");
		}

		[TestMethod]
		public void when_reservering_rooms_with_serializable_isolation_level_the_correct_number_of_rooms_is_available() {
			
			reserve_rooms_with_isolation_level(IsolationLevel.Serializable);
		}

		[TestMethod]
		public void when_reservering_rooms_with_readcommitted_isolation_level_the_correct_number_of_rooms_available() {
			reserve_rooms_with_isolation_level(IsolationLevel.ReadCommitted);
		}

		private void reserve_rooms_with_isolation_level(IsolationLevel isolationLevel) {
			const int availableRooms= 100;
			const int iterations = 20;
			var config = new Dictionary<Guid,int> { { RoomTypeId,availableRooms } };
			LoopATaskInParallel(iterations,availableRooms/iterations,() => {
				var success=false;
				while(!success) {
					try {
						using(var scope = new TransactionScope(TransactionScopeOption.Required,new TransactionOptions { IsolationLevel = isolationLevel })) {
							ReserveARoom(config);
							scope.Complete();
						}
						success=true;
					}
					catch {
					}
				}
			});

			try {
				ReserveARoom(config);
			}
			catch(RoomTypeNotAvailableException) {
				return;
			}
			Assert.Fail("Was still able to book a room, probably means data is inconsistent");
		}

		[ClassInitialize]
		public static void Init(TestContext context) {	
			Configuration.Configure.UpdateDBSchema();
		}

		[TestInitialize]
		public void Init() {	
			var ctx = GetContext();
			
			ctx.Database.ExecuteSqlCommand("delete from ReservationService.Reservation");
			ctx.Database.ExecuteSqlCommand("delete from ReservationService.DayReservations");
		}

		private DAL.ReservationDataContext GetContext() {
			return new DAL.ReservationDataContext();
		}
	}
}
