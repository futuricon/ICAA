using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICAA.Models.ViewModels
{
    public class BlogsListViewModel
    { 
        public IEnumerable<Blog> Blogs { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
