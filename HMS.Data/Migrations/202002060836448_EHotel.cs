namespace HMS.Data.Migrations
{
    using System.Data.Entity.Migrations;    
    public partial class EHotel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccomodationPackages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccomodationTypeId = c.Int(nullable: false),
                        PackageName = c.String(),
                        NoOfRoom = c.Int(nullable: false),
                        FeePerNight = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccomodationTypes", t => t.AccomodationTypeId, cascadeDelete: true)
                .Index(t => t.AccomodationTypeId);
            
            CreateTable(
                "dbo.AccomodationTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccType = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PackagePictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PictureId = c.Int(nullable: false),
                        AccomodationPackageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pictures", t => t.PictureId, cascadeDelete: true)
                .ForeignKey("dbo.AccomodationPackages", t => t.AccomodationPackageId, cascadeDelete: true)
                .Index(t => t.PictureId)
                .Index(t => t.AccomodationPackageId);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PicUrl = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Accomodations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccomodationPackageId = c.Int(nullable: false),
                        AccName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccomodationPackages", t => t.AccomodationPackageId, cascadeDelete: true)
                .Index(t => t.AccomodationPackageId);
            
            CreateTable(
                "dbo.AccomPictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PictureId = c.Int(nullable: false),
                        AccomodationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pictures", t => t.PictureId, cascadeDelete: true)
                .ForeignKey("dbo.Accomodations", t => t.AccomodationId, cascadeDelete: true)
                .Index(t => t.PictureId)
                .Index(t => t.AccomodationId);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccomodationId = c.Int(nullable: false),
                        FromDate = c.DateTime(nullable: false),
                        Duration = c.Int(nullable: false),
                        GuestName = c.String(),
                        ContactNo = c.String(),
                        Email = c.String(),
                        NoOfAdults = c.Int(nullable: false),
                        NoOfChilds = c.Int(nullable: false),
                        CheckOut = c.Int(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accomodations", t => t.AccomodationId, cascadeDelete: true)
                .Index(t => t.AccomodationId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        Address = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Bookings", "AccomodationId", "dbo.Accomodations");
            DropForeignKey("dbo.AccomPictures", "AccomodationId", "dbo.Accomodations");
            DropForeignKey("dbo.AccomPictures", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.Accomodations", "AccomodationPackageId", "dbo.AccomodationPackages");
            DropForeignKey("dbo.PackagePictures", "AccomodationPackageId", "dbo.AccomodationPackages");
            DropForeignKey("dbo.PackagePictures", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.AccomodationPackages", "AccomodationTypeId", "dbo.AccomodationTypes");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Bookings", new[] { "AccomodationId" });
            DropIndex("dbo.AccomPictures", new[] { "AccomodationId" });
            DropIndex("dbo.AccomPictures", new[] { "PictureId" });
            DropIndex("dbo.Accomodations", new[] { "AccomodationPackageId" });
            DropIndex("dbo.PackagePictures", new[] { "AccomodationPackageId" });
            DropIndex("dbo.PackagePictures", new[] { "PictureId" });
            DropIndex("dbo.AccomodationPackages", new[] { "AccomodationTypeId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Bookings");
            DropTable("dbo.AccomPictures");
            DropTable("dbo.Accomodations");
            DropTable("dbo.Pictures");
            DropTable("dbo.PackagePictures");
            DropTable("dbo.AccomodationTypes");
            DropTable("dbo.AccomodationPackages");
        }
    }
}
