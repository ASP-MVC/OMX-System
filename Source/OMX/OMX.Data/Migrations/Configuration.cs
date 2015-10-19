namespace OMX.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using OMX.Common;
    using OMX.Common.RandomGenerator;
    using OMX.Models;

    public sealed class Configuration : DbMigrationsConfiguration<OMXDbContext>
    {
        private const string CategoryNameJsonPath = "~/Files/categories.json";
        private const string SubCategoryNameJsonPath = "~/Files/subcategories.json";
        private const string AdsJsonPath = "~/Files/ads.json";

        private const string AdminUserName = "admin";
        private const string AdminEmail = "admin@mysite.com";
        private const string AdminPassword = "123456";

        private readonly IRandomOMXDataGenerator omxDataGenerator;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.omxDataGenerator = new RandomOMXDataGenerator();
        }

        protected override void Seed(OMXDbContext context)
        {
            this.SeedRoles(context);
            this.SeedUsers(context);
            this.SeedCategories(context);
            this.SeedSubCategories(context);
            this.SeedAds(context);
        }

        private void SeedUsers(OMXDbContext context)
        {
            if (!context.Users.Any())
            {
                var store = new UserStore<User>(context);
                var manager = new UserManager<User>(store);
                var user = new User()
                {
                    UserName = AdminUserName,
                    Email = AdminEmail
                };

                manager.Create(user, AdminPassword);
                manager.AddToRole(user.Id, GlobalConstants.AdminRole);
                context.SaveChanges();
            }
        }

        private void SeedRoles(OMXDbContext context)
        {
            if (!context.Roles.Any())
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);

                var adminRole = new IdentityRole { Name = GlobalConstants.AdminRole };
                var userRole = new IdentityRole { Name = GlobalConstants.UserRole };

                manager.Create(adminRole);
                manager.Create(userRole);
                context.SaveChanges();
            }
        }

        private void SeedAds(OMXDbContext context)
        {
            if (!context.Ads.Any())
            {
                var adsTitles = this.omxDataGenerator.GenerateRandomData(AdsJsonPath);
                var index = 0;
                var randomSubCategoryId = 
                    context.SubCategories.OrderBy(x => Guid.NewGuid()).Select(c => c.Id).Take(1).ToList();
                var ownerId = context.Users.Where(u => u.UserName == AdminUserName).Select(u => u.Id).ToList();
                foreach (var title in adsTitles)
                {
                    if (index % 5 == 0)
                    {
                        // On every 5 iterations of the loop we are making a query to the db, this will boost the performance a little bit
                        randomSubCategoryId = context.Categories.OrderBy(x => Guid.NewGuid()).Select(c => c.Id).Take(1).ToList();
                    }
                    context.Ads.AddOrUpdate(new Ad
                    {
                        Title = title,
                        Content = title,
                        SubCategoryId = randomSubCategoryId[0],
                        OwnerId = ownerId[0],
                        Price = this.omxDataGenerator.GenerateRandomNumber(50, 1000)
                    });
                    index++;
                }
                context.SaveChanges();
            }
        }

        private void SeedSubCategories(OMXDbContext context)
        {
            if (!context.SubCategories.Any())
            {
                var subCategoryTitles = this.omxDataGenerator.GenerateRandomData(SubCategoryNameJsonPath);
                var index = 0;
                var randomCategoryId = context.Categories.OrderBy(x => Guid.NewGuid()).Select(c => c.Id).Take(1).ToList();
                foreach (var title in subCategoryTitles)
                {
                    if (index % 5 == 0)
                    {
                        // On every 5 iterations of the loop we are making a query to the db, this will boost the performance a little bit
                        randomCategoryId = context.Categories.OrderBy(x => Guid.NewGuid()).Select(c => c.Id).Take(1).ToList();
                    }
                    context.SubCategories.AddOrUpdate(new SubCategory { Title = title, CategoryId = randomCategoryId[0] });
                    index++;
                }
                context.SaveChanges();
            }
        }

        private void SeedCategories(OMXDbContext context)
        {
            if (!context.Categories.Any())
            {
                var categoryTitles = this.omxDataGenerator.GenerateRandomData(CategoryNameJsonPath);
                foreach (var title in categoryTitles)
                {
                    context.Categories.AddOrUpdate(new Category { Title = title });
                }
                context.SaveChanges();
            }
        }
    }
}