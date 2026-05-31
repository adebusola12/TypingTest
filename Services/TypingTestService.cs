using Microsoft.EntityFrameworkCore;
using TypingTest.Data;
using TypingTest.Models;
using TypingTest.Services.Interfaces;
using TypingTest.ViewModels;

namespace TypingTest.Services
{
    public class TypingTestService : ITypingTestService
    {
        private readonly ApplicationDbContext _context;

        public TypingTestService(ApplicationDbContext context)
        {
            _context = context;
        }

        // ── WPM Calculation ───────────────────────────────────────────────────
        // Standard formula: (correct keystrokes / 5) / minutes elapsed
        public int CalculateWpm(int correctKeystrokes, int elapsedSeconds)
        {
            if (elapsedSeconds <= 0) return 0;
            double minutes = elapsedSeconds / 60.0;
            double words = correctKeystrokes / 5.0;
            return (int)Math.Round(words / minutes);
        }

        // ── Accuracy Calculation ──────────────────────────────────────────────
        // Based on FINAL character states — corrected mistypes count as correct
        // finalCorrectChars = chars marked correct at end of test
        // finalErrorChars   = chars still marked incorrect at end of test
        public double CalculateAccuracy(int finalCorrectChars, int finalErrorChars)
        {
            int total = finalCorrectChars + finalErrorChars;
            if (total <= 0) return 0;
            return Math.Round((double)finalCorrectChars / total * 100, 2);
        }

        // ── Save Result ───────────────────────────────────────────────────────
        public async Task<TestResult> SaveResultAsync(SubmitTestViewModel submission, string userId)
        {
            var wpm = CalculateWpm(submission.CorrectKeystrokes, submission.ElapsedSeconds);
            // Use final char states for accuracy — not raw keystrokes
            var accuracy = CalculateAccuracy(submission.FinalCorrectChars, submission.FinalErrorChars);

            var result = new TestResult
            {
                UserId = userId,
                WordPassageId = submission.PassageId,
                Wpm = wpm,
                Accuracy = accuracy,
                CorrectKeystrokes = submission.CorrectKeystrokes,
                TotalKeystrokes = submission.TotalKeystrokes,
                ErrorCount = submission.FinalErrorChars,  // uncorrected errors only
                Mode = submission.Mode,
                Difficulty = submission.Difficulty,
                DurationSeconds = submission.DurationSeconds,
                WordCountTarget = submission.WordCountTarget,
                CompletedAt = DateTime.UtcNow
            };

            _context.TestResults.Add(result);
            await _context.SaveChangesAsync();
            return result;
        }

        // ── Get Single Result ─────────────────────────────────────────────────
        public async Task<TestResult?> GetResultByIdAsync(int id)
        {
            return await _context.TestResults
                .Include(r => r.WordPassage)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        // ── Get All Results For a User ────────────────────────────────────────
        public async Task<List<TestResult>> GetUserResultsAsync(string userId)
        {
            return await _context.TestResults
                .Where(r => r.UserId == userId)
                .Include(r => r.WordPassage)
                .OrderByDescending(r => r.CompletedAt)
                .ToListAsync();
        }
    }
}