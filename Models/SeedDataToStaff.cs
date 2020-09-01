using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace ICAA.Models
{
    public class SeedDataToStaff
    {
        public static void EnsurePopulatedStaff(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Staffs.Any())
            {
                context.Staffs.AddRange(
                    new Staff
                    {
                        Name = "Fakhriddin",
                        Surname = "Usmanov",
                        Description = "President & Founder of International Combat Aikido Association",
                        FullInfo = "full information about this staff and so"
                    },
                    new Staff
                    {
                        Name = "Mukhammad",
                        Surname = "Yuldashev",
                        Description = "disciple",
                        FullInfo = "full information about this staff and so"
                    },
                    new Staff
                    {
                        Name = "Nurislom",
                        Surname = "Yuldashev",
                        Description = "disciple",
                        FullInfo = "full information about this staff and so"
                    },
                    new Staff
                    {
                        Name = "Abdulmalik",
                        Surname = "Yuldashev",
                        Description = "disciple",
                        FullInfo = "full information about this staff and so"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}

