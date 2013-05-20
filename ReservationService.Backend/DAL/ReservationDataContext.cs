﻿using ReservationService.Backend.DAL.Models;
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
			modelBuilder.Entity<DayReservations>().Property(dr=>dr.Day).HasColumnType("datetime2");
			

			modelBuilder.Entity<Reservation>().ToTable("Reservation","ReservationService");
			modelBuilder.Entity<Reservation>().Property(p=>p.Status).IsRequired();
			modelBuilder.Entity<Reservation>().Property(r=>r.From).HasColumnType("datetime2");
			modelBuilder.Entity<Reservation>().Property(r=>r.To).HasColumnType("datetime2");
			modelBuilder.Entity<Reservation>().Property(r=>r.ReservedAt).HasColumnType("datetime2");

		}

		public DbSet<Reservation> Reservations{get;set;}
		public DbSet<DayReservations> DayReservations{get;set;}
	}
}
