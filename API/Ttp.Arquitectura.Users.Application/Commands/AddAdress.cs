using Mapster;
using Ttp.Arquitectura.Users.Application.Models;
using Ttp.Arquitectura.Users.Domain;
using Ttp.Arquitectura.Users.Domain.Interfaces.Repository;

namespace Ttp.Arquitectura.Users.Application.Commands
{
    public class AddAdressHandler
    {
        private readonly IGenericRepository<Adress> _adressRepository;

        public AddAdressHandler(IGenericRepository<Adress> adressRepository)
        {
            _adressRepository = adressRepository;
        }

        public void Handle(AddAdressCommand command)
        {
            var adress = command.Adapt<Adress>();
            _adressRepository.Insert(adress);
            _adressRepository.Save();
        }
    }
}