using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChatApi.Services.Groups;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using ChatApi.HelperExtensions;

namespace ChatApi.Controllers
{
    [Route("api/group")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly GroupService _service;
        public GroupsController(GroupService service)
        {
            _service = service;
        }
        [HttpGet("list/{userId}")]
        public async Task<IActionResult> ListGroups([FromRoute] int userId)
        {
            var groups = await _service.ListGroups(userId).ToListAsync();

            return Ok(groups);
        }
        [HttpGet("{groupId}")]
        public async Task<IActionResult> GetGroup([FromRoute] int groupId)
        {
            var group = await _service.GetGroupAsync(groupId);

            if(!_service.HasErrors)                                         //#A
                return Ok(group);

            ModelState.AddServiceErrors(_service);                         //#B

            return ValidationProblem(ModelState);
        }
    }
    /*=====================================================
    #A: Ensure, that service hasn't found any errors or validation problems
    #B: Add errors, that have occered in service to ModelState to return information with responce
     ====================================================*/
}
