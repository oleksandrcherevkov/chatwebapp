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
        public async Task<IActionResult> Index(int responcerId, int userId, int page)
        {
            var options = new GetChatOptions()
            {
                UserId = userId,
                ResponcerId = responcerId,
                PageNum = page,
                PageSize = 20
            };

            var chat = await _service.GetChatPagedAsync(options);

            return View(chat);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ChatMessage message)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.CreateMessageAsync(message);
            }
            return RedirectToAction("Index", new { responcerId = message.ReceiverId, userId = message.SenderId, page = 1 });
        }

        public async Task<IActionResult> Show(int messageId, int userId)
        {
            var message = await _service.GetMessageAsync(messageId);

            if (message is null)
                return BadRequest();

            var messageCombined = new ChatMessageCombined()
            {
                Message = message,
                UserId = userId
            };

            return View(messageCombined);
        }
        public async Task<IActionResult> Edit(int messageId)
        {
            var message = await _service.GetMessageAsync(messageId);

            return View(message);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ChatMessage message)
        {
            var respoce = await _service.UpdateMessgeTextAsync(message);

            return RedirectToAction("Show", new { messageId = message.Id, userId = message.SenderId });
        }
        public async Task<IActionResult> Delete(int messageId, int responcerId, int userId)
        {
            var respoce = await _service.DeleteMessageAsync(messageId);

            return RedirectToAction("Index", new { responcerId = responcerId, userId = userId, page = 1 });
        }
        public async Task<IActionResult> Erase(int messageId, int responcerId, int userId)
        {
            var respoce = await _service.EraseMessageAsync(messageId);

            return RedirectToAction("Index", new { responcerId = responcerId, userId = userId, page = 1 });
        }
    }
}
