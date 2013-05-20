namespace ReservationService.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservedat : DbMigration
    {
        public override void Up()
        {
            AddColumn("ReservationService.Reservation", "ReservedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("ReservationService.Reservation", "ReservedAt");
        }
    }
}
