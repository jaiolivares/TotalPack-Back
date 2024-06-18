namespace Ttp.Arquitectura.Users.WebApi.Models.Response
{
    public class GetAdressResponse
    {
        public int IdAdress { get; set; }
        public Guid IdUser { get; set; }
        public string Street { get; set; }
        public bool Principal { get; set; }
    }
}