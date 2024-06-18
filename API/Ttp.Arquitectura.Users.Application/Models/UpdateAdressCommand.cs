namespace Ttp.Arquitectura.Users.Application.Models
{
    public class UpdateAdressCommand
    {
        public int IdAdress { get; set; }
        public Guid IdUser { get; set; }
        public string Street { get; set; }
        public bool Principal { get; set; }
    }
}