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
            if (!ModelState.IsValid)
                return NotFound();

            try
            {
                var group = await _service.GetGroupPagedAsync(options);
                return View(group);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(GroupMessage message)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var responce = await _service.CreateMessgeAsync(message);

            if (responce == 0)
                return BadRequest();

            return RedirectToAction("Index", new {groupId = message.GroupId, userId = message.UserId, pageNum = 1});
        }

        public async Task<IActionResult> Show(int messageId, int userId)
        {
            try
            {
                var message = await _service.GetMessageAsync(messageId);

                var messageCombined = new GroupMessageCombined()
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
        public async Task<IActionResult> Edit(GroupMessage message)
        {
            var responce = await _service.UpdateMessgeTextAsync(message);

            if (responce == 0)
                return BadRequest();

            return RedirectToAction("Show", new { messageId = message.Id, userId = message.UserId });
        }
        public async Task<IActionResult> Delete(int groupId, int messageId, int userId)
        {
            var responce = await _service.DeleteMessageAsync(messageId);

            if (responce == 0)
                return BadRequest();

            return RedirectToAction("Index", new { groupId = groupId, userId = userId, pageNum = 1 });
        }
        public async Task<IActionResult> Erase(int groupId, int messageId, int userId)
        {
            var responce = await _service.EraseMessageAsync(messageId);

            if (responce == 0)
                return BadRequest();

            return RedirectToAction("Index", new { groupId = groupId, userId = userId, pageNum = 1 });
        }
    }
}
