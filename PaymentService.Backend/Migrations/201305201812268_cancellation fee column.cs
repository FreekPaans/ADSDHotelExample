namespace PaymentService.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cancellationfeecolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("PaymentService.BillingData", "CancellationFeeHoldAcquired", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("PaymentService.BillingData", "CancellationFeeHoldAcquired");
        }
    }
}
