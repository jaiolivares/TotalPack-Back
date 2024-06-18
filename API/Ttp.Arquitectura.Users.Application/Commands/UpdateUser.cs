using Mapster;
using Ttp.Arquitectura.Users.Application.Models;
using Ttp.Arquitectura.Users.Domain;
using Ttp.Arquitectura.Users.Domain.Interfaces.Repository;

namespace Ttp.Arquitectura.Users.Application.Commands
{
    public class UpdateUserHandler
    {
        private readonly IGenericRepository<User> _userRepository;

        public UpdateUserHandler(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void Handle(UpdateUserCommand command)
        {
            var user = command.Adapt<User>();
            _userRepository.Update(user);
            _userRepository.Save();
        }
    }
}