using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TypingTest.Data;
using TypingTest.Models;
using TypingTest.ViewModels;

namespace TypingTest.Controllers
{
    [Authorize]
    public class WordRacerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public WordRacerController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        // GET: /WordRacer
        public IActionResult Index() => View("Landing");

        // GET: /WordRacer/Play
        public IActionResult Play()
        {
            var model = new WordRacerLaunchViewModel();
            return View("Index", model);
        }

        // GET: /WordRacer/Results
        [HttpGet]
        public async Task<IActionResult> Results(
            int score,
            int wave,
            int words,
            int wpm,
            int streak,
            int cars,
            int powerups)
        {
            var user = await _userManager.GetUserAsync(User);

            int previousBest = 0;
            bool isNewHighScore = false;

            if (user != null)
            {
                var best = await _db.GameScores
                    .Where(g => g.UserId == user.Id && g.GameType == "WordRacer")
                    .OrderByDescending(g => g.Score)
                    .FirstOrDefaultAsync();

                previousBest = best?.Score ?? 0;
                isNewHighScore = score > previousBest;

                _db.GameScores.Add(new GameScore
                {
                    UserId = user.Id,
                    GameType = "WordRacer",
                    Score = score,
                    Wave = wave,
                    WordsCompleted = words,
                    BestWpm = wpm,
                    BestStreak = streak,
                    PerfectHits = cars,
                    PlayedAt = DateTime.UtcNow
                });

                await _db.SaveChangesAsync();
            }

            var model = new WordRacerResultViewModel
            {
                Score = score,
                Wave = wave,
                WordsDestroyed = words,
                BestWpm = wpm,
                BestStreak = streak,
                CarsDestroyed = cars,
                PowerUpsUsed = powerups,
                PreviousBest = previousBest,
                IsNewHighScore = isNewHighScore
            };

            return View("Results", model);
        }
    }
}