using Ttp.Arquitectura.Users.Domain;
using Ttp.Arquitectura.Users.Domain.Interfaces.Repository;

namespace Ttp.Arquitectura.Users.Application.Commands
{
    public class DeleteAdressHandler
    {
        private readonly IGenericRepository<Adress> _adressRepository;

        public DeleteAdressHandler(IGenericRepository<Adress> adressRepository)
        {
            _adressRepository = adressRepository;
        }

        public void Handle(Guid id)
        {
            _adressRepository.DeleteAll(id);
            _adressRepository.Save();
        }

        public void HandlePrincipal(int id)
        {
            _adressRepository.UpdatePrincipal(id);
            _adressRepository.Save();
        }
    }
}