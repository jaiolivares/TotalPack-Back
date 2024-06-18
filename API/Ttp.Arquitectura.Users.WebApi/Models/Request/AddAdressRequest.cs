namespace Ttp.Arquitectura.Users.WebApi.Models.Request
{
    public class AddAdressRequest
    {
        public Guid IdUser { get; set; }
        public string Street { get; set; }
        public bool Principal { get; set; }
    }
}