namespace GuestService.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ef600 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "GuestService.Guests",
                c => new
                    {
                        ReservationId = c.Guid(nullable: false),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Email = c.String(),
                        Phonenumber = c.String(),
                    })
                .PrimaryKey(t => t.ReservationId);
            
        }
        
        public override void Down()
        {
            DropTable("GuestService.Guests");
        }
    }
}
