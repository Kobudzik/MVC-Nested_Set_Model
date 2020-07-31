namespace MVC_Adjacency_list_model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCategoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        lft = c.Int(nullable: false),
                        rgt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Categories");
        }
    }
}
