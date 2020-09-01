using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace ICAA.Models
{
    public class AppIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) 
            : base(options) { }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Gallery> Images { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<InfoTag> InfoTags { get; set; }
    }
}