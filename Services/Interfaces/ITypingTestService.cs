using TypingTest.Models;
using TypingTest.ViewModels;

namespace TypingTest.Services.Interfaces
{
    public interface ITypingTestService
    {
        // Calculate WPM from keystrokes and elapsed time
        int CalculateWpm(int correctKeystrokes, int elapsedSeconds);

        // Calculate accuracy as a percentage
        double CalculateAccuracy(int correctKeystrokes, int totalKeystrokes);

        // Save a completed test result to the database
        Task<TestResult> SaveResultAsync(SubmitTestViewModel submission, string userId);

        // Get a single result by ID
        Task<TestResult?> GetResultByIdAsync(int id);

        // Get all results for a specific user
        Task<List<TestResult>> GetUserResultsAsync(string userId);
    }
}
