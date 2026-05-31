using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TypingTest.Data;
using TypingTest.Models;
using TypingTest.Services.Interfaces;
using TypingTest.ViewModels;

namespace TypingTest.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILeaderboardService _leaderboardService;

        public UserService(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            ILeaderboardService leaderboardService)
        {
            _context = context;
            _userManager = userManager;
            _leaderboardService = leaderboardService;
        }

        // ── Get Display Name ──────────────────────────────────────────────────
        public async Task<string> GetDisplayNameAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user?.DisplayName ?? "Anonymous";
        }

        // ── Update Display Name ───────────────────────────────────────────────
        public async Task<bool> UpdateDisplayNameAsync(string userId, string displayName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            user.DisplayName = displayName;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        // ── Get Full User Stats ───────────────────────────────────────────────
        public async Task<UserStatsViewModel> GetUserStatsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var results = await _context.TestResults
                .Where(r => r.UserId == userId)
                .Include(r => r.WordPassage)
                .OrderByDescending(r => r.CompletedAt)
                .ToListAsync();

            var rank = await _leaderboardService.GetUserRankAsync(userId);

            // Map recent results to view models
            var recentResults = results.Take(10).Select(r => new TestResultViewModel
            {
                Wpm = r.Wpm,
                Accuracy = r.Accuracy,
                CorrectKeystrokes = r.CorrectKeystrokes,
                TotalKeystrokes = r.TotalKeystrokes,
                ErrorCount = r.ErrorCount,
                Mode = r.Mode,
                Difficulty = r.Difficulty,
                DurationSeconds = r.DurationSeconds,
                CompletedAt = r.CompletedAt,
                PassageTitle = r.WordPassage?.Title ?? "Unknown"
            }).ToList();

            return new UserStatsViewModel
            {
                DisplayName = user?.DisplayName ?? "Anonymous",
                TotalTestsTaken = results.Count,
                BestWpm = results.Any() ? results.Max(r => r.Wpm) : 0,
                AverageWpm = results.Any() ? results.Average(r => r.Wpm) : 0,
                AverageAccuracy = results.Any() ? results.Average(r => r.Accuracy) : 0,
                GlobalRank = rank,
                RecentResults = recentResults
            };
        }
    }
}