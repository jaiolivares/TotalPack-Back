using Mapster;
using Ttp.Arquitectura.Users.Domain.Interfaces.Repository;
using Ttp.Arquitectura.Users.Domain;
using Ttp.Arquitectura.Users.Application.Models;

namespace Ttp.Arquitectura.Users.Application.Queries
{
    public class GetUsersHandler
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Adress> _adressRepository;

        public GetUsersHandler(IGenericRepository<User> userRepository, IGenericRepository<Adress> adressRepository)
        {
            _userRepository = userRepository;
            _adressRepository = adressRepository;
        }

        public List<GetUsersQuery> Handle()
        {
            var users = _userRepository.Get().ToList();
            var lstUsers = users.Adapt<List<GetUsersQuery>>();

            foreach (var user in lstUsers)
            {
                var adress = _adressRepository.GetByPrincipal(user.Id);
                if (adress != null)
                {
                    user.Street = adress.Street;
                }
            }

            return lstUsers;
        }

        public List<GetUsersQuery> HandlePage(int pageNumber, int pageSize)
        {
            var users = _userRepository.GetPage(pageNumber, pageSize).ToList();
            var lstUsers = users.Adapt<List<GetUsersQuery>>();

            foreach (var user in lstUsers)
            {
                var adress = _adressRepository.GetByPrincipal(user.Id);
                if (adress != null)
                {
                    user.Street = adress.Street;
                }
            }

            return lstUsers;
        }

        public GetUsersQuery HandleById(Guid id)
        {
            var user = _userRepository.GetByID(id);
            return user.Adapt<GetUsersQuery>();
        }
    }
}