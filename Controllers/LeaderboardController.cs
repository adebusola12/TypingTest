using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TypingTest.Models;
using TypingTest.Services.Interfaces;
using TypingTest.ViewModels;

namespace TypingTest.Controllers
{
    public class LeaderboardController : Controller
    {
        private readonly ILeaderboardService _leaderboardService;
        private readonly UserManager<ApplicationUser> _userManager;

        public LeaderboardController(
            ILeaderboardService leaderboardService,
            UserManager<ApplicationUser> userManager)
        {
            _leaderboardService = leaderboardService;
            _userManager = userManager;
        }

        // GET: /Leaderboard
        // GET: /Leaderboard?tab=sniper
        public async Task<IActionResult> Index(
            DifficultyLevel? difficulty = null,
            TestMode? mode = null,
            int page = 1,
            string tab = "typing")
        {
            var currentUserId = _userManager.GetUserId(User);

            var viewModel = await _leaderboardService.GetLeaderboardAsync(
                difficulty: difficulty,
                mode: mode,
                page: page,
                pageSize: 10,
                activeTab: tab,
                currentUserId: currentUserId);

            return View(viewModel);
        }
    }
}