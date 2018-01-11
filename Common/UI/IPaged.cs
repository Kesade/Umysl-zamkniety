using System.Collections.Generic;
using System.Linq;
using Common.DomainEntities;

namespace Common.UI
{
    public interface IPaged<T> where T: IDomainEntity
    {
        string NextUrl { get; set; }
        string PrevUrl { get; set; }
        ICollection<IEntryDomainEntity> Content { get; set; }
    }
}