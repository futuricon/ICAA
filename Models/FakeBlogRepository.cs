using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICAA.Models
{
    public class FakeBlogRepository /*: IBlogRepository*/
    {
        public IQueryable<Blog> Blogs => new List<Blog> {
            new Blog { Title = "First Blog", Description = "1111lorem ipsum dos", CreatedDate = DateTime.Now},
            new Blog { Title = "Second Blog", Description = "2222lorem ipsum dos", CreatedDate = DateTime.Now},
            new Blog { Title = "Third Blog shoes", Description = "3333lorem ipsum dos", CreatedDate = DateTime.Now}
        }.AsQueryable<Blog>();
    }
}
