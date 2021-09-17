using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UserManagement.Model;

namespace UserManagement.DataAccess
{
    public class Seeder
    {
        /// <summary>
        /// Populates the Role and User Tables with Initial data
        /// </summary>
        /// <param name="roleManager"></param>
        /// <param name="userManager"></param>
        /// <param name="context"></param>
        public static async void SeedDataBase(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, AppDbContext context)
        {
            try
            {
                await context.Database.EnsureCreatedAsync();
                if (!context.Users.Any())
                {
                    string roles = File.ReadAllText(@"JsonFiles/Roles.json");
                    List<IdentityRole> listOfRoles = JsonConvert.DeserializeObject<List<IdentityRole>>(roles);
                    //List<string> listOfRoles = new List<string> { "Admin", "Regular" };

                    foreach (var role in listOfRoles)
                    {
                        await roleManager.CreateAsync(role);
                    }

                    string users = File.ReadAllText(@"JsonFiles/Users.json");
                    List<User> listOfUsers = JsonConvert.DeserializeObject<List<User>>(users);
                    int i = 0;
                    foreach (var user in listOfUsers)
                    {
                        await userManager.CreateAsync(user, user.Password);

                        // Adds the first 5 users to the Admin role and the others to the regular role
                        if(i<5)
                        {
                            await userManager.AddToRoleAsync(user, "Admin");
                            i++;
                        }
                        else
                            await userManager.AddToRoleAsync(user, "Regular");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
