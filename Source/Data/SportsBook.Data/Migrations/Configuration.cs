namespace SportsBook.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using SportsBook.Common;
    
    public sealed class Configuration : DbMigrationsConfiguration<SportsBookDbContext>
    {
        private UserManager<AppUser> userManager;
        private IRandomGenerator random;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.random = new RandomGenerator();
        }

        protected override void Seed(SportsBookDbContext context)
        {
            // this.userManager = new UserManager<AppUser>(new UserStore<AppUser>(context));
            var userManager = new UserManager<AppUser>(new UserStore<AppUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Regular" });
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "Employer" });
            }

            var adminUser = userManager.Users.FirstOrDefault(x => x.Email == "admin@gmail.com");
            if (adminUser == null)
            {
                var admin = new AppUser
                {
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                    EmailConfirmed = true
                };

                userManager.Create(admin, "123456");
                userManager.AddToRole(admin.Id, "Admin");
            }

            if (userManager.Users.FirstOrDefault(x => x.Email == "user1@mail.com") == null)
            {
                var user = new AppUser
                {
                    Email = "user1@mail.com",
                    UserName = "user1@mail.com"
                };

                userManager.Create(user, "123456");
                userManager.AddToRole(user.Id, "Regular");
            }

            if (userManager.Users.FirstOrDefault(x => x.Email == "user2@mail.com") == null)
            {
                var user = new AppUser
                {
                    Email = "user2@mail.com",
                    UserName = "user2@mail.com"
                };

                userManager.Create(user, "123456");
                userManager.AddToRole(user.Id, "Regular");
            }

            if (userManager.Users.FirstOrDefault(x => x.Email == "user3@mail.com") == null)
            {
                var user = new AppUser
                {
                    Email = "user3@mail.com",
                    UserName = "user3@mail.com"
                };

                userManager.Create(user, "123456");
                userManager.AddToRoles(user.Id, "Regular", "Admin");
            }

            if (userManager.Users.FirstOrDefault(x => x.Email == "user4@mail.com") == null)
            {
                var user = new AppUser
                {
                    Email = "user4@mail.com",
                    UserName = "user4@mail.com"
                };

                userManager.Create(user, "123456");
                userManager.AddToRole(user.Id, "Regular");
            }

            adminUser = userManager.Users.FirstOrDefault(x => x.Email == "admin@gmail.com");

            if (adminUser != null)
            {
                userManager.AddToRoles(adminUser.Id, "Regular", "Admin", "Employer");
            }

            var allUsers = context.Users.AsQueryable();
            context.SaveChanges();

            if (context.Cities.Count() == 0)
            {
                City sofia = new City
                {
                    Name = "Sofia"
                };

                City plovdiv = new City
                {
                    Name = "Plovdiv"
                };

                context.Cities.Add(sofia);
                context.Cities.Add(plovdiv);

                context.SaveChanges();
                for (int i = 0; i < 10; i++)
                {
                    Facility facility = new Facility
                    {
                        Name = "Faciltiy" + i,
                        City = sofia,
                        Image = "https://upload.wikimedia.org/wikipedia/commons/4/4d/Cat_March_2010-1.jpg",
                        Description = "Description" + i
                    };

                    context.Facilities.Add(facility);
                }

                context.SaveChanges();
            }

            var facilities = context.Facilities.Take(10).ToList();

            SportCategory football = new SportCategory
            {
                Name = "Football",
                Description = "Description"
            };

            SportCategory volleyball = new SportCategory
            {
                Name = "Volleyball",
                Description = "Description"
            };


            SportCategory basketball = new SportCategory
            {
                Name = "basketball",
                Description = "Description"
            };

            football.Facilities.Add(facilities[0]);
            football.Facilities.Add(facilities[1]);
            football.Facilities.Add(facilities[2]);
            basketball.Facilities.Add(facilities[3]);
            basketball.Facilities.Add(facilities[4]);
            basketball.Facilities.Add(facilities[5]);
            volleyball.Facilities.Add(facilities[6]);
            volleyball.Facilities.Add(facilities[7]);
            volleyball.Facilities.Add(facilities[8]);
            football.Facilities.Add(facilities[9]);
            volleyball.Facilities.Add(facilities[9]);
            basketball.Facilities.Add(facilities[9]);

            context.SportCategories.Add(football);
            context.SportCategories.Add(volleyball);
            context.SportCategories.Add(basketball);

            context.SaveChanges();
        }
    }
}