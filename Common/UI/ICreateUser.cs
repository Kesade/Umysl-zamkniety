namespace Common.UI
{
    public interface ICreateUser
    {
        string Login { get; set; }
        string Name { get; set; }
        string Password { get; set; }
        string Roles { get; set; }
        string ConfirmPassword { get; set; }
    }
}