using Microsoft.AspNetCore.Identity;
using TypingTest.Models;

namespace TypingTest.Services
{
    public class StageInfo
    {
        public int Stage { get; set; }
        public string Name { get; set; } = "";
        public DifficultyLevel Difficulty { get; set; }
        public int DurationSeconds { get; set; }
        public double MinAccuracy { get; set; }
    }

    public class StageProgressionService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public static readonly List<StageInfo> Stages = new()
        {
            // ── Easy (Stages 1–4) ─────────────────────────────────────────
            new StageInfo { Stage = 1,  Name = "Novice",      Difficulty = DifficultyLevel.Easy,   DurationSeconds = 15,  MinAccuracy = 80 },
            new StageInfo { Stage = 2,  Name = "Beginner",    Difficulty = DifficultyLevel.Easy,   DurationSeconds = 20,  MinAccuracy = 82 },
            new StageInfo { Stage = 3,  Name = "Apprentice",  Difficulty = DifficultyLevel.Easy,   DurationSeconds = 30,  MinAccuracy = 84 },
            new StageInfo { Stage = 4,  Name = "Rising",      Difficulty = DifficultyLevel.Easy,   DurationSeconds = 35,  MinAccuracy = 85 },

            // ── Medium (Stages 5–8) ───────────────────────────────────────
            new StageInfo { Stage = 5,  Name = "Skilled",     Difficulty = DifficultyLevel.Medium, DurationSeconds = 45,  MinAccuracy = 86 },
            new StageInfo { Stage = 6,  Name = "Adept",       Difficulty = DifficultyLevel.Medium, DurationSeconds = 60,  MinAccuracy = 87 },
            new StageInfo { Stage = 7,  Name = "Capable",     Difficulty = DifficultyLevel.Medium, DurationSeconds = 75,  MinAccuracy = 88 },
            new StageInfo { Stage = 8,  Name = "Proficient",  Difficulty = DifficultyLevel.Medium, DurationSeconds = 90,  MinAccuracy = 89 },

            // ── Hard (Stages 9–15) ────────────────────────────────────────
            new StageInfo { Stage = 9,  Name = "Expert",      Difficulty = DifficultyLevel.Hard,   DurationSeconds = 105, MinAccuracy = 90 },
            new StageInfo { Stage = 10, Name = "Advanced",    Difficulty = DifficultyLevel.Hard,   DurationSeconds = 120, MinAccuracy = 91 },
            new StageInfo { Stage = 11, Name = "Veteran",     Difficulty = DifficultyLevel.Hard,   DurationSeconds = 135, MinAccuracy = 92 },
            new StageInfo { Stage = 12, Name = "Elite",       Difficulty = DifficultyLevel.Hard,   DurationSeconds = 150, MinAccuracy = 93 },
            new StageInfo { Stage = 13, Name = "Master",      Difficulty = DifficultyLevel.Hard,   DurationSeconds = 165, MinAccuracy = 95 },
            new StageInfo { Stage = 14, Name = "Legend",      Difficulty = DifficultyLevel.Hard,   DurationSeconds = 180, MinAccuracy = 97 },
            new StageInfo { Stage = 15, Name = "Grandmaster", Difficulty = DifficultyLevel.Hard,   DurationSeconds = 210, MinAccuracy = 99 },
        };

        public StageProgressionService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public StageInfo GetStage(int stage)
        {
            return Stages.FirstOrDefault(s => s.Stage == stage) ?? Stages.First();
        }

        // Player must satisfy ALL THREE conditions to pass:
        // 1. Complete the entire passage
        // 2. Finish within the time limit
        // 3. Meet the minimum accuracy threshold
        public bool IsPassed(StageInfo stage, double accuracy, int elapsedSeconds, bool passageCompleted)
        {
            if (!passageCompleted) return false;
            if (elapsedSeconds > stage.DurationSeconds) return false;
            if (accuracy < stage.MinAccuracy) return false;
            return true;
        }

        public async Task<bool> TryAdvanceStageAsync(
            ApplicationUser user,
            double accuracy,
            int elapsedSeconds,
            bool passageCompleted)
        {
            var currentStage = GetStage(user.CurrentStage);

            if (!IsPassed(currentStage, accuracy, elapsedSeconds, passageCompleted))
                return false;

            if (user.CurrentStage < 15)
                user.CurrentStage++;

            await _userManager.UpdateAsync(user);
            return true;
        }
    }
}