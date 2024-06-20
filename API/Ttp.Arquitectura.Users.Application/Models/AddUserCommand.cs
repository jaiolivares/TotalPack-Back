namespace Ttp.Arquitectura.Users.Application.Models
{
    public class AddUserCommand
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birth { get; set; }
        public string Email { get; set; }
    }
}