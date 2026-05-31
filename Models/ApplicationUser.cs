using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TypingTest.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        public string DisplayName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CurrentStage { get; set; } = 1;

        // Tracks consecutive failures on the current stage
        // Resets to 0 whenever the player advances or completes a warm-up
        public int FailedAttempts { get; set; } = 0;

        // Navigation property — one user has many test results
        public ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
    }
}
