namespace GuestService.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteguestid : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Guests", new[] { "GuestId" });
            AddPrimaryKey("dbo.Guests", "ReservationId");
            DropColumn("dbo.Guests", "GuestId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Guests", "GuestId", c => c.Guid(nullable: false));
            DropPrimaryKey("dbo.Guests", new[] { "ReservationId" });
            AddPrimaryKey("dbo.Guests", "GuestId");
        }
    }
}
