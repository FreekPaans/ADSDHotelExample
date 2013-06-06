namespace Occupancy.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameroomsacquired : DbMigration
    {
        public override void Up()
        {
			RenameColumn("OccupancyService.Reservations","RoomsAcquired", "RoomAcquired");
            AddColumn("OccupancyService.Reservations", "GuestArrived", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
			RenameColumn("OccupancyService.Reservations","RoomAcquired","RoomsAcquired");
            DropColumn("OccupancyService.Reservations", "GuestArrived");
            
        }
    }
}
