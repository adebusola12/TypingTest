using TypingTest.Models;

namespace TypingTest.Services.Interfaces
{
    public interface IWordService
    {
        // Get a random passage for a given difficulty
        Task<WordPassage?> GetRandomPassageAsync(DifficultyLevel difficulty,int stage = 0);

        // Get a specific passage by ID
        Task<WordPassage?> GetPassageByIdAsync(int id);

        // Get all active passages (for admin/seeding purposes)
        Task<List<WordPassage>> GetAllPassagesAsync();
    }
}
