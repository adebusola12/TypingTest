namespace TypingTest.Services
{
    public class WordDropService
    {
        // Number of waves a player must clear to complete a warm-up session
        public const int WarmupWavesRequired = 2;

        // Number of consecutive stage failures before the Word Drop prompt appears
        public const int FailsBeforePrompt = 2;

        // Starting lives in every Word Drop session
        public const int StartingLives = 3;
    }
}