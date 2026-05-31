using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TypingTest.Migrations
{
    /// <inheritdoc />
    public partial class addfailedattempts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FailedAttempts",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Content", "Title", "WordCount" },
                values: new object[] { "the dog ran across the yard and jumped over the low fence", "Morning Walk", 12 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Content", "WordCount" },
                values: new object[] { "she woke up early made some tea and sat by the window", 12 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Content", "Title", "WordCount" },
                values: new object[] { "the boy kicked the ball and it flew high into the air", "After School", 12 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "the best way to get better at typing is to keep practicing every single day even when it feels slow", 0, 18 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "when you focus on what you are doing and take your time the words will start to flow more easily", 0, 18 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "sitting up straight and keeping your eyes on the screen instead of your hands will help you type faster", 0, 17 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "getting better at anything takes time and effort but if you stick with it and do not give up you will be surprised at how much you can improve", 0, 25 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "every time you make a mistake it is a chance to learn and do better next time so do not get frustrated just take a breath and try again", 0, 25 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "the key to typing well under pressure is to stay calm and not rush because when you slow down a little your accuracy goes up and your speed improves too", 0, 26 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "a skilled typist does not just type fast they type with control knowing exactly when to slow down to avoid mistakes that would cost them more time to fix than they saved by rushing through the passage", 1, 32 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "confidence plays a big role in how well you perform under pressure so trust your training keep your posture steady and let your fingers do what they have been trained to do without hesitation", 1, 31 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "there is a state where typing feels almost effortless where your hands move without thinking and the words appear on the screen as fast as you can read them in your head and that is what you are aiming for", 1, 35 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "at this level raw speed is no longer the only goal what matters now is the ability to maintain high accuracy over longer passages without losing focus or letting small errors break your rhythm and slow you down considerably", 1, 38 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "after thousands of repetitions your fingers begin to know where every key is without you having to think about it and that is when typing becomes less of a skill and more of a reflex that never fades no matter how much time passes", 1, 40 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "becoming truly great at typing is not about talent it is about showing up every day putting in the work and trusting that the small improvements you make each session will add up to something remarkable over a long period of time", 1, 38 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "you have come a long way from where you started and every stage you cleared was proof that you had what it takes to keep going even when it was hard and now you can look back and see just how far your fingers have carried you on this journey", 1, 48 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Content", "Difficulty" },
                values: new object[] { "mastery is not a destination you reach and then stop at it is a way of moving through the world with care and precision doing the small things right every single time because you know deep down that the small things are what everything else is ultimately built upon", 1 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Content", "Difficulty", "Title", "WordCount" },
                values: new object[] { "this is where everything you have practiced starts to come together your speed your accuracy your focus and your patience all working as one fluid motion so take a deep breath put your fingers on the keys and show the screen what you are made of because you have worked hard to get here", 1, "Ready to Rise", 50 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Content", "Difficulty", "Title", "WordCount" },
                values: new object[] { "the difference between an average typist and an exceptional one is not found in moments of peak performance but rather in the consistency of output maintained across long sessions when focus begins to waver and the temptation to rush becomes harder to resist with every passing minute of concentrated effort", 1, "Consistent Output", 55 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Content", "Difficulty", "Title", "WordCount" },
                values: new object[] { "performing well under pressure is a skill that must be developed deliberately through repeated exposure to challenging conditions because the body and mind both need time to learn that the elevated heart rate and heightened focus that come with difficult situations are assets rather than obstacles to be feared", 1, "Under Pressure", 54 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Content", "Difficulty", "Title", "WordCount" },
                values: new object[] { "there is enormous value in trusting the process even when results are slow to appear because every session spent at the keyboard is quietly building the neural pathways and muscle memory that will eventually allow your fingers to move with a speed and accuracy that once seemed completely out of reach but is now becoming your new normal", 1, "The Process", 58 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Content", "Title", "WordCount" },
                values: new object[] { "algorithmic problem solving requires the practitioner to decompose complex challenges into discrete manageable components that can be addressed systematically using established computational patterns while simultaneously evaluating the trade offs between processing efficiency memory utilization and implementation complexity across diverse hardware architectures", "Computational Thinking", 46 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Content", "Title", "WordCount" },
                values: new object[] { "neuroplasticity research demonstrates that sustained cognitive engagement fundamentally restructures synaptic connectivity enabling individuals to acquire sophisticated competencies that initially appeared incomprehensible through deliberate incremental exposure combined with consistent reinforcement of newly established neural pathways across extended learning periods", "Neural Pathways", 44 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Content", "Title", "WordCount" },
                values: new object[] { "electromagnetic interference can significantly degrade signal integrity in high frequency transmission lines requiring careful impedance matching and comprehensive shielding techniques to maintain acceptable bit error rates across extended communication channels operating in electrically noisy industrial environments with multiple competing interference sources", "Signal Integrity", 48 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Content", "Title", "WordCount" },
                values: new object[] { "modern cryptographic protocols rely on the computational intractability of specific mathematical problems such as integer factorization and discrete logarithms to ensure that unauthorized decryption remains infeasible even when adversaries possess substantial distributed processing resources operating continuously across geographically dispersed server infrastructure for extended periods exceeding several decades", "Cryptographic Systems", 56 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Content", "Title", "WordCount" },
                values: new object[] { "the thermodynamic irreversibility of spontaneous natural processes is rigorously quantified through entropic calculations that reveal the fundamental asymmetry between forward and reverse reactions under equilibrium conditions conclusively demonstrating why perpetual motion machines of both the first and second kind necessarily violate well established physical conservation principles without exception", "Thermodynamic Laws", 58 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Content", "Title", "WordCount" },
                values: new object[] { "Byzantine fault tolerance in distributed computing systems requires sophisticated consensus mechanisms capable of maintaining full operational integrity even when a significant proportion of participating nodes behave arbitrarily maliciously or simultaneously transmit contradictory information to different recipients undermining the reliability guarantees that mission critical infrastructure depends upon absolutely", "Distributed Consensus", 60 });

            migrationBuilder.InsertData(
                table: "WordPassages",
                columns: new[] { "Id", "Content", "Difficulty", "IsActive", "Stage", "Title", "WordCount" },
                values: new object[,]
                {
                    { 31, "pharmacokinetic variability among diverse patient populations significantly complicates standardized therapeutic dosing protocols requiring clinicians to carefully account for polymorphic metabolic enzyme expression hepatic functional capacity renal clearance rates age related physiological changes and potential drug interactions that collectively influence bioavailability and therapeutic efficacy in ways that remain difficult to predict reliably without comprehensive genetic profiling", 2, true, 11, "Pharmacokinetics", 66 },
                    { 32, "the Copenhagen interpretation of quantum mechanical wave function collapse remains philosophically contentious among theoretical physicists because it implies that observation itself plays a fundamental role in determining physical reality suggesting that unmeasured quantum systems exist in genuine superpositions of mutually exclusive states rather than simply reflecting incomplete knowledge about predetermined classical outcomes that existed independently of measurement", 2, true, 11, "Quantum Mechanics", 68 },
                    { 33, "the transmission mechanisms through which central bank monetary policy decisions propagate through complex interconnected financial systems to ultimately influence real economic variables such as employment inflation and productive investment involve numerous intermediate channels including credit availability yield curve dynamics currency valuation expectations formation and wealth effects operating across multiple time horizons simultaneously with varying degrees of predictability", 2, true, 11, "Macroeconomic Policy", 70 },
                    { 34, "epigenetic modifications including cytosine methylation patterns and histone acetylation states dynamically regulate transcriptional accessibility across the genome without altering the underlying nucleotide sequences themselves conclusively demonstrating that heritable phenotypic variation can emerge through molecular mechanisms entirely independent of conventional Mendelian genetic transmission providing compelling evidence that environmental exposures experienced by parents can directly influence gene expression patterns in subsequent generations", 2, true, 12, "Epigenetic Inheritance", 72 },
                    { 35, "stochastic gradient descent optimization algorithms navigate extremely high dimensional non convex loss landscapes by iteratively adjusting parameter vectors in directions that approximately minimize empirical risk on randomly sampled data subsets while carefully tuned regularization techniques simultaneously constrain overall model complexity to prevent catastrophic overfitting on finite training distributions that may not adequately represent the full diversity of real world deployment conditions", 2, true, 12, "Stochastic Optimization", 74 },
                    { 36, "the extended evolutionary synthesis incorporates epigenetic inheritance developmental plasticity niche construction and multilevel selection into the conceptual framework of evolutionary biology recognizing that phenotypic variation can arise through mechanisms operating across multiple biological levels and timescales simultaneously challenging the gene centric perspective that dominated theoretical evolutionary biology throughout the latter half of the twentieth century and continuing to generate productive empirical research programs", 2, true, 12, "Evolutionary Biology", 76 },
                    { 37, "quaternionic representations extend complex number theory into four dimensional non commutative algebras enabling remarkably compact parameterization of three dimensional rotational transformations that completely avoid the gimbal lock singularities inherent in conventional Euler angle decompositions which are widely exploited in aerospace navigation systems robotic kinematic chain computations computer graphics rendering pipelines and inertial measurement unit fusion algorithms where computational efficiency and numerical stability across all possible orientations are simultaneously critical requirements", 2, true, 13, "Quaternion Algebra", 80 },
                    { 38, "the Sapir Whorf hypothesis in its strong formulation proposes that the grammatical structures and lexical categories present in a speaker's native language fundamentally constrain the range of thoughts that speaker is capable of forming thereby suggesting that cognitive architecture varies systematically across linguistic communities in ways that go beyond mere surface level differences in expressive convention to reflect genuinely distinct ways of organizing and categorizing perceptual experience of the shared physical world", 2, true, 13, "Linguistic Relativity", 82 },
                    { 39, "Shannon entropy provides a rigorous mathematical framework for quantifying the average information content of messages drawn from probabilistic source distributions enabling precise theoretical bounds on lossless data compression ratios and establishing fundamental capacity limits for reliable communication across noisy transmission channels that hold regardless of the specific encoding schemes or error correction strategies employed by system designers working within the constraints imposed by the channel's statistical characteristics", 2, true, 13, "Information Theory", 84 },
                    { 40, "the hard problem of consciousness as articulated by David Chalmers draws a fundamental distinction between the relatively tractable functional and behavioral aspects of mental life that neuroscience is progressively explaining through mechanistic accounts of neural information processing and the genuinely mysterious explanatory gap that separates any complete physical description of brain activity from the subjective phenomenal qualities of conscious experience the intrinsic felt character of sensations emotions and thoughts that seems to resist reduction to purely objective third person scientific description however detailed and comprehensive", 2, true, 14, "Consciousness Studies", 90 },
                    { 41, "the Poincare conjecture which was finally resolved by Grigori Perelman through his application of Richard Hamilton's Ricci flow with surgery technique asserts that any closed three dimensional manifold in which every loop can be continuously contracted to a single point is necessarily topologically equivalent to the standard three sphere representing a profound insight into the relationship between local geometric constraints and global topological structure that has far reaching implications for our mathematical understanding of the possible shapes of the physical universe at cosmological scales", 2, true, 14, "Topology and Space", 92 },
                    { 42, "the adaptive immune system achieves its remarkable capacity for highly specific pathogen recognition through a stochastic process of somatic recombination that generates an astronomically large repertoire of antigen binding receptor configurations during lymphocyte development followed by clonal selection mechanisms that amplify cells bearing receptors with sufficient affinity for encountered pathogens while simultaneously establishing immunological memory populations that enable dramatically accelerated and intensified secondary responses upon subsequent exposure to the same or closely related antigenic determinants", 2, true, 14, "Immune System Dynamics", 88 },
                    { 43, "Einstein's general theory of relativity reconceptualizes gravitational interaction not as a conventional force acting instantaneously across distance in the manner of Newtonian mechanics but rather as a manifestation of the curvature of four dimensional spacetime geometry induced by the presence of mass and energy distributions with freely falling objects following geodesic paths through this curved manifold that appear as accelerated trajectories when described from the perspective of non inertial reference frames anchored to massive bodies thereby unifying the previously separate phenomena of inertia and gravitation within a single elegant geometric framework that has withstood every experimental test devised over more than a century", 2, true, 15, "General Relativity", 104 },
                    { 44, "the P versus NP problem which stands as arguably the most consequential unresolved question in theoretical computer science asks whether every computational decision problem whose solutions can be verified in polynomial time by a deterministic Turing machine can also be solved in polynomial time by such a machine with the widespread belief that P does not equal NP reflecting the empirical observation that many practically important optimization and search problems appear to require exponentially growing computational resources as input size increases despite the fact that proposed solutions to specific instances can be checked with remarkable efficiency suggesting a profound and fundamental asymmetry between solution generation and solution verification", 2, true, 15, "Complexity Theory", 106 },
                    { 45, "Karl Popper's criterion of falsifiability as the demarcation principle separating genuinely scientific theories from metaphysical speculation has proven enormously influential in shaping how scientists and philosophers understand the epistemological foundations of empirical inquiry yet the criterion itself faces serious philosophical challenges from the Duhem Quine thesis which demonstrates that individual theoretical hypotheses cannot be tested in isolation from the auxiliary assumptions that must be invoked to derive observable predictions meaning that experimental disconfirmation always leaves open the possibility of preserving a favored central hypothesis by revising peripheral background assumptions rather than accepting refutation", 2, true, 15, "Philosophy of Science", 108 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DropColumn(
                name: "FailedAttempts",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Content", "Title", "WordCount" },
                values: new object[] { "the dog ran across the yard and jumped over the fence into the garden", "Simple Intro", 13 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Content", "WordCount" },
                values: new object[] { "she woke up early made some tea and sat by the window to watch the birds", 15 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Content", "Title", "WordCount" },
                values: new object[] { "the boy kicked the ball and it flew high into the air before landing in the grass", "Short Story", 17 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "the best way to get better at typing is to keep practicing every day even when it feels slow and difficult", 1, 21 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "when you focus on what you are doing and take your time the words will start to flow more easily from your fingers", 1, 23 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "sitting up straight and keeping your eyes on the screen instead of your hands will help you type much faster over time", 1, 22 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "getting better at anything takes time and effort but if you stick with it and do not give up you will be surprised at how much you can improve in just a few weeks", 1, 34 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "every time you make a mistake it is a chance to learn and do better next time so do not get frustrated just take a breath and try again from the beginning", 1, 33 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "the key to typing well under pressure is to stay calm and not rush because when you slow down just a little your accuracy goes up and your overall speed improves too", 1, 34 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "a skilled typist does not just type fast they type with control knowing exactly when to slow down to avoid mistakes that would cost them more time to fix than they saved by rushing", 2, 35 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "confidence plays a big role in how well you perform under pressure so trust your training keep your posture steady and let your fingers do what they have been trained to do", 2, 33 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "there is a state where typing feels almost effortless where your hands move without thinking and the words appear on the screen as fast as you can read them in your head", 2, 34 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "at this level raw speed is no longer the only goal what matters now is the ability to maintain high accuracy over longer passages without losing focus or letting small errors break your rhythm and slow you down", 2, 40 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "after thousands of hours of practice your fingers begin to know where every key is without you having to think about it and that is when typing becomes less of a skill and more of a reflex that never fades", 2, 42 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "becoming truly great at typing is not about talent it is about showing up every day putting in the work and trusting that the small improvements you make each session will add up to something remarkable over time", 2, 40 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Content", "Difficulty", "WordCount" },
                values: new object[] { "you have come a long way from where you started and every stage you cleared was proof that you had what it takes to keep going even when it was hard and now standing at the top you can look back and see just how far your fingers have carried you", 2, 52 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Content", "Difficulty" },
                values: new object[] { "mastery is not a destination you reach and then stop at it is a way of moving through the world with care and precision doing the small things right every time because you know that the small things are what everything else is built on", 2 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Content", "Difficulty", "Title", "WordCount" },
                values: new object[] { "this is where everything you have practiced comes together your speed your accuracy your focus and your patience all working as one so take a deep breath put your fingers on the keys and show the world what you are made of because you have earned this moment", 2, "The Final Test", 49 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Content", "Difficulty", "Title", "WordCount" },
                values: new object[] { "cryptographic algorithms rely on computational complexity to ensure that unauthorized decryption remains mathematically infeasible even with significant processing power distributed across multiple high performance systems simultaneously", 2, "The Elite Path", 28 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Content", "Difficulty", "Title", "WordCount" },
                values: new object[] { "electromagnetic interference can significantly degrade signal integrity in high frequency transmission lines requiring careful impedance matching and shielding techniques to maintain acceptable bit error rates across extended communication channels", 2, "Controlled Power", 30 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Content", "Difficulty", "Title", "WordCount" },
                values: new object[] { "neuroplasticity demonstrates that sustained cognitive engagement fundamentally restructures synaptic pathways enabling individuals to acquire sophisticated competencies that initially appeared incomprehensible through deliberate incremental exposure and reinforcement", 2, "No Shortcuts", 27 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Content", "Title", "WordCount" },
                values: new object[] { "the thermodynamic irreversibility of spontaneous processes is quantified through entropic calculations that reveal the fundamental asymmetry between forward and reverse reactions under equilibrium conditions demonstrating why perpetual motion machines violate established physical principles unconditionally", "Legendary Focus", 35 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Content", "Title", "WordCount" },
                values: new object[] { "Byzantine fault tolerance in distributed computing systems requires sophisticated consensus mechanisms capable of maintaining operational integrity even when a significant proportion of participating nodes behave arbitrarily maliciously or transmit contradictory information to different recipients simultaneously", "Beyond Limits", 36 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Content", "Title", "WordCount" },
                values: new object[] { "pharmacokinetic variability among patient populations complicates standardized dosing protocols requiring clinicians to account for polymorphic metabolic enzymes hepatic function renal clearance rates and potential drug interactions that collectively influence bioavailability and therapeutic efficacy unpredictably", "The Long Road", 34 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Content", "Title", "WordCount" },
                values: new object[] { "stochastic gradient descent optimization algorithms navigate high dimensional non convex loss landscapes by iteratively adjusting parameter vectors in directions that minimize empirical risk while regularization techniques simultaneously constrain model complexity to prevent overfitting on finite training distributions with limited representational diversity", "Grandmaster", 40 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Content", "Title", "WordCount" },
                values: new object[] { "quaternionic representations extend complex number theory into four dimensional algebras enabling compact parameterization of three dimensional rotational transformations that avoid gimbal lock singularities inherent in conventional Euler angle decompositions widely exploited in aerospace navigation and robotic kinematic chain computations", "The Final Form", 40 });

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Content", "Title", "WordCount" },
                values: new object[] { "epigenetic modifications including cytosine methylation and histone acetylation dynamically regulate transcriptional accessibility without altering underlying nucleotide sequences demonstrating that heritable phenotypic variation can emerge through mechanisms entirely independent of conventional Mendelian genetic transmission across successive generations", "Beyond the Summit", 38 });
        }
    }
}
