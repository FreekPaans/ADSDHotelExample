namespace PaymentService.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "PaymentService.BillingData",
                c => new
                    {
                        ReservationId = c.Guid(nullable: false),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        StreetName = c.String(),
                        PostalCode = c.String(),
                        City = c.String(),
                        CreditCardNumber = c.String(),
                        CreditCardExpiration = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationId);
        }
        
        public override void Down()
        {
            DropTable("PaymentService.BillingData");
        }
    }
}
