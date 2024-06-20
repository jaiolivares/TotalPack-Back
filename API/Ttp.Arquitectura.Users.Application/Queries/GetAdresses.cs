using Mapster;
using Ttp.Arquitectura.Users.Domain.Interfaces.Repository;
using Ttp.Arquitectura.Users.Domain;
using Ttp.Arquitectura.Users.Application.Models;

namespace Ttp.Arquitectura.Users.Application.Queries
{
    public class GetAdressesHandler
    {
        private readonly IGenericRepository<Adress> _adressRepository;

        public GetAdressesHandler(IGenericRepository<Adress> adressRepository)
        {
            _adressRepository = adressRepository;
        }

        public List<GetAdressesQuery> Handle()
        {
            var adresses = _adressRepository.Get().ToList();
            return adresses.Adapt<List<GetAdressesQuery>>();
        }

        public List<GetAdressesQuery> HandleById(Guid idUser)
        {
            var adresses = _adressRepository.GetByIdAdress(idUser);
            return adresses.Adapt<List<GetAdressesQuery>>();
        }

        public GetAdressesQuery HandleByPrincipal(Guid idUser)
        {
            var adress = _adressRepository.GetByPrincipal(idUser);
            return adress.Adapt<GetAdressesQuery>();
        }
    }
}