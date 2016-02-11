namespace SportsBook.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<SportsBookDbContext>
    {
        private UserManager<AppUser> userManager;
        // private IRandomGenerator random;
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
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
        }
    }
}