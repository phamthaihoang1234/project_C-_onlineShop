namespace Ictshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddThanhtien : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        Madon = c.Int(nullable: false),
                        Masp = c.Int(nullable: false),
                        Soluong = c.Int(),
                        Dongia = c.Decimal(precision: 18, scale: 0),
                        Thanhtien = c.Double(),
                    })
                .PrimaryKey(t => new { t.Madon, t.Masp })
                .ForeignKey("dbo.Order", t => t.Madon)
                .ForeignKey("dbo.Product", t => t.Masp)
                .Index(t => t.Madon)
                .Index(t => t.Masp);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Madon = c.Int(nullable: false, identity: true),
                        Ngaydat = c.DateTime(),
                        Tinhtrang = c.Int(),
                        MaUser = c.Int(),
                    })
                .PrimaryKey(t => t.Madon)
                .ForeignKey("dbo.User", t => t.MaUser)
                .Index(t => t.MaUser);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        MaUser = c.Int(nullable: false, identity: true),
                        Hoten = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        Dienthoai = c.String(maxLength: 10, fixedLength: true),
                        Matkhau = c.String(maxLength: 50, unicode: false),
                        IDQuyen = c.Int(),
                        Diachi = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.MaUser)
                .ForeignKey("dbo.Role", t => t.IDQuyen)
                .Index(t => t.IDQuyen);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        IDQuyen = c.Int(nullable: false, identity: true),
                        TenQuyen = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.IDQuyen);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Masp = c.Int(nullable: false, identity: true),
                        Tensp = c.String(maxLength: 50),
                        Giatien = c.Decimal(precision: 18, scale: 0),
                        Soluong = c.Int(),
                        Mota = c.String(storeType: "ntext"),
                        Thesim = c.Int(),
                        Bonhotrong = c.Int(),
                        NewProduct = c.Boolean(),
                        Ram = c.Int(),
                        Anhbia = c.String(maxLength: 10),
                        Mahang = c.Int(),
                        Mahdh = c.Int(),
                    })
                .PrimaryKey(t => t.Masp)
                .ForeignKey("dbo.Brand", t => t.Mahang)
                .ForeignKey("dbo.Category", t => t.Mahdh)
                .Index(t => t.Mahang)
                .Index(t => t.Mahdh);
            
            CreateTable(
                "dbo.Brand",
                c => new
                    {
                        Mahang = c.Int(nullable: false, identity: true),
                        Tenhang = c.String(maxLength: 10, fixedLength: true),
                    })
                .PrimaryKey(t => t.Mahang);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Mahdh = c.Int(nullable: false, identity: true),
                        Tenhdh = c.String(maxLength: 10, fixedLength: true),
                    })
                .PrimaryKey(t => t.Mahdh);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "Mahdh", "dbo.Category");
            DropForeignKey("dbo.Product", "Mahang", "dbo.Brand");
            DropForeignKey("dbo.OrderDetail", "Masp", "dbo.Product");
            DropForeignKey("dbo.User", "IDQuyen", "dbo.Role");
            DropForeignKey("dbo.Order", "MaUser", "dbo.User");
            DropForeignKey("dbo.OrderDetail", "Madon", "dbo.Order");
            DropIndex("dbo.Product", new[] { "Mahdh" });
            DropIndex("dbo.Product", new[] { "Mahang" });
            DropIndex("dbo.User", new[] { "IDQuyen" });
            DropIndex("dbo.Order", new[] { "MaUser" });
            DropIndex("dbo.OrderDetail", new[] { "Masp" });
            DropIndex("dbo.OrderDetail", new[] { "Madon" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Category");
            DropTable("dbo.Brand");
            DropTable("dbo.Product");
            DropTable("dbo.Role");
            DropTable("dbo.User");
            DropTable("dbo.Order");
            DropTable("dbo.OrderDetail");
        }
    }
}
