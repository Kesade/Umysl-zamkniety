namespace Common.DomainEntities
{
    public interface IDomainEntity
    {
        int ParrentId { get; }
        int Id { get; set; }
    }
}