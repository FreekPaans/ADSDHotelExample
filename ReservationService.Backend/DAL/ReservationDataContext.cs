using ReservationService.Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationService.Backend.DAL {
	public class ReservationDataContext : DbContext{
		public ReservationDataContext() : base("name=ADSDData") { }
		public ReservationDataContext(string connectionString) : base(connectionString) { }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<DayReservations>().Property(d=>d.Version).IsRowVersion();
			modelBuilder.Entity<DayReservations>().ToTable("DayReservations", "ReservationService");
			modelBuilder.Entity<DayReservations>().HasKey(dr=>new  { dr.Day, dr.RoomTypeId });
			modelBuilder.Entity<Reservation>().ToTable("Reservation","ReservationService");
			modelBuilder.Entity<Reservation>().Property(p=>p.Status).IsRequired();

		}

		public DbSet<Reservation> Reservations{get;set;}
		public DbSet<DayReservations> DayReservations{get;set;}
	}
}
