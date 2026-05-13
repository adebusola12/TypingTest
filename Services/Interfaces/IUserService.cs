using TypingTest.Models;
using TypingTest.ViewModels;
namespace TypingTest.Services.Interfaces
{
    public interface IUserService
    {
        // Get a user's display name by their ID
        Task<string> GetDisplayNameAsync(string userId);

        // Update a user's display name
        Task<bool> UpdateDisplayNameAsync(string userId, string displayName);

        // Get a user's full test history with stats
        Task<UserStatsViewModel> GetUserStatsAsync(string userId);
    }
}
