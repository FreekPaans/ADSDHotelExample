namespace ReservationService.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2reservationentities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ReservationService.ReservationsWithAcquiredRoom",
                c => new
                    {
                        ReservationId = c.Guid(nullable: false),
                        RoomTypeId = c.Guid(nullable: false),
                        Arrival = c.DateTime(nullable: false),
                        Checkout = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationId);
            
            AddColumn("ReservationService.Reservation", "FlowStatus", c => c.String(nullable: false));
            AlterColumn("ReservationService.Reservation", "CancellationFeeStatus", c => c.String());
            DropColumn("ReservationService.Reservation", "Status");
        }
        
        public override void Down()
        {
            AddColumn("ReservationService.Reservation", "Status", c => c.String(nullable: false));
            AlterColumn("ReservationService.Reservation", "CancellationFeeStatus", c => c.String(nullable: false));
            DropColumn("ReservationService.Reservation", "FlowStatus");
            DropTable("ReservationService.ReservationsWithAcquiredRoom");
        }
    }
}
