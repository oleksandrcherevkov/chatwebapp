using ChatApi.HelperExtensions;
using ChatApi.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace ChatApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly UserService _service;
        public UsersController(UserService service)
        {
            _service = service;
        }
        [HttpGet("list/{userId}")]
        public async Task<IActionResult> ListChats([FromRoute] int userId)
        {
            var chats = await _service.ListChats(userId).ToListAsync();

            return Ok(chats);
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetProfile([FromRoute] int userId)
        {
            var profile = await _service.GetChatAsync(userId);

            if (!_service.HasErrors)                                        //#A
                return Ok(profile);

            ModelState.AddServiceErrors(_service);                         //#B

            return ValidationProblem(ModelState);
        }
    }
    /*=====================================================
    #A: Ensure, that service hasn't found any errors or validation problems
    #B: Add errors, that have occered in service to ModelState to return information with responce
     ====================================================*/
}
