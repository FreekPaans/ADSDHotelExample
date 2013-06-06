using Occupancy.Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Occupancy.Backend.DAL {
	public class DataContext : DbContext{
		public DbSet<Reservation> Reservations{get;set;}
		public DbSet<RoomOccupancy> RoomOccupancy { get; set; }

		public DataContext() : base("name=ADSDData") { }
		public DataContext(string connectionString) : base(connectionString) { }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Reservation>().HasKey(r=>r.ReservationId);
			modelBuilder.Entity<Reservation>().ToTable("Reservations","OccupancyService");
			modelBuilder.Entity<Reservation>().Property(r=>r.Arrival).HasColumnType("datetime2");
			modelBuilder.Entity<Reservation>().Property(r => r.Checkout).HasColumnType("datetime2");
			modelBuilder.Entity<Reservation>().Property(d => d.Version).IsRowVersion();

			modelBuilder.Entity<RoomOccupancy>().HasKey(r => new { r.RoomNumber,r.Date });
			modelBuilder.Entity<RoomOccupancy>().ToTable("RoomOccupancy","OccupancyService");
			modelBuilder.Entity<RoomOccupancy>().Property(r => r.Date).HasColumnType("date");
		}
	}
}
