using GuestService.Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestService.Backend.DAL {
	public class GuestDataContext : DbContext{
		public GuestDataContext() : base("name=ADSDData") { }
		public DbSet<Guest> Guests{get;set;}
		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Guest>().ToTable("Guests", "GuestService");
		}
	}
}
