using StS2SeedRoller.Core;

namespace StS2SeedRoller.Simulation;

/// <summary>
/// Represents one of the three Neow options shown to the player.
/// </summary>
public record NeowOption(string RelicName, bool IsCurse);

/// <summary>
/// Simulates Neow's GenerateInitialOptions() logic exactly as the game does it.
/// </summary>
public static class NeowSimulator
{
    // Positive options (exact order from game source)
    private static readonly string[] PositiveOptions =
    {
        "ArcaneScroll",
        "BoomingConch",
        "GoldenPearl",
        "LeadPaperweight",
        "LostCoffer",
        "NeowsTorment",
        "NewLeaf",
        "PreciseScissors",
    };

    // Curse options (exact order from game source)
    private static readonly string[] CurseOptions =
    {
        "CursedPearl",
        "HeftyTablet",
        "LargeCapsule",
        "LeafyPoultice",
        "PrecariousShears",
    };

    // Additional options that are conditionally added
    private const string ScrollBoxes = "ScrollBoxes";
    private const string SilverCrucible = "SilverCrucible";
    private const string LavaRock = "LavaRock";
    private const string SmallCapsule = "SmallCapsule";
    private const string NutritiousOyster = "NutritiousOyster";
    private const string StoneHumidifier = "StoneHumidifier";
    private const string NeowsTalisman = "NeowsTalisman";
    private const string Pomander = "Pomander";
    private const string MassiveScroll = "MassiveScroll";

    /// <summary>
    /// Simulate Neow option generation for a given seed and character.
    /// Returns exactly 3 options: 2 positive + 1 curse.
    ///
    /// We assume: single player, no modifiers, all unlocked, ScrollBoxes can always generate bundles.
    /// </summary>
    public static NeowOption[] GenerateOptions(Rng neowRng, bool isSinglePlayer = true)
    {
        // Step 1: Build curse list and pick one
        var curseList = new List<string>(CurseOptions);

        // ScrollBoxes: always added (assumes CanGenerateBundles = true for all characters)
        curseList.Add(ScrollBoxes);

        // SilverCrucible: only in single player
        if (isSinglePlayer)
            curseList.Add(SilverCrucible);

        // Pick one curse option
        var chosenCurse = neowRng.NextItem((IReadOnlyList<string>)curseList)!;

        // Step 2: Build positive list with filtering
        var positiveList = new List<string>(PositiveOptions);

        // Remove conflicting pairs based on chosen curse
        if (chosenCurse == "CursedPearl")
            positiveList.RemoveAll(o => o == "GoldenPearl");
        if (chosenCurse == "HeftyTablet")
            positiveList.RemoveAll(o => o == "ArcaneScroll");
        if (chosenCurse == "LeafyPoultice")
            positiveList.RemoveAll(o => o == "NewLeaf");
        if (chosenCurse == "PrecariousShears")
            positiveList.RemoveAll(o => o == "PreciseScissors");

        // Step 3: Conditionally add extra positive options
        if (chosenCurse != "LargeCapsule")
        {
            if (neowRng.NextBool())
                positiveList.Add(LavaRock);
            else
                positiveList.Add(SmallCapsule);
        }

        if (neowRng.NextBool())
            positiveList.Add(NutritiousOyster);
        else
            positiveList.Add(StoneHumidifier);

        if (neowRng.NextBool())
            positiveList.Add(NeowsTalisman);
        else
            positiveList.Add(Pomander);

        // MassiveScroll only in multiplayer
        if (!isSinglePlayer)
            positiveList.Add(MassiveScroll);

        // Step 4: Shuffle positive list and take 2
        UnstableShuffle(positiveList, neowRng);
        var chosen1 = positiveList[0];
        var chosen2 = positiveList[1];

        // Result: 2 positive options + 1 curse option
        return new NeowOption[]
        {
            new(chosen1, false),
            new(chosen2, false),
            new(chosenCurse, true),
        };
    }

    private static void UnstableShuffle<T>(List<T> list, Rng rng)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.NextInt(n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }
}
