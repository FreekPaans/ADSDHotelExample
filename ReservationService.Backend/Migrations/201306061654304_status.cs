namespace ReservationService.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class status : DbMigration
    {
        public override void Up()
        {
			

			RenameTable("ReservationService.Reservation","ReservationStatus");
        }
        
        public override void Down()
        {
			RenameTable("ReservationService.ReservationStatus","Reservation");
			
        }
    }
}
