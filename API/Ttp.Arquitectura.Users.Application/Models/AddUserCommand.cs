namespace Ttp.Arquitectura.Users.Application.Models
{
    public class AddUserCommand
    {
        public string FullName { get; set; }
        public DateTime Birth { get; set; }
        public string Email { get; set; }
    }
}