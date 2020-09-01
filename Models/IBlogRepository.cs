using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICAA.Models
{
    public interface IBlogRepository { 
        IQueryable<Blog> Blogs { get; }
        void SaveBlog(Blog blog);
        Blog DeleteBlog(int blogID);
    }
}
