using TypingTest.Models;

namespace TypingTest.ViewModels
{
    // ── Typing Test entry ─────────────────────────────────────────────────────
    public class LeaderboardEntryViewModel
    {
        public int Rank { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public int Wpm { get; set; }
        public double Accuracy { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public TestMode Mode { get; set; }
        public DateTime CompletedAt { get; set; }

        public string AccuracyDisplay => $"{Accuracy:F1}%";
        public string TimeAgo
        {
            get
            {
                var diff = DateTime.UtcNow - CompletedAt;
                if (diff.TotalMinutes < 1) return "just now";
                if (diff.TotalHours < 1) return $"{(int)diff.TotalMinutes}m ago";
                if (diff.TotalDays < 1) return $"{(int)diff.TotalHours}h ago";
                return $"{(int)diff.TotalDays}d ago";
            }
        }
    }

    // ── Game score entry (Sniper / Drop / Chain / Sprint) ─────────────────────
    public class GameLeaderboardEntryViewModel
    {
        public int Rank { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public int Score { get; set; }
        public int BestWpm { get; set; }
        public int BestStreak { get; set; }
        public int Words { get; set; }
        public int Wave { get; set; }           // Reused for finish position in races
        public int PerfectHits { get; set; }
        public DateTime PlayedAt { get; set; }
        public bool IsCurrentUser { get; set; }

        public string TimeAgo
        {
            get
            {
                var diff = DateTime.UtcNow - PlayedAt;
                if (diff.TotalMinutes < 1) return "just now";
                if (diff.TotalHours < 1) return $"{(int)diff.TotalMinutes}m ago";
                if (diff.TotalDays < 1) return $"{(int)diff.TotalHours}h ago";
                return $"{(int)diff.TotalDays}d ago";
            }
        }
    }

    // ── Full leaderboard page ─────────────────────────────────────────────────
    public class LeaderboardViewModel
    {
        // Typing test
        public List<LeaderboardEntryViewModel> Entries { get; set; } = new();

        public DifficultyLevel? FilterDifficulty { get; set; }
        public TestMode? FilterMode { get; set; }
        public int PageNumber { get; set; } = 1;
        public int TotalPages { get; set; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        // Arcade / Mini-games
        public List<GameLeaderboardEntryViewModel> SniperEntries { get; set; } = new();
        public List<GameLeaderboardEntryViewModel> WordDropEntries { get; set; } = new();
        public List<GameLeaderboardEntryViewModel> ChainEntries { get; set; } = new();
        public List<GameLeaderboardEntryViewModel> WordRacerEntries { get; set; } = new();

        // Active tab
        public string ActiveTab { get; set; } = "typing";
    }
}