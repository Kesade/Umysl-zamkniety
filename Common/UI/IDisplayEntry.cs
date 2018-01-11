using Common.DomainEntities;
namespace Common.UI
{
    public interface IDisplayEntry
    {
        IEntryDomainEntity Entry { get; set; }
        ICreateComment NewComment { get; set; }
    }
}