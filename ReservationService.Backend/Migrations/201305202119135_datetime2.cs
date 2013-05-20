namespace ReservationService.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("ReservationService.Reservation", "From", c => c.DateTime(nullable: false, storeType: "datetime2"));
            AlterColumn("ReservationService.Reservation", "To", c => c.DateTime(nullable: false, storeType: "datetime2"));
            AlterColumn("ReservationService.Reservation", "ReservedAt", c => c.DateTime(nullable: false, storeType: "datetime2"));
            AlterColumn("ReservationService.DayReservations", "Day", c => c.DateTime(nullable: false, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("ReservationService.DayReservations", "Day", c => c.DateTime(nullable: false));
            AlterColumn("ReservationService.Reservation", "ReservedAt", c => c.DateTime(nullable: false));
            AlterColumn("ReservationService.Reservation", "To", c => c.DateTime(nullable: false));
            AlterColumn("ReservationService.Reservation", "From", c => c.DateTime(nullable: false));
        }
    }
}
