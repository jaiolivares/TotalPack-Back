using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Ttp.Arquitectura.Users.Application.Commands;
using Ttp.Arquitectura.Users.Application.Queries;
using Ttp.Arquitectura.Users.WebApi.Models.Helpers;
using Ttp.Arquitectura.Users.WebApi.Models.Request;
using Ttp.Arquitectura.Users.WebApi.Models.Response;

namespace Ttp.Arquitectura.Users.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IOptions<AppSettings> appSettings, AddUserHandler addUserHandler, GetUsersHandler getUsersHandler) : ControllerBase
    {
        private readonly AppSettings _appSettings = appSettings.Value;
        private readonly AddUserHandler _addUserHandler = addUserHandler;
        private readonly GetUsersHandler _getUsersHandler = getUsersHandler;

        [HttpGet]
        public IActionResult Get()
        {
            var users = _getUsersHandler.Handle();
            var response = users.Adapt<List<GetUserResponse>>();
            return Ok(response);
        }

        [HttpGet("Page")]
        public IActionResult GetPage(int pageNumber)
        {
            var users = _getUsersHandler.HandlePage(pageNumber, _appSettings.PageSize);
            var response = users.Adapt<List<GetUserResponse>>();
            return Ok(response);
        }

        [HttpGet("ById")]
        public IActionResult GetById([FromQuery] Guid id)
        {
            var user = _getUsersHandler.HandleById(id);
            var response = user.Adapt<GetUserResponse>();
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