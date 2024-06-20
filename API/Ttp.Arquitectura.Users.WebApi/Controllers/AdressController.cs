using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Ttp.Arquitectura.Users.Application.Commands;
using Ttp.Arquitectura.Users.Application.Models;
using Ttp.Arquitectura.Users.Application.Queries;
using Ttp.Arquitectura.Users.WebApi.Models.Helpers;
using Ttp.Arquitectura.Users.WebApi.Models.Request;
using Ttp.Arquitectura.Users.WebApi.Models.Response;

namespace Ttp.Arquitectura.Users.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdressController(AddAdressHandler addAdressHandler, GetAdressesHandler getAdressesHandler, UpdateAdressHandler updateAdressHandler, DeleteAdressHandler deleteAdressHandler)
        : ControllerBase
    {
        private readonly AddAdressHandler _addAdressHandler = addAdressHandler;
        private readonly GetAdressesHandler _getAdressesHandler = getAdressesHandler;
        private readonly UpdateAdressHandler _updateAdressHandler = updateAdressHandler;
        private readonly DeleteAdressHandler _deleteAdressHandler = deleteAdressHandler;

        //TODO: VALIDACIONES en API= 204, 500, 400
        //TODO: VALIDACIONES en API= buscar dirección antes de editar
        //TOOD: VALIDACIONES en API= no ingresar direcciones repetidas
        //TODO: VALIDACIONES en API= Foreign key con usuarios

        [HttpPost]
        public IActionResult Post([FromBody] AddAdressRequest request)
        {
            var command = request.Adapt<AddAdressCommand>();
            _addAdressHandler.Handle(command);
            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var adresses = _getAdressesHandler.Handle();
            var response = adresses.Adapt<List<GetAdressResponse>>().OrderBy(x => x.Street).ToList();
            return Ok(response);
        }

        [HttpGet("ById")]
        public IActionResult GetById(Guid idUser)
        {
            var adresses = _getAdressesHandler.HandleById(idUser);
            var response = adresses.Adapt<List<GetAdressResponse>>().OrderBy(x => x.Street).ToList();
            return Ok(response);
        }

        [HttpGet("ByPrincipal")]
        public IActionResult GetByPrincipal(Guid idUser)
        {
            var adress = _getAdressesHandler.HandleByPrincipal(idUser);
            var response = adress.Adapt<GetAdressResponse>();
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put(UpdateAdressRequest request)
        {
            var command = request.Adapt<UpdateAdressCommand>();
            _updateAdressHandler.Handle(command);
            return Ok();
        }

        [HttpPut("Principal")]
        public IActionResult Put(int id)
        {
            _updateAdressHandler.HandlePrincipal(id);
            return Ok();
        }

        [HttpDelete("All")]
        public IActionResult DeleteAll(Guid idUser)
        {
            _deleteAdressHandler.Handle(idUser);
            return Ok();
        }
    }
}