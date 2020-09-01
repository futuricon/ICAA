using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICAA.Models
{
    public interface IGalleryRepository
    {
        IQueryable<Gallery> Images { get; }
        void SaveImage(Gallery image);
        Gallery DeleteImage(int imageId);
    }
}

