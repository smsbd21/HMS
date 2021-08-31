namespace HMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HReserve : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        RsvId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Dob = c.String(),
                        Sex = c.String(),
                        CellNo = c.String(),
                        Email = c.String(),
                        NoOfGuest = c.Int(nullable: false),
                        Street = c.String(),
                        AptSuite = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        RoomType = c.String(),
                        FloorNo = c.String(),
                        RoomNo = c.String(),
                        TotalBill = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentType = c.String(),
                        CardType = c.String(),
                        CardNo = c.String(),
                        CardExp = c.String(),
                        CardCvc = c.String(),
                        CheckIn = c.Boolean(nullable: false),
                        ArrivalTime = c.DateTime(nullable: false),
                        LeavingTime = c.DateTime(nullable: false),
                        BreakFast = c.Int(nullable: false),
                        Lunch = c.Int(nullable: false),
                        Dinner = c.Int(nullable: false),
                        Cleaning = c.Boolean(nullable: false),
                        Towel = c.Boolean(nullable: false),
                        SpSurprise = c.Boolean(nullable: false),
                        SupplyStatus = c.Boolean(nullable: false),
                        FoodBill = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RsvId);
            
            AddColumn("dbo.Bookings", "CreatedOn", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "CreatedOn");
            DropTable("dbo.Reservations");
        }
    }
}
