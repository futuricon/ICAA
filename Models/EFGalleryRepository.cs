using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICAA.Models
{
    public class EFGalleryRepository : IGalleryRepository
    {
        private AppIdentityDbContext context;
        public EFGalleryRepository(AppIdentityDbContext ctx) { context = ctx; }
        public IQueryable<Gallery> Images => context.Images.Include(x => x.InfoTags);
        public void SaveImage(Gallery image)
        {
            if (image.ImageID == 0)
            {
                context.Images.Add(image);
            }
            else
            {
                Gallery dbEntry = context.Images
                    .FirstOrDefault(p => p.ImageID == image.ImageID);
                if (dbEntry != null)
                {
                    dbEntry.ImagePath = image.ImagePath;
                    dbEntry.UploadDate = image.UploadDate;
                    dbEntry.InfoTags = image.InfoTags;
                }
            }
            context.SaveChanges();
        }
        public Gallery DeleteImage(int imageId)
        {
            Gallery dbEntry = context.Images
                    .FirstOrDefault(p => p.ImageID == imageId);
            if (dbEntry != null)
            {
                context.Images.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
