using HealthifyMeFinalProject.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using HealthifyMeFinalProject.Models;

namespace HealthifyMeFinalProject.Data
{
    // Singleton
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedIdentityRolesAsync(RoleManager<IdentityRole> rolemanager)
        {
            foreach (MyIdentityRoleNames rolename in Enum.GetValues(typeof(MyIdentityRoleNames)))
            {
                if (!await rolemanager.RoleExistsAsync(rolename.ToString()))
                {
                    await rolemanager.CreateAsync(
                        new IdentityRole { Name = rolename.ToString() });
                }
            }
        }

        public static async Task SeedApplicationUserAsync(UserManager<ApplicationUser> usermanager)
        {
            ApplicationUser user;

            user = await usermanager.FindByNameAsync("admin@demo.com");
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = "admin@demo.com",
                    Email = "admin@demo.com",
                    EmailConfirmed = true
                };
                await usermanager.CreateAsync(user, password: "Password@123");

                await usermanager.AddToRoleAsync(user, MyIdentityRoleNames.Admin.ToString());
            }

            user = await usermanager.FindByNameAsync("ShwetaMeher123@gmail.com");
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = "ShwetaMeher123@gmail.com",
                    Email = "ShwetaMeher123@gmail.com",
                    EmailConfirmed = true
                };
                await usermanager.CreateAsync(user, password: "ShwetaMeher123@gmail.com");

                await usermanager.AddToRoleAsync(user, MyIdentityRoleNames.Dietitian.ToString());
            }

            user = await usermanager.FindByNameAsync("user@demo.com");
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = "user@demo.com",
                    Email = "user@demo.com",
                    EmailConfirmed = true
                };
                await usermanager.CreateAsync(user, password: "Asdf@123");
                //await usermanager.AddToRolesAsync(user, new string[] {
                //    MyIdentityRoleNames.AppUser.ToString()
                //});
                await usermanager.AddToRoleAsync(user, MyIdentityRoleNames.Member.ToString());
            }
        }

    }
}
