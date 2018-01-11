using Common.DomainEntities;

namespace DomainModel
{
    public class User : IUserDomainEntity
    {
        public int ParrentId => Id;
        public string Name { get; set; }
        public string Roles { get; set; }
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}