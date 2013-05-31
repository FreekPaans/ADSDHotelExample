namespace PaymentService.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeacquirefeefied : DbMigration
    {
        public override void Up()
        {
            DropColumn("PaymentService.BillingData", "CancellationFeeHoldAcquired");
        }
        
        public override void Down()
        {
            AddColumn("PaymentService.BillingData", "CancellationFeeHoldAcquired", c => c.Boolean(nullable: false));
        }
    }
}
