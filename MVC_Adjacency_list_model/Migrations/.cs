namespace MVC_nested_set_model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedRootCategory : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                SET IDENTITY_INSERT [dbo].[Categories] ON
                INSERT INTO [dbo].[Categories] ([ID], [Name], [lft], [rgt]) VALUES (1, N'Root', 1, 2)
                SET IDENTITY_INSERT [dbo].[Categories] OFF
            ");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Categories");
        }
    }
}
