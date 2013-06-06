namespace ReservationService.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class checkinstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("ReservationService.ReservationStatus", "IsCheckingIn", c => c.Boolean(nullable: false));
            AddColumn("ReservationService.ReservationStatus", "IsCheckedIn", c => c.Boolean(nullable: false));
            AddColumn("ReservationService.ReservationStatus", "CheckInFailed", c => c.Boolean(nullable: false));
            AddColumn("ReservationService.ReservationStatus", "FailedReason", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("ReservationService.ReservationStatus", "FailedReason");
            DropColumn("ReservationService.ReservationStatus", "CheckInFailed");
            DropColumn("ReservationService.ReservationStatus", "IsCheckedIn");
            DropColumn("ReservationService.ReservationStatus", "IsCheckingIn");
        }
    }
}
