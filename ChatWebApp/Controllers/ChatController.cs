using Microsoft.AspNetCore.Mvc;
using ChatWebApp.Services.Chat;
using ChatWebApp.Models.Chat;
using System.Threading.Tasks;

namespace ChatWebApp.Controllers
{
    public class ChatController : Controller
    {
        private readonly ChatService _service;
        public ChatController(ChatService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(GetChatOptions options)
        {
            if(!ModelState.IsValid)
                return NotFound();

            try
            {
                var chat = await _service.GetChatPagedAsync(options);
                return View(chat);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ChatMessage message)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var responce = await _service.CreateMessageAsync(message);

            if (responce == 0)
                return BadRequest();

            return RedirectToAction("Index", new { responcerId = message.ReceiverId, userId = message.SenderId, pageNum = 1 });
        }

        public async Task<IActionResult> Show(int messageId, int userId)
        {
            try
            {
                var message = await _service.GetMessageAsync(messageId);

                var messageCombined = new ChatMessageCombined()
                {
                    Message = message,
                    UserId = userId
                };

                return View(messageCombined);
            }
            catch
            {
                return NotFound();
            }
            
        }
        public async Task<IActionResult> Edit(int messageId)
        {
            try
            {
                var message = await _service.GetMessageAsync(messageId);

                return View(message);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ChatMessage message)
        {
            var responce = await _service.UpdateMessgeTextAsync(message);

            if (responce == 0)
                return BadRequest();

            return RedirectToAction("Show", new { messageId = message.Id, userId = message.SenderId });
        }
        public async Task<IActionResult> Delete(int messageId, int responcerId, int userId)
        {
            var responce = await _service.DeleteMessageAsync(messageId);

            if (responce == 0)
                return BadRequest();

            return RedirectToAction("Index", new { responcerId = responcerId, userId = userId, pageNum = 1 });
        }
        public async Task<IActionResult> Erase(int messageId, int responcerId, int userId)
        {
            var responce = await _service.EraseMessageAsync(messageId);

            if (responce == 0)
                return BadRequest();

            return RedirectToAction("Index", new { responcerId = responcerId, userId = userId, pageNum = 1 });
        }
    }
}
