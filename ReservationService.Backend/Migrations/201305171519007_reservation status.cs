namespace ReservationService.Backend.Migrations
{
	using ReservationService.Backend.DAL.Models;
	using System;
	using System.Data.Entity.Migrations;
    
    public partial class reservationstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("ReservationService.Reservation", "Status", c => c.String(nullable: false, defaultValue:Reservation.Pending));
        }
        
        public override void Down()
        {
            DropColumn("ReservationService.Reservation", "Status");
        }
    }
}
