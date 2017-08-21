namespace CoffeeMachineApp.Domain.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Badges",
                c => new
                    {
                        BadgeID = c.Int(nullable: false, identity: true),
                        BadgeName = c.String(),
                        ProductID = c.Int(),
                        SugarQuantity = c.Int(),
                    })
                .PrimaryKey(t => t.BadgeID);
            
            CreateTable(
                "dbo.CoffeeMachines",
                c => new
                    {
                        MachineID = c.Int(nullable: false, identity: true),
                        SugarQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MachineID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        MachineID = c.Int(nullable: false),
                        Name = c.String(),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.CoffeeMachines", t => t.MachineID, cascadeDelete: true)
                .Index(t => t.MachineID);

            Sql("delete from Badges;");
            Sql("delete from Products;");
            Sql("delete from CoffeeMachines;");

            Sql("DBCC CHECKIDENT (CoffeeMachines, RESEED, 0)");
            Sql("DBCC CHECKIDENT (Products, RESEED, 0)");
            Sql("DBCC CHECKIDENT (Badges, RESEED, 0)");

            Sql("insert into CoffeeMachines values (20)");

            Sql("insert into Products values ((select top(1) MachineID from CoffeeMachines), 'Tea', 5)");
            Sql("insert into Products values ((select top(1) MachineID from CoffeeMachines), 'Coffee', 5)");
            Sql("insert into Products values ((select top(1) MachineID from CoffeeMachines), 'Chocolate', 5)");

            Sql("insert into Badges values ('Badge 1', (select ProductID from Products Where Name='Tea'), 2)");
            Sql("insert into Badges values ('Badge 2', null, null)");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "MachineID", "dbo.CoffeeMachines");
            DropIndex("dbo.Products", new[] { "MachineID" });
            DropTable("dbo.Products");
            DropTable("dbo.CoffeeMachines");
            DropTable("dbo.Badges");
        }
    }
}
