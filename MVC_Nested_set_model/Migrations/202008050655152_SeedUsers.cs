namespace MVC_nested_set_model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"

INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'51ed54fd-2ab0-42fa-beda-f0bd6d0525e8', N'guest@net.com', 0, N'AGQihHRvN3NV6PnpQG4cmIZy3jGXG3y1BSdYIXJqJy5AKwFDYX9Jb++cC0l0lZZQxA==', N'79a46d6e-0707-41dc-b084-a115223d50f9', NULL, 0, 0, NULL, 1, 0, N'guest@net.com')
INSERT INTO[dbo].[AspNetUsers]([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES(N'f9c3a311-f6e5-450a-9d78-542fadada722', N'admin@net.com', 0, N'AABNbhURDI/jk9y9J8eSOB2v6mDEQlIkPYhOTVDfTJPogynTDXSUYQb+57whLREWqQ==', N'8c713b7b-7df8-48d6-b6be-68a356d3d8b0', NULL, 0, 0, NULL, 1, 0, N'admin@net.com')
INSERT INTO[dbo].[AspNetRoles] ([Id], [Name]) VALUES(N'd67911d5-1295-4609-8ade-ec969ca8bf80', N'Administrator')
INSERT INTO[dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES(N'f9c3a311-f6e5-450a-9d78-542fadada722', N'd67911d5-1295-4609-8ade-ec969ca8bf80')
");
        }

        public override void Down()
        {
        }
    }
}
