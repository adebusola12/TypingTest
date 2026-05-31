using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TypingTest.Models;
using TypingTest.Services.Interfaces;

namespace TypingTest.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            IUserService userService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: /Account/Profile
        // Shows the user's stats and recent results
        public async Task<IActionResult> Profile()
        {
            var userId = _userManager.GetUserId(User)!;
            var stats = await _userService.GetUserStatsAsync(userId);
            return View(stats);
        }

        // GET: /Account/EditProfile
        public async Task<IActionResult> EditProfile()
        {
            var userId = _userManager.GetUserId(User)!;
            var displayName = await _userService.GetDisplayNameAsync(userId);
            ViewBag.DisplayName = displayName;
            return View();
        }

        // POST: /Account/EditProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(string displayName)
        {
            if (string.IsNullOrWhiteSpace(displayName) || displayName.Length > 50)
            {
                ModelState.AddModelError("", "Display name must be between 1 and 50 characters.");
                return View();
            }

            var userId = _userManager.GetUserId(User)!;
            var success = await _userService.UpdateDisplayNameAsync(userId, displayName);

            if (success)
            {
                TempData["Success"] = "Display name updated successfully!";
                return RedirectToAction(nameof(Profile));
            }

            ModelState.AddModelError("", "Failed to update display name. Please try again.");
            return View();
        }
    }
}