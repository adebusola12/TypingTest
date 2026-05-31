namespace TypingTest.Models
{
    public class GameScore
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public ApplicationUser User { get; set; } = null!;
        public string GameType { get; set; } = "";
        public int Score { get; set; }
        public int BestStreak { get; set; }
        public int WordsCompleted { get; set; }
        public int BestWpm { get; set; }
        public int PerfectHits { get; set; }
        public int Wave { get; set; }        
        public DateTime PlayedAt { get; set; } = DateTime.UtcNow;
    }
}