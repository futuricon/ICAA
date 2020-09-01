using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICAA.Models
{
    public class EFBlogRepository : IBlogRepository
    {
        private AppIdentityDbContext context; 
        public EFBlogRepository(AppIdentityDbContext ctx) { context = ctx; }
        public IQueryable<Blog> Blogs => context.Blogs;
        public void SaveBlog(Blog blog)
        {
            if (blog.BlogID == 0)
            {
                context.Blogs.Add(blog);
            }
            else
            {
                Blog dbEntry = context.Blogs
                    .FirstOrDefault(p => p.BlogID == blog.BlogID);
                if (dbEntry != null)
                {
                    dbEntry.Title = blog.Title;
                    dbEntry.Description = blog.Description;
                    dbEntry.TitleRu = blog.TitleRu;
                    dbEntry.DescriptionRu = blog.DescriptionRu;
                    dbEntry.TitleUz = blog.TitleUz;
                    dbEntry.DescriptionUz = blog.DescriptionUz;
                    dbEntry.CreatedDate = blog.CreatedDate;
                    if (blog.ImageUrl != null)
                    {
                        dbEntry.ImageUrl = blog.ImageUrl;
                    }
                }
            }
            context.SaveChanges();
        }
        public Blog DeleteBlog(int blogId)
        {
            Blog dbEntry = context.Blogs
                    .FirstOrDefault(p => p.BlogID == blogId);
            if (dbEntry != null)
            {
                context.Blogs.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
