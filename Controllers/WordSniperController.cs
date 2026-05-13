using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TypingTest.Data;
using TypingTest.Models;

namespace TypingTest.Controllers
{
    [Authorize]
    public class WordSniperController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public WordSniperController(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        // GET: /WordSniper
        public IActionResult Index() => View("Landing");

        // GET: /WordSniper/Play
        public IActionResult Play() => View();

        // POST: /WordSniper/SaveScore
        [HttpPost]
        public async Task<IActionResult> SaveScore([FromBody] SniperScoreRequest req)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            // Get previous best score
            var previousBest = await _db.GameScores
                .Where(g => g.UserId == user.Id && g.GameType == "WordSniper")
                .OrderByDescending(g => g.Score)
                .FirstOrDefaultAsync();

            int previousBestScore = previousBest?.Score ?? 0;
            bool isNewHighScore = req.Score > previousBestScore;

            // Save new score
            _db.GameScores.Add(new GameScore
            {
                UserId = user.Id,
                GameType = "WordSniper",
                Score = req.Score,
                BestStreak = req.BestStreak,
                WordsCompleted = req.WordsHit,
                BestWpm = req.BestWpm,
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

    public class SniperScoreRequest
    {
        public int Score { get; set; }
        public int BestStreak { get; set; }
        public int WordsHit { get; set; }
        public int BestWpm { get; set; }
    }
}