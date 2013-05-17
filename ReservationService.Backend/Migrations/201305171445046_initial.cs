namespace ReservationService.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
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
                    })
                .PrimaryKey(t => t.ReservationId);
            
            CreateTable(
                "ReservationService.DayReservations",
                c => new
                    {
                        Day = c.DateTime(nullable: false),
                        ReservationData = c.String(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Day);
            
        }
        
        public override void Down()
        {
            DropTable("ReservationService.DayReservations");
            DropTable("ReservationService.Reservation");
        }
    }
}
