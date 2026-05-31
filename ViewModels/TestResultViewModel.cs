using TypingTest.Models;

namespace TypingTest.ViewModels
{
    // Passed to the Results view after a test is submitted
    public class TestResultViewModel
    {
        public int Wpm { get; set; }
        public double Accuracy { get; set; }
        public int CorrectKeystrokes { get; set; }
        public int TotalKeystrokes { get; set; }
        public int ErrorCount { get; set; }
        public TestMode Mode { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public int DurationSeconds { get; set; }
        public int WordCountTarget { get; set; }
        public DateTime CompletedAt { get; set; }
        public string PassageTitle { get; set; } = string.Empty;
        public bool StageAdvanced { get; set; }
        public int CurrentStage { get; set; }
        public string StageName { get; set; } = "";
        public double NextMinAccuracy { get; set; }
        public int NextDuration { get; set; }

        // Warm-up prompt fields
        public bool SuggestWarmup { get; set; } = false;
        public int FailedAttempts { get; set; } = 0;

        // Specific reason why the stage was not cleared
        public string FailReason { get; set; } = "";

        // Friendly display helpers
        public string AccuracyDisplay => $"{Accuracy:F1}%";
        public string DurationDisplay => DurationSeconds >= 60
            ? $"{DurationSeconds / 60}m {DurationSeconds % 60}s"
            : $"{DurationSeconds}s";
    }

    // Passed to the Test view to start a new test
    public class TestSessionViewModel
    {
        public int PassageId { get; set; }
        public string PassageContent { get; set; } = string.Empty;
        public string PassageTitle { get; set; } = string.Empty;
        public DifficultyLevel Difficulty { get; set; }
        public TestMode Mode { get; set; }
        public int DurationSeconds { get; set; }
        public int WordCountTarget { get; set; }
        public int CurrentStage { get; set; }
        public string StageName { get; set; } = "";
        public double MinAccuracy { get; set; }
    }

    // Submitted from the browser when the user finishes typing
    public class SubmitTestViewModel
    {
        public int PassageId { get; set; }
        public int TotalKeystrokes { get; set; }
        public int CorrectKeystrokes { get; set; }  // used for WPM only
        public int ErrorCount { get; set; }
        public int ElapsedSeconds { get; set; }
        public TestMode Mode { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public int DurationSeconds { get; set; }
        public int WordCountTarget { get; set; }
        public bool PassageCompleted { get; set; }

        // Final character states — corrected mistypes count as correct
        // These are used for accuracy calculation instead of raw keystrokes
        public int FinalCorrectChars { get; set; }
        public int FinalErrorChars { get; set; }
    }
}