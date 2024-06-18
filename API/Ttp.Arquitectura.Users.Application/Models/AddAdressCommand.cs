namespace Ttp.Arquitectura.Users.Application.Models
{
    public class AddAdressCommand
    {
        public Guid IdUser { get; set; }
        public string Street { get; set; }
        public bool Principal { get; set; }
    }
}