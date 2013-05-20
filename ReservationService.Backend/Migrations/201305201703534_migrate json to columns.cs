namespace ReservationService.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migratejsontocolumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("ReservationService.DayReservations", "RoomTypeId", c => c.Guid(nullable: false));
            AddColumn("ReservationService.DayReservations", "AvailableRooms", c => c.Int(nullable: false));
            DropPrimaryKey("ReservationService.DayReservations", new[] { "Day" });
            AddPrimaryKey("ReservationService.DayReservations", new[] { "Day", "RoomTypeId" });
            DropColumn("ReservationService.DayReservations", "ReservationData");
        }
        
        public override void Down()
        {
            AddColumn("ReservationService.DayReservations", "ReservationData", c => c.String());
            DropPrimaryKey("ReservationService.DayReservations", new[] { "Day", "RoomTypeId" });
            AddPrimaryKey("ReservationService.DayReservations", "Day");
            DropColumn("ReservationService.DayReservations", "AvailableRooms");
            DropColumn("ReservationService.DayReservations", "RoomTypeId");
        }
    }
}
