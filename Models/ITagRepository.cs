using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICAA.Models
{
    public interface ITagRepository
    {
        IQueryable<InfoTag> InfoTags { get; }
        void SaveTag(InfoTag Tag);
        InfoTag DeleteTag(int tagId);
    }
}
