namespace ReservationService.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ef600 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "ReservationService.Reservation",
                c => new
                    {
                        ReservationId = c.Guid(nullable: false),
                        RoomTypeId = c.Guid(nullable: false),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                        Status = c.String(nullable: false),
                        ReservedAt = c.DateTime(nullable: false),
                        CancellationFeeStatus = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationId);
            
            CreateTable(
                "ReservationService.DayReservations",
                c => new
                    {
                        Day = c.DateTime(nullable: false),
                        RoomTypeId = c.Guid(nullable: false),
                        AvailableRooms = c.Int(nullable: false),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => new { t.Day, t.RoomTypeId });
            
        }
        
        public override void Down()
        {
            DropTable("ReservationService.DayReservations");
            DropTable("ReservationService.Reservation");
        }
    }
}
