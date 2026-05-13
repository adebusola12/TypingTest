using TypingTest.Models;
using TypingTest.ViewModels;

namespace TypingTest.Services.Interfaces
{
    public interface ILeaderboardService
    {
        Task<LeaderboardViewModel> GetLeaderboardAsync(
            DifficultyLevel? difficulty = null,
            TestMode? mode = null,
            int page = 1,
            int pageSize = 10,
            string activeTab = "typing",
            string? currentUserId = null);

        Task<TestResult?> GetPersonalBestAsync(string userId);
        Task<int> GetUserRankAsync(string userId);

        Task<List<GameLeaderboardEntryViewModel>> GetGameLeaderboardAsync(
            string gameType,
            string? currentUserId = null,
            int topN = 10);
    }
}