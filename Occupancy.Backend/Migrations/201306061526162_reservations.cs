namespace Occupancy.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "OccupancyService.Reservations",
                c => new
                    {
                        ReservationId = c.Guid(nullable: false),
                        Arrival = c.DateTime(nullable: false, storeType: "datetime2"),
                        Checkout = c.DateTime(nullable: false, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ReservationId);
            
        }
        
        public override void Down()
        {
            DropTable("OccupancyService.Reservations");
        }
    }
}
