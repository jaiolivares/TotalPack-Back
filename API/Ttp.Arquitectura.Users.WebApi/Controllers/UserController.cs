using Mapster;
using Microsoft.AspNetCore.Mvc;
using Ttp.Arquitectura.Users.Application.Commands;
using Ttp.Arquitectura.Users.Application.Queries;
using Ttp.Arquitectura.Users.WebApi.Models.Request;
using Ttp.Arquitectura.Users.WebApi.Models.Response;

namespace Ttp.Arquitectura.Users.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(AddUserHandler addUserHandler, GetUsersHandler getUsersHandler) : ControllerBase
    {
        private readonly AddUserHandler _addUserHandler = addUserHandler;
        private readonly GetUsersHandler _getUsersHandler = getUsersHandler;

        [HttpGet]
        public IActionResult Get()
        {
            var users = _getUsersHandler.Handle();
            var response = users.Adapt<List<GetUserResponse>>();
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddUserRequest request)
        {
            var command = request.Adapt<AddUserCommand>();
            _addUserHandler.Handle(command);
            return Ok();
        }
    }
}