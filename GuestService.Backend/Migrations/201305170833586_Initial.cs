namespace GuestService.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guests",
                c => new
                    {
                        GuestId = c.Guid(nullable: false),
                        ReservationId = c.Guid(nullable: false),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Email = c.String(),
                        Phonenumber = c.String(),
                    })
                .PrimaryKey(t => t.GuestId);
        }
        
        public override void Down()
        {
            DropTable("dbo.Guests");
        }
    }
}
