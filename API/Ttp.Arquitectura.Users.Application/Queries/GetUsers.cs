using Mapster;
using Ttp.Arquitectura.Users.Domain.Interfaces.Repository;
using Ttp.Arquitectura.Users.Domain;

namespace Ttp.Arquitectura.Users.Application.Queries
{
    public class GetUsersQuery
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birth { get; set; }
        public string Email { get; set; }
    }

    public class GetUsersHandler
    {
        private readonly IGenericRepository<User> _userRepository;

        public GetUsersHandler(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public List<GetUsersQuery> Handle()
        {
            var users = _userRepository.Get().ToList();
            return users.Adapt<List<GetUsersQuery>>();
        }
    }
}