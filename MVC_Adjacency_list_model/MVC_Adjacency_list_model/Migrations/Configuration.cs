namespace MVC_Adjacency_list_model.Migrations
{
    using MVC_Adjacency_list_model.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVC_Adjacency_list_model.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MVC_Adjacency_list_model.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.


            //DUMMY DATA- manually creating Dummy categories and sub categories  
            context.Categories.AddOrUpdate(c => c.Name,
             new Category { Name = "ELECTRONICS", lft = 1, rgt = 20 },
             new Category { Name = "TELEVISIONS", lft = 2, rgt = 9 },
             new Category { Name = "TUBE", lft = 3, rgt = 4 },
             new Category { Name = "LCD", lft = 5, rgt = 6 },
             new Category { Name = "PLASMA", lft = 7, rgt = 8 },
             new Category { Name = "PORTABLE ELECTRONICS", lft = 10, rgt = 19 },
             new Category { Name = "MP3 PLAYERS", lft = 11, rgt = 14 },
             new Category { Name = "FLASH", lft = 12, rgt = 13 },
             new Category { Name = "CD PLAYERS", lft = 15, rgt = 16 },
             new Category { Name = "2 WAY RADIOS", lft = 17, rgt = 18 }

             );

        }
    }
}
