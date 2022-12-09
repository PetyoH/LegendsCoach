namespace LegendsCoach.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using LegendsCoach.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any(u => u.UserName == "admin"))
            {
                return;
            }

            var admin = new ApplicationUser()
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "administrator@admin.com",
                NormalizedEmail = "ADMINISTRATOR@ADMIN.COM",
                EmailConfirmed = true,
                PhoneNumber = "+359161294783",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
            };

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            admin.PasswordHash = passwordHasher.HashPassword(admin, "123456");

            var userStore = new UserStore<ApplicationUser>(dbContext);
            var result = await userStore.CreateAsync(admin);

            var adminFromDb = await dbContext.Users
                .Where(x => x.UserName == "admin")
                .FirstOrDefaultAsync();

            if (result.Succeeded)
            {
                var role = await dbContext.Roles
                    .Where(r => r.Name == "Administrator")
                    .FirstOrDefaultAsync();

                await dbContext.UserRoles.AddAsync(new IdentityUserRole<string>()
                {
                    UserId = adminFromDb.Id,
                    RoleId = role.Id,
                });

                await dbContext.SaveChangesAsync();
            }

            return;
        }
    }
}
