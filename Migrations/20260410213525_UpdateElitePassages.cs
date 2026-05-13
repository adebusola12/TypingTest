using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TypingTest.Migrations
{
    /// <inheritdoc />
    public partial class UpdateElitePassages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WordPassages",
                columns: new[] { "Id", "Content", "Difficulty", "IsActive", "Stage", "Title", "WordCount" },
                values: new object[,]
                {
                    { 22, "cryptographic algorithms rely on computational complexity to ensure that unauthorized decryption remains mathematically infeasible even with significant processing power distributed across multiple high performance systems simultaneously", 2, true, 8, "The Elite Path", 28 },
                    { 23, "electromagnetic interference can significantly degrade signal integrity in high frequency transmission lines requiring careful impedance matching and shielding techniques to maintain acceptable bit error rates across extended communication channels", 2, true, 8, "Controlled Power", 30 },
                    { 24, "neuroplasticity demonstrates that sustained cognitive engagement fundamentally restructures synaptic pathways enabling individuals to acquire sophisticated competencies that initially appeared incomprehensible through deliberate incremental exposure and reinforcement", 2, true, 8, "No Shortcuts", 27 },
                    { 25, "the thermodynamic irreversibility of spontaneous processes is quantified through entropic calculations that reveal the fundamental asymmetry between forward and reverse reactions under equilibrium conditions demonstrating why perpetual motion machines violate established physical principles unconditionally", 2, true, 9, "Legendary Focus", 35 },
                    { 26, "Byzantine fault tolerance in distributed computing systems requires sophisticated consensus mechanisms capable of maintaining operational integrity even when a significant proportion of participating nodes behave arbitrarily maliciously or transmit contradictory information to different recipients simultaneously", 2, true, 9, "Beyond Limits", 36 },
                    { 27, "pharmacokinetic variability among patient populations complicates standardized dosing protocols requiring clinicians to account for polymorphic metabolic enzymes hepatic function renal clearance rates and potential drug interactions that collectively influence bioavailability and therapeutic efficacy unpredictably", 2, true, 9, "The Long Road", 34 },
                    { 28, "stochastic gradient descent optimization algorithms navigate high dimensional non convex loss landscapes by iteratively adjusting parameter vectors in directions that minimize empirical risk while regularization techniques simultaneously constrain model complexity to prevent overfitting on finite training distributions with limited representational diversity", 2, true, 10, "Grandmaster", 40 },
                    { 29, "quaternionic representations extend complex number theory into four dimensional algebras enabling compact parameterization of three dimensional rotational transformations that avoid gimbal lock singularities inherent in conventional Euler angle decompositions widely exploited in aerospace navigation and robotic kinematic chain computations", 2, true, 10, "The Final Form", 40 },
                    { 30, "epigenetic modifications including cytosine methylation and histone acetylation dynamically regulate transcriptional accessibility without altering underlying nucleotide sequences demonstrating that heritable phenotypic variation can emerge through mechanisms entirely independent of conventional Mendelian genetic transmission across successive generations", 2, true, 10, "Beyond the Summit", 38 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 30);
        }
    }
}
