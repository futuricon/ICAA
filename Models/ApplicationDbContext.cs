using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace ICAA.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Gallery> Images { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<InfoTag> InfoTags { get; set; }
        
    }
    
}
