using Common.DomainEntities;
using Common.UI;

namespace BlogUI.Models
{
    public class CreateComment : ICreateComment
    {
        public IUserDomainEntity Author { get; set; }
        public string Body { get; set; }
        public IEntryDomainEntity Entry { get; set; }
        public int ParrentId { get; set; }
    }
}