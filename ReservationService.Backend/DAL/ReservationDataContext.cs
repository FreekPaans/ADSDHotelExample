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

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<DayReservations>().Property(d=>d.Version).IsRowVersion();
		
			modelBuilder.Entity<DayReservations>().ToTable("DayReservations", "ReservationService");
			modelBuilder.Entity<Reservation>().ToTable("Reservation","ReservationService");
		}

		public DbSet<Reservation> Reservations{get;set;}
		public DbSet<DayReservations> DayReservations{get;set;}
	}
}
