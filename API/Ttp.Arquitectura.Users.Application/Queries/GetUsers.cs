using Mapster;
using Ttp.Arquitectura.Users.Domain.Interfaces.Repository;
using Ttp.Arquitectura.Users.Domain;
using Ttp.Arquitectura.Users.Application.Models;

namespace Ttp.Arquitectura.Users.Application.Queries
{
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

        public List<GetUsersQuery> HandlePage(int pageNumber, int pageSize)
        {
            var users = _userRepository.GetPage(pageNumber, pageSize).ToList();
            return users.Adapt<List<GetUsersQuery>>();
        }

        public GetUsersQuery HandleById(Guid id)
        {
            var user = _userRepository.GetByID(id);
            return user.Adapt<GetUsersQuery>();
        }
    }
}