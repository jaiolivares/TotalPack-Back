using Mapster;
using Ttp.Arquitectura.Users.Application.Models;
using Ttp.Arquitectura.Users.Domain;
using Ttp.Arquitectura.Users.Domain.Interfaces.Repository;

namespace Ttp.Arquitectura.Users.Application.Commands
{
    public class UpdateAdressHandler
    {
        private readonly IGenericRepository<Adress> _adressRepository;

        public UpdateAdressHandler(IGenericRepository<Adress> adressRepository)
        {
            _adressRepository = adressRepository;
        }

        public void Handle(UpdateAdressCommand command)
        {
            var adress = command.Adapt<Adress>();
            _adressRepository.Update(adress);
            _adressRepository.Save();
        }
    }
}