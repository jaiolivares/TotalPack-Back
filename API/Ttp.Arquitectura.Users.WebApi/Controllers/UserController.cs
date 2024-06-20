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
    public class UserController(IOptions<AppSettings> appSettings, AddUserHandler addUserHandler, GetUsersHandler getUsersHandler, DeleteUserHandler deleteUserHandler, UpdateUserHandler updateUserHandler) : ControllerBase
    {
        private readonly AppSettings _appSettings = appSettings.Value;
        private readonly AddUserHandler _addUserHandler = addUserHandler;
        private readonly GetUsersHandler _getUsersHandler = getUsersHandler;
        private readonly DeleteUserHandler _deleteUserHandler = deleteUserHandler;
        private readonly UpdateUserHandler _updateUserHandler = updateUserHandler;

        //TODO: VALIDACIONES en API= 204, 500, 400
        //TODO: VALIDACIONES en API= buscar usuario antes de eliminar o editar
        //TOOD: VALIDACIONES en API= no ingresar usuarios repetidos

        [HttpPost]
        public IActionResult Post([FromBody] AddUserRequest request)
        {
            var command = request.Adapt<AddUserCommand>();
            _addUserHandler.Handle(command);
            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = _getUsersHandler.Handle();
            var response = users.Adapt<List<GetUserResponse>>().OrderByDescending(x => x.Id).ToList();
            return Ok(response);
        }

        [HttpGet("Page")]
        public IActionResult GetPage(int pageNumber)
        {
            var users = _getUsersHandler.HandlePage(pageNumber, _appSettings.PageSize);
            var response = users.Adapt<List<GetUserResponse>>().OrderByDescending(x => x.Id).ToList();
            return Ok(response);
        }

        [HttpGet("ById")]
        public IActionResult GetById([FromQuery] Guid id)
        {
            var user = _getUsersHandler.HandleById(id);
            var response = user.Adapt<GetUserResponse>();
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put(UpdateUserRequest request)
        {
            var command = request.Adapt<UpdateUserCommand>();
            _updateUserHandler.Handle(command);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            _deleteUserHandler.Handle(id);
            return Ok();
        }
    }
}