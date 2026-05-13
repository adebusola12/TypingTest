using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TypingTest.Models;
using TypingTest.Services;
using TypingTest.Services.Interfaces;
using TypingTest.ViewModels;

namespace TypingTest.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private readonly ITypingTestService _typingTestService;
        private readonly IWordService _wordService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly StageProgressionService _stageService;

        // How many consecutive failures before the Word Drop prompt appears
        private const int FailsBeforeWarmup = WordDropService.FailsBeforePrompt;

        public TestController(
            ITypingTestService typingTestService,
            IWordService wordService,
            UserManager<ApplicationUser> userManager,
            StageProgressionService stageService)
        {
            _typingTestService = typingTestService;
            _wordService = wordService;
            _userManager = userManager;
            _stageService = stageService;
        }

        // GET: /Test
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var stage = _stageService.GetStage(user.CurrentStage);
            return RedirectToAction(nameof(Start), new
            {
                difficulty = stage.Difficulty,
                mode = TestMode.Timed,
                duration = stage.DurationSeconds
            });
        }

        // GET: /Test/Start
        public async Task<IActionResult> Start(
            DifficultyLevel difficulty = DifficultyLevel.Easy,
            TestMode mode = TestMode.Timed,
            int duration = 15,
            int wordCount = 50)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Content("User is null");

            var stage = _stageService.GetStage(user.CurrentStage);
            if (stage == null) return Content("Stage is null - CurrentStage: " + user.CurrentStage);

            var passage = await _wordService.GetRandomPassageAsync(stage.Difficulty, stage.Stage);
            if (passage == null) return Content("Passage is null - Difficulty: " + stage.Difficulty + " Stage: " + stage.Stage);

            var session = new TestSessionViewModel
            {
                PassageId = passage.Id,
                PassageContent = passage.Content,
                PassageTitle = passage.Title,
                Difficulty = stage.Difficulty,
                Mode = TestMode.Timed,
                DurationSeconds = stage.DurationSeconds,
                WordCountTarget = wordCount,
                CurrentStage = stage.Stage,
                StageName = stage.Name,
                MinAccuracy = stage.MinAccuracy
            };

            return View(session);
        }

        // POST: /Test/Submit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(SubmitTestViewModel submission)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            var user = await _userManager.GetUserAsync(User)!;
            var result = await _typingTestService.SaveResultAsync(submission, user.Id);
            var accuracy = _typingTestService.CalculateAccuracy(
                submission.FinalCorrectChars, submission.FinalErrorChars);

            var advanced = await _stageService.TryAdvanceStageAsync(
                user, accuracy, submission.ElapsedSeconds, submission.PassageCompleted);

            bool suggestWarmup = false;
            string failReason = "";

            if (advanced)
            {
                user.FailedAttempts = 0;
                await _userManager.UpdateAsync(user);
            }
            else
            {
                // Work out exactly why the stage was not cleared
                var stage = _stageService.GetStage(user.CurrentStage);

                if (!submission.PassageCompleted)
                    failReason = "You did not finish the passage before the timer ran out.";
                else if (accuracy < stage.MinAccuracy)
                    failReason = $"Your accuracy was {accuracy:F1}% — you need at least {stage.MinAccuracy}%. " +
                                 $"You had {submission.FinalErrorChars} uncorrected mistake(s).";

                user.FailedAttempts++;
                await _userManager.UpdateAsync(user);

                if (user.FailedAttempts >= FailsBeforeWarmup)
                    suggestWarmup = true;
            }

            return RedirectToAction(nameof(Results), new
            {
                id = result.Id,
                advanced,
                suggestWarmup,
                failReason
            });
        }

        // GET: /Test/Results/5
        public async Task<IActionResult> Results(int id, bool advanced = false, bool suggestWarmup = false, string failReason = "")
        {
            var result = await _typingTestService.GetResultByIdAsync(id);
            if (result == null) return NotFound();

            var userId = _userManager.GetUserId(User)!;
            if (result.UserId != userId) return Forbid();

            var user = await _userManager.GetUserAsync(User)!;
            var stage = _stageService.GetStage(user.CurrentStage);

            var viewModel = new TestResultViewModel
            {
                Wpm = result.Wpm,
                Accuracy = result.Accuracy,
                CorrectKeystrokes = result.CorrectKeystrokes,
                TotalKeystrokes = result.TotalKeystrokes,
                ErrorCount = result.ErrorCount,
                Mode = result.Mode,
                Difficulty = result.Difficulty,
                DurationSeconds = result.DurationSeconds,
                CompletedAt = result.CompletedAt,
                PassageTitle = result.WordPassage?.Title ?? "Unknown",
                StageAdvanced = advanced,
                CurrentStage = user.CurrentStage,
                StageName = stage.Name,
                NextMinAccuracy = stage.MinAccuracy,
                NextDuration = stage.DurationSeconds,
                SuggestWarmup = suggestWarmup,
                FailedAttempts = user.FailedAttempts,
                FailReason = failReason
            };

            return View(viewModel);
        }

        // GET: /Test/History
        public async Task<IActionResult> History()
        {
            var userId = _userManager.GetUserId(User)!;
            var results = await _typingTestService.GetUserResultsAsync(userId);

            var viewModels = results.Select(r => new TestResultViewModel
            {
                Wpm = r.Wpm,
                Accuracy = r.Accuracy,
                CorrectKeystrokes = r.CorrectKeystrokes,
                TotalKeystrokes = r.TotalKeystrokes,
                ErrorCount = r.ErrorCount,
                Mode = r.Mode,
                Difficulty = r.Difficulty,
                DurationSeconds = r.DurationSeconds,
                CompletedAt = r.CompletedAt,
                PassageTitle = r.WordPassage?.Title ?? "Unknown"
            }).ToList();

            return View(viewModels);
        }
    }
}