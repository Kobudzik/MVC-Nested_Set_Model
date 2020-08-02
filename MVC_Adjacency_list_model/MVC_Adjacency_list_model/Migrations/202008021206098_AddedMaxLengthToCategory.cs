namespace MVC_Adjacency_list_model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMaxLengthToCategory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
        }
    }
}
