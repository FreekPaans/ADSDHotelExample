namespace GuestService.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletenonpkindexonreservationid : DbMigration
    {
        public override void Up()
        {
			DropIndex("dbo.Guests","IX_ReservationId");
        }
        
        public override void Down()
        {
        }
    }
}
