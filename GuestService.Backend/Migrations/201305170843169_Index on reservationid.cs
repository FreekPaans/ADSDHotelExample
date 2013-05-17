namespace GuestService.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Indexonreservationid : DbMigration
    {
        public override void Up()
        {
			CreateIndex("dbo.Guests","ReservationId");	
        }
        
        public override void Down()
        {
        }
    }
}
