using Microsoft.EntityFrameworkCore;
using TypingTest.Data;
using TypingTest.Models;
using TypingTest.Services.Interfaces;

namespace TypingTest.Services
{
    public class WordService : IWordService
    {
        private readonly ApplicationDbContext _context;
        private readonly Random _random = new();

        public WordService(ApplicationDbContext context)
        {
            _context = context;
        }

        // ── Get Random Passage by Difficulty ──────────────────────────────────
        public async Task<WordPassage?> GetRandomPassageAsync(DifficultyLevel difficulty, int stage = 0)
        {
            var passages = await _context.WordPassages
                .Where(p => p.Difficulty == difficulty && p.IsActive &&
                (p.Stage == stage || p.Stage ==0))
                .ToListAsync();

            if (!passages.Any()) return null;

            // Pick a random one from the filtered list
            var index = _random.Next(passages.Count);
            return passages[index];
        }

        // ── Get Passage by ID ─────────────────────────────────────────────────
        public async Task<WordPassage?> GetPassageByIdAsync(int id)
        {
            return await _context.WordPassages
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
        }

        // ── Get All Active Passages ───────────────────────────────────────────
        public async Task<List<WordPassage>> GetAllPassagesAsync()
        {
            return await _context.WordPassages
                .Where(p => p.IsActive)
                .OrderBy(p => p.Difficulty)
                .ThenBy(p => p.Title)
                .ToListAsync();
        }
    }
}