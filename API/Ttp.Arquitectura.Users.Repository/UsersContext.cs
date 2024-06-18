using Microsoft.EntityFrameworkCore;
using Ttp.Arquitectura.Users.Domain;

namespace Ttp.Arquitectura.Users.Repository
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Adress> Adresses { get; set; }
    }
}