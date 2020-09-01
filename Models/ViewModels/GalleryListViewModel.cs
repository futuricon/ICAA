using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICAA.Models.ViewModels
{
    public class GalleryListViewModel
    {
        public IEnumerable<Gallery> Galleries { get; set; }
        public IEnumerable<InfoTag> InfoTags { get; set; }
        public Gallery Gallery { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
