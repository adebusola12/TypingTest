using TypingTest.Models;

namespace TypingTest.ViewModels
{
    public class UserStatsViewModel
    {
        public string DisplayName { get; set; } = string.Empty;
        public int TotalTestsTaken { get; set; }
        public int BestWpm { get; set; }
        public double AverageWpm { get; set; }
        public double AverageAccuracy { get; set; }
        public int GlobalRank { get; set; }
        public List<TestResultViewModel> RecentResults { get; set; } = new();

        public string AverageWpmDisplay => $"{AverageWpm:F0}";
        public string AverageAccuracyDisplay => $"{AverageAccuracy:F1}%";
    }
}