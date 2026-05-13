namespace TypingTest.ViewModels
{
    // Passed to the Index view to configure the game on launch
    public class WordDropLaunchViewModel
    {
        // If set, this is a warm-up session linked to a specific stage
        public int? ReturnToStage { get; set; }

        // How many waves must be cleared to complete the warm-up (0 = free play)
        public int WarmupWavesRequired { get; set; } = 0;

        public bool IsWarmup => ReturnToStage.HasValue;
    }

    // Posted to GameOver at the end of the game
    public class WordDropScoreViewModel
    {
        public int Score { get; set; }
        public int Wave { get; set; }
        public int WordsDestroyed { get; set; }
        public int BestWpm { get; set; }

        // Warm-up context — null if free play
        public int? ReturnToStage { get; set; }

        // True if the player cleared the required waves in warm-up mode
        public bool WarmupCompleted { get; set; }
    }

    public class WordDropResultViewModel
    {
        public int Score { get; set; }
        public int Wave { get; set; }
        public int WordsDestroyed { get; set; }
        public int BestWpm { get; set; }
        public int PreviousBest { get; set; }
        public bool IsNewHighScore { get; set; }
    }
}