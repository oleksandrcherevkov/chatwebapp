using ChatWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChatWebApp.Services.Home;

namespace ChatWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeService _service;
        public HomeController(HomeService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(int userId)
        {
            var blocks = await _service.GetBlocksAsync(userId);

            return View(blocks);
        }

        
        //public IActionResult Error()
        //{
        //    return View();
        //}
    }
}
