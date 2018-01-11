namespace Common.DomainEntities
{
    public interface IUserDomainEntity : IDomainEntity
    {
        string Name { get; set; }
        string Roles { get; set; }
        string Login { get; set; }
        string Password { get; set; }
    }
}