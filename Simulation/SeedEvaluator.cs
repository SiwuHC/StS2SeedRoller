using StS2SeedRoller.Core;
using StS2SeedRoller.Data;

namespace StS2SeedRoller.Simulation;

/// <summary>
/// Result of evaluating a seed for Neow relic options.
/// </summary>
public record SeedResult
{
    public required string Seed { get; init; }
    public required Character Character { get; init; }
    public required NeowOption[] NeowOptions { get; init; }
    /// <summary>Which option has SmallCapsule or LargeCapsule (null if neither)</summary>
    public NeowOption? CapsuleOption { get; init; }
    /// <summary>Relics that would be obtained from the capsule</summary>
    public required List<string> ObtainedRelics { get; init; }
    public bool HasTargetRelic { get; init; }
}

/// <summary>
/// Evaluates a seed: does Neow offer SmallCapsule/LargeCapsule, and what relics come out?
/// </summary>
public static class SeedEvaluator
{
    private const ulong SinglePlayerNetId = 1;

    public static SeedResult Evaluate(string seedString, Character character, string targetRelic)
    {
        seedString = SeedHelper.CanonicalizeSeed(seedString);
        uint runSeed = (uint)StringHelper.GetDeterministicHashCode(seedString);

        // ── Step 1: Create UpFront RNG (for relic bag shuffle) ──
        var upFrontRng = new Rng(runSeed, "up_front");

        // ── Step 2: Populate shared relic grab bag ──
        // Game: SharedRelicGrabBag.Populate(IEnumerable) - NO rarity filter, all relics shuffled
        var sharedGrabBag = new RelicGrabBag();
        sharedGrabBag.PopulateUnfiltered(RelicData.GetAllSharedRelics(), upFrontRng);

        // ── Step 3: Populate player relic grab bag ──
        // Uses the SAME UpFront RNG instance (counter continues)
        var playerGrabBag = new RelicGrabBag();
        playerGrabBag.Populate(RelicData.GetGrabBagRelics(character), upFrontRng);

        // ── Step 4: Create Neow event RNG ──
        // EventModel.BeginEvent: Rng = new Rng((uint)(runSeed + netId + hash("NEOW")))
        uint neowSeed = (uint)(runSeed + SinglePlayerNetId +
                               (ulong)StringHelper.GetDeterministicHashCode("NEOW"));
        var neowRng = new Rng(neowSeed);

        // ── Step 5: Generate Neow options ──
        var options = NeowSimulator.GenerateOptions(neowRng);

        // ── Step 6: Check for SmallCapsule or LargeCapsule ──
        NeowOption? capsuleOption = null;
        foreach (var opt in options)
        {
            if (opt.RelicName is "SmallCapsule" or "LargeCapsule")
            {
                capsuleOption = opt;
                break;
            }
        }

        var obtainedRelics = new List<string>();
        bool hasTarget = false;

        if (capsuleOption != null)
        {
            // ── Step 7: Simulate relic pulling ──
            // PlayerRng.Rewards seed = hash(seedString) + netId + hash("rewards")
            uint playerSeed = (uint)((ulong)StringHelper.GetDeterministicHashCode(seedString) + SinglePlayerNetId);
            var rewardsRng = new Rng(playerSeed, "rewards");

            int relicCount = capsuleOption.RelicName == "SmallCapsule" ? 1 : 2;

            for (int i = 0; i < relicCount; i++)
            {
                // RollRarity: use Rewards RNG
                var rarity = RollRarity(rewardsRng);
                // Pull from player's grab bag
                var relic = playerGrabBag.PullFromFront(rarity);
                if (relic != null)
                {
                    obtainedRelics.Add(relic.Name);
                    // Also remove from shared grab bag (game does this)
                    sharedGrabBag.Remove(relic.Name);

                    if (relic.Name.Equals(targetRelic, StringComparison.OrdinalIgnoreCase))
                        hasTarget = true;
                }
            }
        }

        return new SeedResult
        {
            Seed = seedString,
            Character = character,
            NeowOptions = options,
            CapsuleOption = capsuleOption,
            ObtainedRelics = obtainedRelics,
            HasTargetRelic = hasTarget,
        };
    }

    /// <summary>
    /// Exact replica of RelicFactory.RollRarity.
    /// </summary>
    private static RelicRarity RollRarity(Rng rng)
    {
        float roll = rng.NextFloat();
        return (roll < 0.5f) ? RelicRarity.Common : ((!(roll < 0.83f)) ? RelicRarity.Rare : RelicRarity.Uncommon);
    }
}
