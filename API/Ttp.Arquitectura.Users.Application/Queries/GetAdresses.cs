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

        public GetAdressesQuery HandleById(int id)
        {
            var adress = _adressRepository.GetByID(id);
            return adress.Adapt<GetAdressesQuery>();
        }
    }
}