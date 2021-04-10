using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SignalRMonitor.Models;

namespace SignalRMonitor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost("/cpu")]
        public async Task<IActionResult> UseCPU()
        {
            var timeout = DateTime.Now.AddSeconds(5);
            while (DateTime.Now.CompareTo(timeout) < 0)
            {
                var newGuid = Guid.NewGuid();
            }
            await Task.CompletedTask;
            return Ok();
        }
        [HttpPost("/ram")]
        public void UseRAM()
        {
            byte[] buff = new byte[1024 * 1024 * 1024];
            for (var i = 0; i < buff.Length; i++) buff[i] = (byte)(i);
            //Sleep 5 seconds to keep the memory space "active"
            Thread.Sleep(5000);
            //release memory
            buff = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
