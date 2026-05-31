using System.ComponentModel.DataAnnotations;
using TypingTest.Models;

namespace TypingTest.Models
{
    public class WordPassage
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;   // The actual text to
                                                              //
        public DifficultyLevel Difficulty { get; set; }
        public int Stage { get; set; } = 0;
        public int WordCount { get; set; }                     // Pre-computed for filtering

        public bool IsActive { get; set; } = true;            // Soft-disable without deleting

        // Navigation — a passage can appear in many test results
        public ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
    }
}