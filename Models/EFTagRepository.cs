using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICAA.Models
{
    public class EFTagRepository : ITagRepository
    {
        private AppIdentityDbContext context;
        public EFTagRepository(AppIdentityDbContext ctx) { context = ctx; }
        public IQueryable<InfoTag> InfoTags => context.InfoTags;
        public void SaveTag(InfoTag Tag)
        {
            if (Tag.id == 0)
            {
                context.InfoTags.Add(Tag);
            }
            else
            {
                InfoTag dbEntry = context.InfoTags
                    .FirstOrDefault(p => p.id == Tag.id);
                if (dbEntry != null)
                {
                    dbEntry.TagName = Tag.TagName;
                }
            }
            context.SaveChanges();
        }
        public InfoTag DeleteTag(int tagId)
        {
            InfoTag dbEntry = context.InfoTags
                    .FirstOrDefault(p => p.id == tagId);
            if (dbEntry != null)
            {
                context.InfoTags.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
