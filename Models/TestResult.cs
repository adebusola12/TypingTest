using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TypingTest.Models
{
    public class TestResult
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        public int WordPassageId { get; set; }

        [ForeignKey(nameof(WordPassageId))]
        public WordPassage? WordPassage { get; set; } 

        [Range(0, 300)]
        public int Wpm { get; set; } //Words per minute

        [Range(0.0,100.0)]
        public double Accuracy { get; set; } //Percentage, e.g. 97.4

        public int CorrectKeystrokes { get; set; }
        public int TotalKeystrokes { get; set; }
        public int ErrorCount { get; set; }


        //Test configuration at time of attempt
        public TestMode Mode { get; set; }  //Timed or WordCount
        public DifficultyLevel Difficulty { get; set; }
        public int DurationSeconds { get; set; } //For Timed mode (e.g. 30, 60, 120)
        public int WordCountTarget { get; set; } //For word-count mode
        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;



    }

    public enum TestMode
    {
        Timed,
        WordCount,
            Survival
    }

    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }
}
