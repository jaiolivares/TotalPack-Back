using System.ComponentModel.DataAnnotations;

namespace Ttp.Arquitectura.Users.Domain
{
    public class Adress
    {
        [Key]
        public int IdAdress { get; set; }

        public Guid IdUser { get; set; }
        public string Street { get; set; }
        public bool Principal { get; set; }
    }
}