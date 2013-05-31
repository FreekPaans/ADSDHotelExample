namespace ReservationService.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cancellationfeestatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("ReservationService.Reservation", "CancellationFeeStatus", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("ReservationService.Reservation", "CancellationFeeStatus");
        }
    }
}
