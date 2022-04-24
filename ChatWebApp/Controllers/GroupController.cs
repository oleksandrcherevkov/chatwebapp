using Microsoft.AspNetCore.Mvc;
using ChatWebApp.Services.Group;
using System.Threading.Tasks;
using ChatWebApp.Models.Group;

namespace ChatWebApp.Controllers
{
    public class GroupController : Controller
    {
        private readonly GroupService _service;
        public GroupController(GroupService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(GetGroupOptions options)
        {
            var group = await _service.GetGroupPagedAsync(options);
            return View(group);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GroupMessage message)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.CreateMessgeAsync(message);
            }
            return RedirectToAction("Index", new {groupId = message.GroupId, userId = message.UserId, pageNum = 1});
        }

        public async Task<IActionResult> Show(int messageId, int userId)
        {
            var message = await _service.GetMessageAsync(messageId);

            if(message is null)
                return BadRequest();

            var messageCombined = new GroupMessageCombined()
            {
                Message = message,
                UserId = userId
            };

            return View(messageCombined);
        }

        public async Task<IActionResult> Edit(int messageId)
        {
            var message = await _service.GetMessageAsync(messageId);

            if (message is null)
                return BadRequest();

            return View(message);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(GroupMessage message)
        {
            var respoce = await _service.UpdateMessgeTextAsync(message);

            return RedirectToAction("Show", new { messageId = message.Id, userId = message.UserId });
        }
        public async Task<IActionResult> Delete(int groupId, int messageId, int userId)
        {
            var respoce = await _service.DeleteMessageAsync(messageId);

            return RedirectToAction("Index", new { groupId = groupId, userId = userId, pageNum = 1 });
        }
        public async Task<IActionResult> Erase(int groupId, int messageId, int userId)
        {
            var respoce = await _service.EraseMessageAsync(messageId);

            return RedirectToAction("Index", new { groupId = groupId, userId = userId, pageNum = 1 });
        }
    }
}
