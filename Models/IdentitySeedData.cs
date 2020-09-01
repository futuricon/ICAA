using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace ICAA.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "SuperAdmin";
        private const string adminPassword = "MyPassword2020$";
        
        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            UserManager<IdentityUser> userManager = app.ApplicationServices
                .GetRequiredService<UserManager<IdentityUser>>();
            IdentityUser user = await userManager.FindByIdAsync(adminUser);
            if (user == null)
            {
                user = new IdentityUser("SuperAdmin");
                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
