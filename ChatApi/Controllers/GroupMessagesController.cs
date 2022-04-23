using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChatApi.Services.GroupMessages;
using System.Linq;
using System.Threading.Tasks;
using ChatApi.EF.Models;
using Microsoft.EntityFrameworkCore;
using ChatApi.HelperExtensions;

namespace ChatApi.Controllers
{
    [Route("api/group/messages")]
    [ApiController]
    public class GroupMessagesController : ControllerBase
    {
        private readonly GroupMessageService _service;
        public GroupMessagesController(GroupMessageService service)
        {
            _service = service;
        }

        [HttpGet("")]
        public async Task<IActionResult> ListMessages(ListGroupMessageOptions options)
        {
            var messages = _service.ListMessages(options);

            if (!_service.HasErrors)                                                    //#A
                return Ok(await messages.ToListAsync());

            ModelState.AddServiceErrors(_service);                                     //#B

            return ValidationProblem(ModelState);
        }

        [HttpGet("{messageId}")]
        public async Task<IActionResult> GetMessage([FromRoute] int messageId)
        {
            var message = await _service.ReadMessageAsync(messageId);

            if (!_service.HasErrors)                                                    //#A
                return Ok(message);

            ModelState.AddServiceErrors(_service);                                     //#B

            return ValidationProblem(ModelState);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateMessage([FromBody] GroupMessageDto newMessage)
        {
            await _service.AddMessageAsync(newMessage);

            return Ok();
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateMessage([FromBody] GroupMessageDto newMessage)
        {
            await _service.UpdateMessageTextAsync(newMessage.Id, newMessage.Text);

            if (!_service.HasErrors)                                                    //#A
                return Ok();

            ModelState.AddServiceErrors(_service);                                     //#B

            return ValidationProblem(ModelState);
        }

        [HttpPut("{messageId}")]
        public async Task<IActionResult> SelfDeleteMessage([FromRoute] int messageId)
        {
            await _service.SelfDeleteMessageAsync(messageId);

            if (!_service.HasErrors)                                                    //#A
                return Ok();

            ModelState.AddServiceErrors(_service);                                     //#B

            return ValidationProblem(ModelState);
        }

        [HttpDelete("{messageId}")]
        public async Task<IActionResult> SoftDeleteMessage([FromRoute] int messageId)
        {
            await _service.SoftDeleteMessageAsync(messageId);

            if (!_service.HasErrors)                                                    //#A
                return Ok();

            ModelState.AddServiceErrors(_service);                                     //#B

            return ValidationProblem(ModelState);
        }
    }

    /*=====================================================
    #A: Ensure, that service hasn't found any errors or validation problems
    #B: Add errors, that have occered in service to ModelState to return information with responce
     ====================================================*/
}
