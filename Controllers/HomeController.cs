using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TypingTest.Models;
using TypingTest.Services.Interfaces;

namespace TypingTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITypingTestService _typingTestService;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(
            IUserService userService,
            ITypingTestService typingTestService,
            UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _typingTestService = typingTestService;
            _userManager = userManager;
        }

        // GET: /
        public async Task<IActionResult> Index()
        {
            // If user is logged in, pass their stats to the view
            if (User.Identity?.IsAuthenticated == true)
            {
                var userId = _userManager.GetUserId(User)!;
                var stats = await _userService.GetUserStatsAsync(userId);
                return View(stats);
            }

            return View();
        }

        // GET: /Home/Privacy
        public IActionResult Privacy() => View();

        // GET: /Home/Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View();
    }
}