namespace TicketSystem.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Domain;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<TicketSystem.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TicketSystem.Data.ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(x=>x.UserName == "admin@admin.com" && x.Email=="admin@admin.com"))
            {
                var user = new ApplicationUser { UserName = "admin@admin.com", Email = "admin@admin.com" };
                userManager.Create(user, "Imam200$");
                context.Roles.AddOrUpdate(x => x.Name, new IdentityRole { Name = "admin" });
                context.Roles.AddOrUpdate(x => x.Name, new IdentityRole { Name = "user" });
                context.SaveChanges();
                userManager.AddToRole(user.Id, "admin");
            }
        }
    }
}
