using Ttp.Arquitectura.Users.Domain;
using Ttp.Arquitectura.Users.Domain.Interfaces.Repository;

namespace Ttp.Arquitectura.Users.Application.Commands
{
    public class DeleteUserHandler
    {
        private readonly IGenericRepository<User> _userRepository;

        public DeleteUserHandler(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void Handle(Guid id)
        {
            _userRepository.Delete(id);
            _userRepository.Save();
        }
    }
}