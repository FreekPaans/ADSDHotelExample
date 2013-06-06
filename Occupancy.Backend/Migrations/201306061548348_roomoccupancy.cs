namespace Occupancy.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roomoccupancy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "OccupancyService.RoomOccupancy",
                c => new
                    {
                        RoomNumber = c.String(nullable: false, maxLength: 128),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        ReservationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoomNumber, t.Date });
            
            AddColumn("OccupancyService.Reservations", "RoomsAcquired", c => c.Boolean(nullable: false));
            AddColumn("OccupancyService.Reservations", "Version", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("OccupancyService.Reservations", "Version");
            DropColumn("OccupancyService.Reservations", "RoomsAcquired");
            DropTable("OccupancyService.RoomOccupancy");
        }
    }
}
