using Common.DomainEntities;
using Common.UI;

namespace BlogUI.Models
{
    public class DisplayEntry : IDisplayEntry
    {
        public IEntryDomainEntity Entry { get; set; }
        public ICreateComment NewComment { get; set; }
    }
}