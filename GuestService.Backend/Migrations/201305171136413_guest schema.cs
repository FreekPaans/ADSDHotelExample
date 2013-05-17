namespace GuestService.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guestschema : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.Guests", newSchema: "GuestService");
        }
        
        public override void Down()
        {
            MoveTable(name: "GuestService.Guests", newSchema: "dbo");
        }
    }
}
