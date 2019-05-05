using CF.Application.Authorization.Policies.Access;
using CF.Common.Messaging;
using CF.Web.AspNetCore.Controllers;
using CF.Web.Models.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class HomeController : WebControllerBase
    {
        private ILogger<HomeController> _logger;

        public HomeController(IScopedMessageRecorder scopedMessageRecorder, ILogger<HomeController> logger) : base(scopedMessageRecorder)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Authorize(Policy = nameof(IUserAccessPolicy))]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            return View(new TestViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Test(TestViewModel model)
        {
            return await TryPostRedirectGetAsync(
                () => Task.CompletedTask,
                () => Redirect(Url.Action(nameof(Test).ToLower())),
                () => View(model)
                );
        }

        [HttpGet]
        public IActionResult Error(string correlationId)
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            ViewData["CorrelationId"] = correlationId;
            return View();
        }
    }
}
