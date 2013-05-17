using PaymentService.Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Backend.DAL {
	public class PaymentDataContext : DbContext{
		public PaymentDataContext() : base("name=ADSDData") {}

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<BillingData>().ToTable("BillingData", "PaymentService");
		}

		public DbSet<BillingData> BillingData{get;set;}
	}
}
