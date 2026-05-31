using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TypingTest.Data;
using TypingTest.Models;
using TypingTest.Services;
using TypingTest.ViewModels;

namespace TypingTest.Controllers
{
    [Authorize]
    public class WordDropController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly WordDropService _wordDropService;
        private readonly ApplicationDbContext _db;

        public WordDropController(
            UserManager<ApplicationUser> userManager,
            WordDropService wordDropService,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _wordDropService = wordDropService;
            _db = db;
        }

        public IActionResult Index() => View("Landing");

        public IActionResult Play()
        {
            var model = new WordDropLaunchViewModel
            {
                ReturnToStage = null,
                WarmupWavesRequired = 0
            };
            return View("Index", model);
        }

        public IActionResult Warmup(int stage)
        {
            var model = new WordDropLaunchViewModel
            {
                ReturnToStage = stage,
                WarmupWavesRequired = WordDropService.WarmupWavesRequired
            };
            return View("Index", model);
        }

        // GET: /WordDrop/Results
        [HttpGet]
        public async Task<IActionResult> Results(int score, int wave, int words, int wpm)
        {
            var user = await _userManager.GetUserAsync(User);

            int previousBest = 0;
            bool isNewHighScore = false;

            if (user != null)
            {
                // Get previous best
                var best = await _db.GameScores
                    .Where(g => g.UserId == user.Id && g.GameType == "WordDrop")
                    .OrderByDescending(g => g.Score)
                    .FirstOrDefaultAsync();

                previousBest = best?.Score ?? 0;
                isNewHighScore = score > previousBest;

                // Save score
                _db.GameScores.Add(new GameScore
                {
                    UserId = user.Id,
                    GameType = "WordDrop",
                    Score = score,
                    Wave = wave,
                    WordsCompleted = words,
                    BestWpm = wpm,
                    PlayedAt = DateTime.UtcNow
                });

                await _db.SaveChangesAsync();
            }

            var model = new WordDropResultViewModel
            {
                Score = score,
                Wave = wave,
                WordsDestroyed = words,
                BestWpm = wpm,
                PreviousBest = previousBest,
                IsNewHighScore = isNewHighScore
            };

            return View("Results", model);
        }

        // POST: /WordDrop/GameOver (warmup only)
        [HttpPost]
        public async Task<IActionResult> GameOver([FromBody] WordDropScoreViewModel score)
        {
            if (score.ReturnToStage.HasValue && score.WarmupCompleted)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    user.FailedAttempts = 0;
                    await _userManager.UpdateAsync(user);
                }
            }
            return Ok();
        }
    }
}