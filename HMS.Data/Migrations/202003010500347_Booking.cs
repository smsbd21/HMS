namespace HMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Booking : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bookings", "CheckOut", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Bookings", "CreatedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bookings", "CreatedOn", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Bookings", "CheckOut", c => c.Int(nullable: false));
        }
    }
}
