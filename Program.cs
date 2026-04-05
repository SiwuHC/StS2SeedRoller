using System.Collections.Concurrent;
using System.Diagnostics;
using StS2SeedRoller.Core;
using StS2SeedRoller.Data;
using StS2SeedRoller.Simulation;

namespace StS2SeedRoller;

class Program
{
    static int Main(string[] args)
    {
        if (args.Length == 0 || args.Contains("--help") || args.Contains("-h"))
        {
            PrintUsage();
            return 0;
        }

        // Parse arguments
        string? targetRelic = GetArg(args, "--relic") ?? GetArg(args, "-r");
        string? characterName = GetArg(args, "--character") ?? GetArg(args, "-c") ?? "Ironclad";
        string? checkSeed = GetArg(args, "--check");
        int threadCount = int.TryParse(GetArg(args, "--threads") ?? GetArg(args, "-t"), out var t) ? t : Environment.ProcessorCount;
        int maxResults = int.TryParse(GetArg(args, "--count") ?? GetArg(args, "-n"), out var n) ? n : 1;

        // Parse character
        if (!Enum.TryParse<Character>(characterName, true, out var character))
        {
            Console.Error.WriteLine($"Unknown character: {characterName}");
            Console.Error.WriteLine($"Available: {string.Join(", ", Enum.GetNames<Character>())}");
            return 1;
        }

        // Single seed check mode
        if (checkSeed != null)
        {
            return CheckSingleSeed(checkSeed, character, targetRelic);
        }

        // Seed search mode
        if (targetRelic == null)
        {
            Console.Error.WriteLine("Error: --relic is required for search mode.");
            PrintUsage();
            return 1;
        }

        // Validate relic name
        var relicInfo = RelicData.FindByName(targetRelic);
        if (relicInfo == null)
        {
            Console.Error.WriteLine($"Unknown relic: {targetRelic}");
            Console.Error.WriteLine("Available relics for grab bag:");
            foreach (var name in RelicData.GetAllGrabBagRelicNames(character))
                Console.Error.WriteLine($"  {name}");
            return 1;
        }

        Console.WriteLine($"Searching for seed with [{relicInfo.Name}] ({relicInfo.Rarity}) from Neow capsule");
        Console.WriteLine($"Character: {character}, Threads: {threadCount}, Target results: {maxResults}");
        Console.WriteLine();

        SearchSeeds(relicInfo.Name, character, threadCount, maxResults);
        return 0;
    }

    static int CheckSingleSeed(string seed, Character character, string? targetRelic)
    {
        var result = SeedEvaluator.Evaluate(seed, character, targetRelic ?? "");
        PrintResult(result);
        return 0;
    }

    static void SearchSeeds(string targetRelic, Character character, int threadCount, int maxResults)
    {
        var sw = Stopwatch.StartNew();
        long totalChecked = 0;
        int foundCount = 0;
        var cts = new CancellationTokenSource();

        var tasks = new Task[threadCount];
        for (int i = 0; i < threadCount; i++)
        {
            int threadId = i;
            tasks[i] = Task.Run(() =>
            {
                var random = new Random(threadId * 31337 + Environment.TickCount);
                while (!cts.Token.IsCancellationRequested)
                {
                    string seed = SeedHelper.GenerateRandomSeed(random);
                    var result = SeedEvaluator.Evaluate(seed, character, targetRelic);

                    long checked_ = Interlocked.Increment(ref totalChecked);

                    if (result.HasTargetRelic)
                    {
                        int count = Interlocked.Increment(ref foundCount);
                        lock (Console.Out)
                        {
                            PrintResult(result);
                        }
                        if (count >= maxResults)
                        {
                            cts.Cancel();
                            return;
                        }
                    }

                    // Progress report every 100k seeds
                    if (checked_ % 100000 == 0)
                    {
                        double elapsed = sw.Elapsed.TotalSeconds;
                        double rate = checked_ / elapsed;
                        Console.Error.Write($"\r  Checked: {checked_:N0} seeds | {rate:N0}/s | Elapsed: {sw.Elapsed:mm\\:ss}    ");
                    }
                }
            }, cts.Token);
        }

        try
        {
            Task.WaitAll(tasks);
        }
        catch (AggregateException) { }

        Console.Error.WriteLine();
        Console.Error.WriteLine($"Done. Checked {Interlocked.Read(ref totalChecked):N0} seeds in {sw.Elapsed:mm\\:ss\\.ff}");
    }

    static void PrintResult(SeedResult result)
    {
        Console.WriteLine($"========================================");
        Console.WriteLine($"  Seed: {result.Seed}");
        Console.WriteLine($"  Character: {result.Character}");
        Console.WriteLine($"  Neow Options:");
        foreach (var opt in result.NeowOptions)
        {
            string marker = opt == result.CapsuleOption ? " <--" : "";
            string type = opt.IsCurse ? "(curse)" : "(positive)";
            Console.WriteLine($"    - {opt.RelicName} {type}{marker}");
        }
        if (result.CapsuleOption != null)
        {
            string capsuleType = result.CapsuleOption.RelicName == "SmallCapsule"
                ? "1 relic" : "2 relics + Strike + Defend";
            Console.WriteLine($"  Capsule: {result.CapsuleOption.RelicName} ({capsuleType})");
            Console.WriteLine($"  Obtained relics: {string.Join(", ", result.ObtainedRelics)}");
        }
        else
        {
            Console.WriteLine($"  No capsule option available.");
        }
        Console.WriteLine($"========================================");
        Console.WriteLine();
    }

    static void PrintUsage()
    {
        Console.WriteLine("StS2 Seed Roller - Find seeds with specific Neow relic rewards");
        Console.WriteLine();
        Console.WriteLine("Usage:");
        Console.WriteLine("  Search:  StS2SeedRoller --relic <name> [--character <char>] [--threads <n>] [--count <n>]");
        Console.WriteLine("  Check:   StS2SeedRoller --check <seed> [--character <char>] [--relic <name>]");
        Console.WriteLine();
        Console.WriteLine("Options:");
        Console.WriteLine("  -r, --relic <name>      Target relic name (e.g., Kunai)");
        Console.WriteLine("  -c, --character <name>  Character: Ironclad, Silent, Defect, Necrobinder, Regent (default: Ironclad)");
        Console.WriteLine("  -t, --threads <n>       Thread count (default: CPU cores)");
        Console.WriteLine("  -n, --count <n>         Number of results to find (default: 1)");
        Console.WriteLine("  --check <seed>          Check a specific seed instead of searching");
        Console.WriteLine();
        Console.WriteLine("The tool finds seeds where Neow offers SmallCapsule (1 relic) or");
        Console.WriteLine("LargeCapsule (2 relics + Strike + Defend), and the obtained relics");
        Console.WriteLine("include the target relic.");
    }

    static string? GetArg(string[] args, string key)
    {
        for (int i = 0; i < args.Length - 1; i++)
        {
            if (args[i] == key)
                return args[i + 1];
        }
        return null;
    }
}
