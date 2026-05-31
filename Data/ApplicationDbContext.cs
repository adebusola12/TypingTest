using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TypingTest.Models;

namespace TypingTest.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<TestResult> TestResults => Set<TestResult>();
        public DbSet<WordPassage> WordPassages => Set<WordPassage>();
        public DbSet<GameScore> GameScores => Set<GameScore>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // TestResult configuration
            builder.Entity<TestResult>(e =>
            {
                e.HasOne(t => t.User)
                 .WithMany(u => u.TestResults)
                 .HasForeignKey(t => t.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(t => t.WordPassage)
                 .WithMany(p => p.TestResults)
                 .HasForeignKey(t => t.WordPassageId)
                 .OnDelete(DeleteBehavior.Restrict);

                e.Property(t => t.Accuracy).HasPrecision(5, 2);

                e.HasIndex(t => new { t.Wpm, t.CompletedAt });
                e.HasIndex(t => t.UserId);
            });

            // WordPassage configuration
            builder.Entity<WordPassage>(e =>
            {
                e.HasIndex(p => p.Difficulty);
            });

            //GameScore configuration
            builder.Entity<GameScore>(e =>
            {
                e.HasOne(g => g.User)
                 .WithMany()
                 .HasForeignKey(g => g.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

                e.HasIndex(g => new { g.GameType, g.Score });
                e.HasIndex(g => g.UserId);
                e.HasIndex(g => g.PlayedAt);
            });

            builder.Entity<WordPassage>().HasData(

                // ── Stage 1 — Novice (Easy, 15s) ─────────────────────────
                // Short 5–8 word passages, very simple vocabulary
                new WordPassage { Id = 1, Title = "First Steps", Content = "the cat sat on the mat", Difficulty = DifficultyLevel.Easy, Stage = 1, WordCount = 5, IsActive = true },
                new WordPassage { Id = 2, Title = "Simple Words", Content = "a big red car drove down the road", Difficulty = DifficultyLevel.Easy, Stage = 1, WordCount = 7, IsActive = true },
                new WordPassage { Id = 3, Title = "Basic Phrases", Content = "the sun is hot and the sky is blue", Difficulty = DifficultyLevel.Easy, Stage = 1, WordCount = 8, IsActive = true },

                // ── Stage 2 — Beginner (Easy, 20s) ───────────────────────
                // 10–14 word passages
                new WordPassage { Id = 4, Title = "Morning Walk", Content = "the dog ran across the yard and jumped over the low fence", Difficulty = DifficultyLevel.Easy, Stage = 2, WordCount = 12, IsActive = true },
                new WordPassage { Id = 5, Title = "Daily Life", Content = "she woke up early made some tea and sat by the window", Difficulty = DifficultyLevel.Easy, Stage = 2, WordCount = 12, IsActive = true },
                new WordPassage { Id = 6, Title = "After School", Content = "the boy kicked the ball and it flew high into the air", Difficulty = DifficultyLevel.Easy, Stage = 2, WordCount = 12, IsActive = true },

                // ── Stage 3 — Apprentice (Easy, 30s) ─────────────────────
                // 16–20 word passages
                new WordPassage { Id = 7, Title = "Keep Going", Content = "the best way to get better at typing is to keep practicing every single day even when it feels slow", Difficulty = DifficultyLevel.Easy, Stage = 3, WordCount = 18, IsActive = true },
                new WordPassage { Id = 8, Title = "Stay Focused", Content = "when you focus on what you are doing and take your time the words will start to flow more easily", Difficulty = DifficultyLevel.Easy, Stage = 3, WordCount = 18, IsActive = true },
                new WordPassage { Id = 9, Title = "Good Habits", Content = "sitting up straight and keeping your eyes on the screen instead of your hands will help you type faster", Difficulty = DifficultyLevel.Easy, Stage = 3, WordCount = 17, IsActive = true },

                // ── Stage 4 — Rising (Easy, 35s) ─────────────────────────
                // 22–26 word passages
                new WordPassage { Id = 10, Title = "The Journey", Content = "getting better at anything takes time and effort but if you stick with it and do not give up you will be surprised at how much you can improve", Difficulty = DifficultyLevel.Easy, Stage = 4, WordCount = 25, IsActive = true },
                new WordPassage { Id = 11, Title = "Keep Trying", Content = "every time you make a mistake it is a chance to learn and do better next time so do not get frustrated just take a breath and try again", Difficulty = DifficultyLevel.Easy, Stage = 4, WordCount = 25, IsActive = true },
                new WordPassage { Id = 12, Title = "Stay Calm", Content = "the key to typing well under pressure is to stay calm and not rush because when you slow down a little your accuracy goes up and your speed improves too", Difficulty = DifficultyLevel.Easy, Stage = 4, WordCount = 26, IsActive = true },

                // ── Stage 5 — Skilled (Medium, 45s) ──────────────────────
                // 28–32 word passages, slightly more complex vocabulary
                new WordPassage { Id = 13, Title = "Speed and Accuracy", Content = "a skilled typist does not just type fast they type with control knowing exactly when to slow down to avoid mistakes that would cost them more time to fix than they saved by rushing through the passage", Difficulty = DifficultyLevel.Medium, Stage = 5, WordCount = 32, IsActive = true },
                new WordPassage { Id = 14, Title = "The Right Mindset", Content = "confidence plays a big role in how well you perform under pressure so trust your training keep your posture steady and let your fingers do what they have been trained to do without hesitation", Difficulty = DifficultyLevel.Medium, Stage = 5, WordCount = 31, IsActive = true },
                new WordPassage { Id = 15, Title = "Finding Flow", Content = "there is a state where typing feels almost effortless where your hands move without thinking and the words appear on the screen as fast as you can read them in your head and that is what you are aiming for", Difficulty = DifficultyLevel.Medium, Stage = 5, WordCount = 35, IsActive = true },

                // ── Stage 6 — Adept (Medium, 60s) ────────────────────────
                // 36–42 word passages
                new WordPassage { Id = 16, Title = "Beyond Speed", Content = "at this level raw speed is no longer the only goal what matters now is the ability to maintain high accuracy over longer passages without losing focus or letting small errors break your rhythm and slow you down considerably", Difficulty = DifficultyLevel.Medium, Stage = 6, WordCount = 38, IsActive = true },
                new WordPassage { Id = 17, Title = "Muscle Memory", Content = "after thousands of repetitions your fingers begin to know where every key is without you having to think about it and that is when typing becomes less of a skill and more of a reflex that never fades no matter how much time passes", Difficulty = DifficultyLevel.Medium, Stage = 6, WordCount = 40, IsActive = true },
                new WordPassage { Id = 18, Title = "The Long Game", Content = "becoming truly great at typing is not about talent it is about showing up every day putting in the work and trusting that the small improvements you make each session will add up to something remarkable over a long period of time", Difficulty = DifficultyLevel.Medium, Stage = 6, WordCount = 38, IsActive = true },

                // ── Stage 7 — Capable (Medium, 75s) ──────────────────────
                // 44–50 word passages
                new WordPassage { Id = 19, Title = "The Summit", Content = "you have come a long way from where you started and every stage you cleared was proof that you had what it takes to keep going even when it was hard and now you can look back and see just how far your fingers have carried you on this journey", Difficulty = DifficultyLevel.Medium, Stage = 7, WordCount = 48, IsActive = true },
                new WordPassage { Id = 20, Title = "Mastery Defined", Content = "mastery is not a destination you reach and then stop at it is a way of moving through the world with care and precision doing the small things right every single time because you know deep down that the small things are what everything else is ultimately built upon", Difficulty = DifficultyLevel.Medium, Stage = 7, WordCount = 46, IsActive = true },
                new WordPassage { Id = 21, Title = "Ready to Rise", Content = "this is where everything you have practiced starts to come together your speed your accuracy your focus and your patience all working as one fluid motion so take a deep breath put your fingers on the keys and show the screen what you are made of because you have worked hard to get here", Difficulty = DifficultyLevel.Medium, Stage = 7, WordCount = 50, IsActive = true },

                // ── Stage 8 — Proficient (Medium, 90s) ───────────────────
                // 52–58 word passages, more descriptive language
                new WordPassage { Id = 22, Title = "Consistent Output", Content = "the difference between an average typist and an exceptional one is not found in moments of peak performance but rather in the consistency of output maintained across long sessions when focus begins to waver and the temptation to rush becomes harder to resist with every passing minute of concentrated effort", Difficulty = DifficultyLevel.Medium, Stage = 8, WordCount = 55, IsActive = true },
                new WordPassage { Id = 23, Title = "Under Pressure", Content = "performing well under pressure is a skill that must be developed deliberately through repeated exposure to challenging conditions because the body and mind both need time to learn that the elevated heart rate and heightened focus that come with difficult situations are assets rather than obstacles to be feared", Difficulty = DifficultyLevel.Medium, Stage = 8, WordCount = 54, IsActive = true },
                new WordPassage { Id = 24, Title = "The Process", Content = "there is enormous value in trusting the process even when results are slow to appear because every session spent at the keyboard is quietly building the neural pathways and muscle memory that will eventually allow your fingers to move with a speed and accuracy that once seemed completely out of reach but is now becoming your new normal", Difficulty = DifficultyLevel.Medium, Stage = 8, WordCount = 58, IsActive = true },

                // ── Stage 9 — Expert (Hard, 105s) ────────────────────────
                // 52–58 words, complex vocabulary begins
                new WordPassage { Id = 25, Title = "Computational Thinking", Content = "algorithmic problem solving requires the practitioner to decompose complex challenges into discrete manageable components that can be addressed systematically using established computational patterns while simultaneously evaluating the trade offs between processing efficiency memory utilization and implementation complexity across diverse hardware architectures", Difficulty = DifficultyLevel.Hard, Stage = 9, WordCount = 46, IsActive = true },
                new WordPassage { Id = 26, Title = "Neural Pathways", Content = "neuroplasticity research demonstrates that sustained cognitive engagement fundamentally restructures synaptic connectivity enabling individuals to acquire sophisticated competencies that initially appeared incomprehensible through deliberate incremental exposure combined with consistent reinforcement of newly established neural pathways across extended learning periods", Difficulty = DifficultyLevel.Hard, Stage = 9, WordCount = 44, IsActive = true },
                new WordPassage { Id = 27, Title = "Signal Integrity", Content = "electromagnetic interference can significantly degrade signal integrity in high frequency transmission lines requiring careful impedance matching and comprehensive shielding techniques to maintain acceptable bit error rates across extended communication channels operating in electrically noisy industrial environments with multiple competing interference sources", Difficulty = DifficultyLevel.Hard, Stage = 9, WordCount = 48, IsActive = true },

                // ── Stage 10 — Advanced (Hard, 120s) ─────────────────────
                // 58–65 words
                new WordPassage { Id = 28, Title = "Cryptographic Systems", Content = "modern cryptographic protocols rely on the computational intractability of specific mathematical problems such as integer factorization and discrete logarithms to ensure that unauthorized decryption remains infeasible even when adversaries possess substantial distributed processing resources operating continuously across geographically dispersed server infrastructure for extended periods exceeding several decades", Difficulty = DifficultyLevel.Hard, Stage = 10, WordCount = 56, IsActive = true },
                new WordPassage { Id = 29, Title = "Thermodynamic Laws", Content = "the thermodynamic irreversibility of spontaneous natural processes is rigorously quantified through entropic calculations that reveal the fundamental asymmetry between forward and reverse reactions under equilibrium conditions conclusively demonstrating why perpetual motion machines of both the first and second kind necessarily violate well established physical conservation principles without exception", Difficulty = DifficultyLevel.Hard, Stage = 10, WordCount = 58, IsActive = true },
                new WordPassage { Id = 30, Title = "Distributed Consensus", Content = "Byzantine fault tolerance in distributed computing systems requires sophisticated consensus mechanisms capable of maintaining full operational integrity even when a significant proportion of participating nodes behave arbitrarily maliciously or simultaneously transmit contradictory information to different recipients undermining the reliability guarantees that mission critical infrastructure depends upon absolutely", Difficulty = DifficultyLevel.Hard, Stage = 10, WordCount = 60, IsActive = true },

                // ── Stage 11 — Veteran (Hard, 135s) ──────────────────────
                // 65–72 words
                new WordPassage { Id = 31, Title = "Pharmacokinetics", Content = "pharmacokinetic variability among diverse patient populations significantly complicates standardized therapeutic dosing protocols requiring clinicians to carefully account for polymorphic metabolic enzyme expression hepatic functional capacity renal clearance rates age related physiological changes and potential drug interactions that collectively influence bioavailability and therapeutic efficacy in ways that remain difficult to predict reliably without comprehensive genetic profiling", Difficulty = DifficultyLevel.Hard, Stage = 11, WordCount = 66, IsActive = true },
                new WordPassage { Id = 32, Title = "Quantum Mechanics", Content = "the Copenhagen interpretation of quantum mechanical wave function collapse remains philosophically contentious among theoretical physicists because it implies that observation itself plays a fundamental role in determining physical reality suggesting that unmeasured quantum systems exist in genuine superpositions of mutually exclusive states rather than simply reflecting incomplete knowledge about predetermined classical outcomes that existed independently of measurement", Difficulty = DifficultyLevel.Hard, Stage = 11, WordCount = 68, IsActive = true },
                new WordPassage { Id = 33, Title = "Macroeconomic Policy", Content = "the transmission mechanisms through which central bank monetary policy decisions propagate through complex interconnected financial systems to ultimately influence real economic variables such as employment inflation and productive investment involve numerous intermediate channels including credit availability yield curve dynamics currency valuation expectations formation and wealth effects operating across multiple time horizons simultaneously with varying degrees of predictability", Difficulty = DifficultyLevel.Hard, Stage = 11, WordCount = 70, IsActive = true },

                // ── Stage 12 — Elite (Hard, 150s) ────────────────────────
                // 72–80 words
                new WordPassage { Id = 34, Title = "Epigenetic Inheritance", Content = "epigenetic modifications including cytosine methylation patterns and histone acetylation states dynamically regulate transcriptional accessibility across the genome without altering the underlying nucleotide sequences themselves conclusively demonstrating that heritable phenotypic variation can emerge through molecular mechanisms entirely independent of conventional Mendelian genetic transmission providing compelling evidence that environmental exposures experienced by parents can directly influence gene expression patterns in subsequent generations", Difficulty = DifficultyLevel.Hard, Stage = 12, WordCount = 72, IsActive = true },
                new WordPassage { Id = 35, Title = "Stochastic Optimization", Content = "stochastic gradient descent optimization algorithms navigate extremely high dimensional non convex loss landscapes by iteratively adjusting parameter vectors in directions that approximately minimize empirical risk on randomly sampled data subsets while carefully tuned regularization techniques simultaneously constrain overall model complexity to prevent catastrophic overfitting on finite training distributions that may not adequately represent the full diversity of real world deployment conditions", Difficulty = DifficultyLevel.Hard, Stage = 12, WordCount = 74, IsActive = true },
                new WordPassage { Id = 36, Title = "Evolutionary Biology", Content = "the extended evolutionary synthesis incorporates epigenetic inheritance developmental plasticity niche construction and multilevel selection into the conceptual framework of evolutionary biology recognizing that phenotypic variation can arise through mechanisms operating across multiple biological levels and timescales simultaneously challenging the gene centric perspective that dominated theoretical evolutionary biology throughout the latter half of the twentieth century and continuing to generate productive empirical research programs", Difficulty = DifficultyLevel.Hard, Stage = 12, WordCount = 76, IsActive = true },

                // ── Stage 13 — Master (Hard, 165s) ───────────────────────
                // 80–88 words
                new WordPassage { Id = 37, Title = "Quaternion Algebra", Content = "quaternionic representations extend complex number theory into four dimensional non commutative algebras enabling remarkably compact parameterization of three dimensional rotational transformations that completely avoid the gimbal lock singularities inherent in conventional Euler angle decompositions which are widely exploited in aerospace navigation systems robotic kinematic chain computations computer graphics rendering pipelines and inertial measurement unit fusion algorithms where computational efficiency and numerical stability across all possible orientations are simultaneously critical requirements", Difficulty = DifficultyLevel.Hard, Stage = 13, WordCount = 80, IsActive = true },
                new WordPassage { Id = 38, Title = "Linguistic Relativity", Content = "the Sapir Whorf hypothesis in its strong formulation proposes that the grammatical structures and lexical categories present in a speaker's native language fundamentally constrain the range of thoughts that speaker is capable of forming thereby suggesting that cognitive architecture varies systematically across linguistic communities in ways that go beyond mere surface level differences in expressive convention to reflect genuinely distinct ways of organizing and categorizing perceptual experience of the shared physical world", Difficulty = DifficultyLevel.Hard, Stage = 13, WordCount = 82, IsActive = true },
                new WordPassage { Id = 39, Title = "Information Theory", Content = "Shannon entropy provides a rigorous mathematical framework for quantifying the average information content of messages drawn from probabilistic source distributions enabling precise theoretical bounds on lossless data compression ratios and establishing fundamental capacity limits for reliable communication across noisy transmission channels that hold regardless of the specific encoding schemes or error correction strategies employed by system designers working within the constraints imposed by the channel's statistical characteristics", Difficulty = DifficultyLevel.Hard, Stage = 13, WordCount = 84, IsActive = true },

                // ── Stage 14 — Legend (Hard, 180s) ───────────────────────
                // 88–96 words
                new WordPassage { Id = 40, Title = "Consciousness Studies", Content = "the hard problem of consciousness as articulated by David Chalmers draws a fundamental distinction between the relatively tractable functional and behavioral aspects of mental life that neuroscience is progressively explaining through mechanistic accounts of neural information processing and the genuinely mysterious explanatory gap that separates any complete physical description of brain activity from the subjective phenomenal qualities of conscious experience the intrinsic felt character of sensations emotions and thoughts that seems to resist reduction to purely objective third person scientific description however detailed and comprehensive", Difficulty = DifficultyLevel.Hard, Stage = 14, WordCount = 90, IsActive = true },
                new WordPassage { Id = 41, Title = "Topology and Space", Content = "the Poincare conjecture which was finally resolved by Grigori Perelman through his application of Richard Hamilton's Ricci flow with surgery technique asserts that any closed three dimensional manifold in which every loop can be continuously contracted to a single point is necessarily topologically equivalent to the standard three sphere representing a profound insight into the relationship between local geometric constraints and global topological structure that has far reaching implications for our mathematical understanding of the possible shapes of the physical universe at cosmological scales", Difficulty = DifficultyLevel.Hard, Stage = 14, WordCount = 92, IsActive = true },
                new WordPassage { Id = 42, Title = "Immune System Dynamics", Content = "the adaptive immune system achieves its remarkable capacity for highly specific pathogen recognition through a stochastic process of somatic recombination that generates an astronomically large repertoire of antigen binding receptor configurations during lymphocyte development followed by clonal selection mechanisms that amplify cells bearing receptors with sufficient affinity for encountered pathogens while simultaneously establishing immunological memory populations that enable dramatically accelerated and intensified secondary responses upon subsequent exposure to the same or closely related antigenic determinants", Difficulty = DifficultyLevel.Hard, Stage = 14, WordCount = 88, IsActive = true },

                // ── Stage 15 — Grandmaster (Hard, 210s) ──────────────────
                // 96–110 words, maximum complexity
                new WordPassage { Id = 43, Title = "General Relativity", Content = "Einstein's general theory of relativity reconceptualizes gravitational interaction not as a conventional force acting instantaneously across distance in the manner of Newtonian mechanics but rather as a manifestation of the curvature of four dimensional spacetime geometry induced by the presence of mass and energy distributions with freely falling objects following geodesic paths through this curved manifold that appear as accelerated trajectories when described from the perspective of non inertial reference frames anchored to massive bodies thereby unifying the previously separate phenomena of inertia and gravitation within a single elegant geometric framework that has withstood every experimental test devised over more than a century", Difficulty = DifficultyLevel.Hard, Stage = 15, WordCount = 104, IsActive = true },
                new WordPassage { Id = 44, Title = "Complexity Theory", Content = "the P versus NP problem which stands as arguably the most consequential unresolved question in theoretical computer science asks whether every computational decision problem whose solutions can be verified in polynomial time by a deterministic Turing machine can also be solved in polynomial time by such a machine with the widespread belief that P does not equal NP reflecting the empirical observation that many practically important optimization and search problems appear to require exponentially growing computational resources as input size increases despite the fact that proposed solutions to specific instances can be checked with remarkable efficiency suggesting a profound and fundamental asymmetry between solution generation and solution verification", Difficulty = DifficultyLevel.Hard, Stage = 15, WordCount = 106, IsActive = true },
                new WordPassage { Id = 45, Title = "Philosophy of Science", Content = "Karl Popper's criterion of falsifiability as the demarcation principle separating genuinely scientific theories from metaphysical speculation has proven enormously influential in shaping how scientists and philosophers understand the epistemological foundations of empirical inquiry yet the criterion itself faces serious philosophical challenges from the Duhem Quine thesis which demonstrates that individual theoretical hypotheses cannot be tested in isolation from the auxiliary assumptions that must be invoked to derive observable predictions meaning that experimental disconfirmation always leaves open the possibility of preserving a favored central hypothesis by revising peripheral background assumptions rather than accepting refutation", Difficulty = DifficultyLevel.Hard, Stage = 15, WordCount = 108, IsActive = true }
            );
        }
    }
}
