using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ICAA.Models
{
    public class IdentSeedData
    {

        private static readonly List<IdentityRole> Roles = new List<IdentityRole>()
        {
            new IdentityRole {Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.NewGuid().ToString()},
            new IdentityRole {Name = "SuperAdmin", NormalizedName = "SUPERADMIN", ConcurrencyStamp = Guid.NewGuid().ToString()}
        };

        public static void EnsurePopulatedIdentRole(IApplicationBuilder app)
        {
            AppIdentityDbContext dbContext = app.ApplicationServices
                .GetRequiredService<AppIdentityDbContext>();

            if (!dbContext.Roles.Any())
            {
                foreach (var role in Roles)
                {
                    dbContext.Roles.Add(role);
                    dbContext.SaveChanges();
                }
            }

            if (!dbContext.UserRoles.Any())
            {
                var userRole = new IdentityUserRole<string>
                {
                    UserId = dbContext.Users.Single(r => r.UserName == "SuperAdmin").Id,
                    RoleId = dbContext.Roles.Single(r => r.Name == "SuperAdmin").Id
                };
                dbContext.UserRoles.Add(userRole);
                dbContext.SaveChanges();
            }
        }
    }
}
