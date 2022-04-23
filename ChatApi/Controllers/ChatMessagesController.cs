using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChatApi.EF;
using ChatApi.EF.Models;
using ChatApi.Services.PrivateMessages;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ChatApi.HelperExtensions;

namespace ChatApi.Controllers
{
    [Route("api/chat/messages")]
    [ApiController]
    public class ChatMessagesController : ControllerBase
    {
        private readonly PrivateMessageService _service;
        public ChatMessagesController(PrivateMessageService service)
        {
            _service = service;
        }

        [HttpGet("")]
        public async Task<IActionResult> ListMessages([FromBody] ListPrivateMessageOptions options)
        {
            var messages =  _service.ListMessages(options);
                                         
            if(!_service.HasErrors)                                                                  //#A
                return Ok(await messages.ToListAsync());

            ModelState.AddServiceErrors(_service);                                                  //#B

            return ValidationProblem(ModelState);
        }

        [HttpGet("{messageId}")]
        public async Task<IActionResult> GetMessage([FromRoute] int messageId)
        {
            var message = await _service.ReadMessageAsync(messageId);

            if (!_service.HasErrors)                                                                  //#A
                return Ok(message);

            ModelState.AddServiceErrors(_service);                                                   //#B

            return ValidationProblem(ModelState);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateMessage([FromBody] PrivateMessageDto newMessage)
        {
            await _service.AddMessageAsync(newMessage);

            return Ok();
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateMessage([FromBody] PrivateMessageDto newMessage)
        {
            await _service.UpdateMessageTextAsync(newMessage.Id, newMessage.Text);

            if (!_service.HasErrors)                                                                   //#A
                return Ok();

            ModelState.AddServiceErrors(_service);                                                    //#B

            return ValidationProblem(ModelState);
        }

        [HttpPut("{messageId}")]
        public async Task<IActionResult> SelfDeleteMessage([FromRoute] int messageId)
        {
            await _service.SelfDeleteMessageAsync(messageId);

            if (!_service.HasErrors)                                                                   //#A
                return Ok();

            ModelState.AddServiceErrors(_service);                                                    //#B

            return ValidationProblem(ModelState);
        }

        [HttpDelete("{messageId}")]
        public async Task<IActionResult> DeleteMessage([FromRoute] int messageId)
        {
            await _service.DeleteMessageAsync(messageId);

            if (!_service.HasErrors)                                                                   //#A
                return Ok();

            ModelState.AddServiceErrors(_service);                                                    //#B

            return ValidationProblem(ModelState);
        }

    }

    /*=====================================================
    #A: Ensure, that service hasn't found any errors or validation problems
    #B: Add errors, that have occered in service to ModelState to return information with responce
     ====================================================*/
}
