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
    public class AdressController(IOptions<AppSettings> appSettings, AddAdressHandler addAdressHandler, GetAdressesHandler getAdressesHandler, UpdateAdressHandler updateAdressHandler)
        : ControllerBase
    {
        private readonly AppSettings _appSettings = appSettings.Value;
        private readonly AddAdressHandler _addAdressHandler = addAdressHandler;
        private readonly GetAdressesHandler _getAdressesHandler = getAdressesHandler;
        private readonly UpdateAdressHandler _updateAdressHandler = updateAdressHandler;

        //TODO: VALIDACIONES= 204, 500, 400
        //TODO: VALIDACIONES= buscar dirección antes de editar
        //TOOD: VALIDACIONES= no ingresar direcciones repetidas
        //TODO: VALIDACIONES= Foreign key con usuarios

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
            var response = adresses.Adapt<List<GetAdressResponse>>();
            return Ok(response);
        }

        [HttpGet("ById")]
        public IActionResult GetById(int id)
        {
            var adress = _getAdressesHandler.HandleById(id);
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
    }
}