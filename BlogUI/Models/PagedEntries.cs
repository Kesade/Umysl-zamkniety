using System.Collections.Generic;
using Common.DomainEntities;
using Common.UI;

namespace BlogUI.Models
{
    public class PagedEntries : IPaged<IEntryDomainEntity>
    {
        public string NextUrl { get; set; }
        public string PrevUrl { get; set; }
        public ICollection<IEntryDomainEntity> Content { get; set; }
    }
}