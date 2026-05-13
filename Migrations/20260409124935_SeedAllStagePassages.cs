using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TypingTest.Migrations
{
    /// <inheritdoc />
    public partial class SeedAllStagePassages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Content", "Stage", "Title", "WordCount" },
                values: new object[] { "the cat sat on the mat", 1, "First Steps", 5 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Content", "Difficulty", "Stage", "Title", "WordCount" },
                values: new object[] { "a big red car drove down the road", 0, 1, "Simple Words", 7 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Content", "Difficulty", "Stage", "Title", "WordCount" },
                values: new object[] { "the sun is hot and the sky is blue", 0, 1, "Basic Phrases", 8 });

            migrationBuilder.InsertData(
                table: "WordPassages",
                columns: new[] { "Id", "Content", "Difficulty", "IsActive", "Stage", "Title", "WordCount" },
                values: new object[,]
                {
                    { 4, "the dog ran across the yard and jumped over the fence into the garden", 0, true, 2, "Simple Intro", 13 },
                    { 5, "she woke up early made some tea and sat by the window to watch the birds", 0, true, 2, "Daily Life", 15 },
                    { 6, "the boy kicked the ball and it flew high into the air before landing in the grass", 0, true, 2, "Short Story", 17 },
                    { 7, "the best way to get better at typing is to keep practicing every day even when it feels slow and difficult", 1, true, 3, "Keep Going", 21 },
                    { 8, "when you focus on what you are doing and take your time the words will start to flow more easily from your fingers", 1, true, 3, "Stay Focused", 23 },
                    { 9, "sitting up straight and keeping your eyes on the screen instead of your hands will help you type much faster over time", 1, true, 3, "Good Habits", 22 },
                    { 10, "getting better at anything takes time and effort but if you stick with it and do not give up you will be surprised at how much you can improve in just a few weeks", 1, true, 4, "The Journey", 34 },
                    { 11, "every time you make a mistake it is a chance to learn and do better next time so do not get frustrated just take a breath and try again from the beginning", 1, true, 4, "Keep Trying", 33 },
                    { 12, "the key to typing well under pressure is to stay calm and not rush because when you slow down just a little your accuracy goes up and your overall speed improves too", 1, true, 4, "Stay Calm", 34 },
                    { 13, "a skilled typist does not just type fast they type with control knowing exactly when to slow down to avoid mistakes that would cost them more time to fix than they saved by rushing", 2, true, 5, "Speed and Accuracy", 35 },
                    { 14, "confidence plays a big role in how well you perform under pressure so trust your training keep your posture steady and let your fingers do what they have been trained to do", 2, true, 5, "The Right Mindset", 33 },
                    { 15, "there is a state where typing feels almost effortless where your hands move without thinking and the words appear on the screen as fast as you can read them in your head", 2, true, 5, "Finding Flow", 34 },
                    { 16, "at this level raw speed is no longer the only goal what matters now is the ability to maintain high accuracy over longer passages without losing focus or letting small errors break your rhythm and slow you down", 2, true, 6, "Beyond Speed", 40 },
                    { 17, "after thousands of hours of practice your fingers begin to know where every key is without you having to think about it and that is when typing becomes less of a skill and more of a reflex that never fades", 2, true, 6, "Muscle Memory", 42 },
                    { 18, "becoming truly great at typing is not about talent it is about showing up every day putting in the work and trusting that the small improvements you make each session will add up to something remarkable over time", 2, true, 6, "The Long Game", 40 },
                    { 19, "you have come a long way from where you started and every stage you cleared was proof that you had what it takes to keep going even when it was hard and now standing at the top you can look back and see just how far your fingers have carried you", 2, true, 7, "The Summit", 52 },
                    { 20, "mastery is not a destination you reach and then stop at it is a way of moving through the world with care and precision doing the small things right every time because you know that the small things are what everything else is built on", 2, true, 7, "Mastery Defined", 46 },
                    { 21, "this is where everything you have practiced comes together your speed your accuracy your focus and your patience all working as one so take a deep breath put your fingers on the keys and show the world what you are made of because you have earned this moment", 2, true, 7, "The Final Test", 49 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Content", "Stage", "Title", "WordCount" },
                values: new object[] { "the quick brown fox jumps over the lazy dog and runs away into the forest", 0, "Simple intro", 14 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Content", "Difficulty", "Stage", "Title", "WordCount" },
                values: new object[] { "practice makes perfect and the best way to improve your typing speed is to type every single day without stopping", 1, 0, "Common words", 21 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Content", "Difficulty", "Stage", "Title", "WordCount" },
                values: new object[] { "asynchronous programming allows multiple operations to execute concurrently without blocking the main thread improving application responsiveness and throughput", 2, 0, "Technical prose", 19 });
        }
    }
}
