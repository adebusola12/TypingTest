namespace TypingTest.ViewModels
{
    // ── Launch ────────────────────────────────────────────────────────────────
    public class WordRacerLaunchViewModel
    {
        // Reserved for future use (difficulty select etc.)
    }

    // ── Results ───────────────────────────────────────────────────────────────
    public class WordRacerResultViewModel
    {
        public int Score { get; set; }
        public int Wave { get; set; }
        public int WordsDestroyed { get; set; }
        public int BestWpm { get; set; }
        public int BestStreak { get; set; }
        public int CarsDestroyed { get; set; }
        public int PowerUpsUsed { get; set; }
        public int PreviousBest { get; set; }
        public bool IsNewHighScore { get; set; }

        public string WaveRating => Wave switch
        {
            <= 2 => "ROOKIE 🐢",
            <= 4 => "RACER 🚗",
            <= 6 => "PRO 🏎️",
            <= 8 => "ELITE ⚡",
            _ => "LEGEND 🔥"
        };

        public string RatingColor => Wave switch
        {
            <= 2 => "#6b7280",
            <= 4 => "#fb923c",
            <= 6 => "#00ffaa",
            <= 8 => "#facc15",
            _ => "#ff3355"
        };
    }
}