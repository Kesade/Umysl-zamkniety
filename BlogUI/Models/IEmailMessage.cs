namespace BlogUI.Models
{
    public interface IEmailMessage
    {
        string Name { get; set; }
        string Email { get; set; }
        string Message { get; set; }
    }
}