namespace Common.StorageEntities
{
    public interface IUserEntity : IStorageEntity
    {
        string Name { get; set; }
        string Roles { get; set; }
        string Login { get; set; }
        string Password { get; set; }
    }
}