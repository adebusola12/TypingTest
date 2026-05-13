using Microsoft.EntityFrameworkCore;
using TypingTest.Data;
using TypingTest.Models;
using TypingTest.Services.Interfaces;
using TypingTest.ViewModels;

namespace TypingTest.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly ApplicationDbContext _context;

        public LeaderboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        // ── Typing Test Leaderboard ───────────────────────────────────────────
        public async Task<LeaderboardViewModel> GetLeaderboardAsync(
            DifficultyLevel? difficulty = null,
            TestMode? mode = null,
            int page = 1,
            int pageSize = 10,
            string activeTab = "typing",
            string? currentUserId = null)
        {
            var query = _context.TestResults.AsQueryable();

            if (difficulty.HasValue)
                query = query.Where(r => r.Difficulty == difficulty.Value);
            if (mode.HasValue)
                query = query.Where(r => r.Mode == mode.Value);

            var bestResultIds = await query
                .GroupBy(r => r.UserId)
                .Select(g => g.OrderByDescending(r => r.Wpm).First().Id)
                .ToListAsync();

            var totalCount = bestResultIds.Count;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var entries = await _context.TestResults
                .Include(r => r.User)
                .Where(r => bestResultIds.Contains(r.Id))
                .OrderByDescending(r => r.Wpm)
                .ThenByDescending(r => r.Accuracy)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var ranked = entries.Select((r, index) => new LeaderboardEntryViewModel
            {
                Rank = (page - 1) * pageSize + index + 1,
                DisplayName = r.User?.DisplayName ?? "Anonymous",
                Wpm = r.Wpm,
                Accuracy = r.Accuracy,
                Difficulty = r.Difficulty,
                Mode = r.Mode,
                CompletedAt = r.CompletedAt
            }).ToList();

            // Load game leaderboards
            var sniperEntries = await GetGameLeaderboardAsync("WordSniper", currentUserId);
            var wordDropEntries = await GetGameLeaderboardAsync("WordDrop", currentUserId);
            var chainEntries = await GetGameLeaderboardAsync("ChainMode", currentUserId);

            return new LeaderboardViewModel
            {
                Entries = ranked,
                FilterDifficulty = difficulty,
                FilterMode = mode,
                PageNumber = page,
                TotalPages = totalPages,
                SniperEntries = sniperEntries,
                WordDropEntries = wordDropEntries,
                ChainEntries = chainEntries,
                ActiveTab = activeTab
            };
        }

        // ── Game Leaderboard ──────────────────────────────────────────────────
        public async Task<List<GameLeaderboardEntryViewModel>> GetGameLeaderboardAsync(
            string gameType,
            string? currentUserId = null,
            int topN = 10)
        {
            // Get best score per user for this game
            var bestScoreIds = await _context.GameScores
                .Where(g => g.GameType == gameType)
                .GroupBy(g => g.UserId)
                .Select(g => g.OrderByDescending(s => s.Score).First().Id)
                .ToListAsync();

            var scores = await _context.GameScores
                .Include(g => g.User)
                .Where(g => bestScoreIds.Contains(g.Id))
                .OrderByDescending(g => g.Score)
                .Take(topN)
                .ToListAsync();

            return scores.Select((g, i) => new GameLeaderboardEntryViewModel
            {
                Rank = i + 1,
                DisplayName = g.User?.DisplayName ?? "Anonymous",
                Score = g.Score,
                BestWpm = g.BestWpm,
                BestStreak = g.BestStreak,
                Words = g.WordsCompleted,
                Wave = g.Wave,
                PerfectHits = g.PerfectHits,
                PlayedAt = g.PlayedAt,
                IsCurrentUser = g.UserId == currentUserId
            }).ToList();
        }

        // ── Personal Best ─────────────────────────────────────────────────────
        public async Task<TestResult?> GetPersonalBestAsync(string userId)
        {
            return await _context.TestResults
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.Wpm)
                .FirstOrDefaultAsync();
        }

        // ── User Rank ─────────────────────────────────────────────────────────
        public async Task<int> GetUserRankAsync(string userId)
        {
            var bestScores = await _context.TestResults
                .GroupBy(r => r.UserId)
                .Select(g => new
                {
                    UserId = g.Key,
                    BestWpm = g.Max(r => r.Wpm)
                })
                .OrderByDescending(x => x.BestWpm)
                .ToListAsync();

            var userEntry = bestScores.FirstOrDefault(x => x.UserId == userId);
            if (userEntry == null) return 0;
            return bestScores.IndexOf(userEntry) + 1;
        }
    }
}