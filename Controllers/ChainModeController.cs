using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TypingTest.Data;
using TypingTest.Models;

namespace TypingTest.Controllers
{
    [Authorize]
    public class ChainModeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChainModeController(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index() => View("Landing");
        public IActionResult Play() => View();

        // POST: /ChainMode/SaveScore
        [HttpPost]
        public async Task<IActionResult> SaveScore([FromBody] ChainScoreRequest req)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            // Get previous best
            var previousBest = await _db.GameScores
                .Where(g => g.UserId == user.Id && g.GameType == "ChainMode")
                .OrderByDescending(g => g.Score)
                .FirstOrDefaultAsync();

            int previousBestScore = previousBest?.Score ?? 0;
            bool isNewHighScore = req.Score > previousBestScore;

            // Save new score
            _db.GameScores.Add(new GameScore
            {
                UserId = user.Id,
                GameType = "ChainMode",
                Score = req.Score,
                BestStreak = req.BestChain,
                WordsCompleted = req.WordsChained,
                BestWpm = req.BestWpm,
                PerfectHits = req.PerfectHits,
                PlayedAt = DateTime.UtcNow
            });

            await _db.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                isNewHighScore = isNewHighScore,
                previousBest = previousBestScore,
                currentScore = req.Score
            });
        }
    }

    public class ChainScoreRequest
    {
        public int Score { get; set; }
        public int BestChain { get; set; }
        public int WordsChained { get; set; }
        public int BestWpm { get; set; }
        public int PerfectHits { get; set; }
    }
}