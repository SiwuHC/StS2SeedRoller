namespace StS2SeedRoller.Data;

/// <summary>
/// All relic definitions extracted from game source, in exact game order.
/// Only relics with rarity Common/Uncommon/Rare/Shop are included in the grab bag.
/// </summary>
public static class RelicData
{
    // ── SharedRelicPool (exact order from GenerateAllRelics) ──
    // Note: FresnelLens(Event), LoomingFruit(Ancient), VeryHotCocoa(Ancient) are in the pool
    // definition but get filtered out by the grab bag (only Common/Uncommon/Rare/Shop).
    public static readonly RelicInfo[] SharedPool = new RelicInfo[]
    {
        new("Akabeko", RelicRarity.Uncommon, RelicPool.Shared),
        new("AmethystAubergine", RelicRarity.Common, RelicPool.Shared),
        new("Anchor", RelicRarity.Common, RelicPool.Shared),
        new("ArtOfWar", RelicRarity.Rare, RelicPool.Shared),
        new("BagOfMarbles", RelicRarity.Common, RelicPool.Shared),
        new("BagOfPreparation", RelicRarity.Common, RelicPool.Shared),
        new("BeatingRemnant", RelicRarity.Rare, RelicPool.Shared),
        new("Bellows", RelicRarity.Rare, RelicPool.Shared),
        new("BeltBuckle", RelicRarity.Shop, RelicPool.Shared),
        new("BloodVial", RelicRarity.Common, RelicPool.Shared),
        new("BookOfFiveRings", RelicRarity.Common, RelicPool.Shared),
        new("BowlerHat", RelicRarity.Uncommon, RelicPool.Shared),
        new("Bread", RelicRarity.Shop, RelicPool.Shared),
        new("BronzeScales", RelicRarity.Common, RelicPool.Shared),
        new("BurningSticks", RelicRarity.Shop, RelicPool.Shared),
        new("Candelabra", RelicRarity.Uncommon, RelicPool.Shared),
        new("CaptainsWheel", RelicRarity.Rare, RelicPool.Shared),
        new("Cauldron", RelicRarity.Shop, RelicPool.Shared),
        new("CentennialPuzzle", RelicRarity.Common, RelicPool.Shared),
        new("Chandelier", RelicRarity.Rare, RelicPool.Shared),
        new("ChemicalX", RelicRarity.Shop, RelicPool.Shared),
        new("CloakClasp", RelicRarity.Rare, RelicPool.Shared),
        new("DingyRug", RelicRarity.Shop, RelicPool.Shared),
        new("DollysMirror", RelicRarity.Shop, RelicPool.Shared),
        new("DragonFruit", RelicRarity.Shop, RelicPool.Shared),
        new("EternalFeather", RelicRarity.Uncommon, RelicPool.Shared),
        new("FestivePopper", RelicRarity.Common, RelicPool.Shared),
        new("FresnelLens", RelicRarity.Event, RelicPool.Shared),
        new("FrozenEgg", RelicRarity.Rare, RelicPool.Shared),
        new("GamblingChip", RelicRarity.Rare, RelicPool.Shared),
        new("GamePiece", RelicRarity.Rare, RelicPool.Shared),
        new("GhostSeed", RelicRarity.Shop, RelicPool.Shared),
        new("Girya", RelicRarity.Rare, RelicPool.Shared),
        new("GnarledHammer", RelicRarity.Shop, RelicPool.Shared),
        new("Gorget", RelicRarity.Common, RelicPool.Shared),
        new("GremlinHorn", RelicRarity.Uncommon, RelicPool.Shared),
        new("HappyFlower", RelicRarity.Common, RelicPool.Shared),
        new("HornCleat", RelicRarity.Uncommon, RelicPool.Shared),
        new("IceCream", RelicRarity.Rare, RelicPool.Shared),
        new("IntimidatingHelmet", RelicRarity.Rare, RelicPool.Shared),
        new("JossPaper", RelicRarity.Uncommon, RelicPool.Shared),
        new("JuzuBracelet", RelicRarity.Common, RelicPool.Shared),
        new("Kifuda", RelicRarity.Shop, RelicPool.Shared),
        new("Kunai", RelicRarity.Rare, RelicPool.Shared),
        new("Kusarigama", RelicRarity.Uncommon, RelicPool.Shared),
        new("Lantern", RelicRarity.Common, RelicPool.Shared),
        new("LastingCandy", RelicRarity.Uncommon, RelicPool.Shared),
        new("LavaLamp", RelicRarity.Shop, RelicPool.Shared),
        new("LeesWaffle", RelicRarity.Shop, RelicPool.Shared),
        new("LetterOpener", RelicRarity.Uncommon, RelicPool.Shared),
        new("LizardTail", RelicRarity.Rare, RelicPool.Shared),
        new("LoomingFruit", RelicRarity.Ancient, RelicPool.Shared),
        new("LuckyFysh", RelicRarity.Uncommon, RelicPool.Shared),
        new("Mango", RelicRarity.Rare, RelicPool.Shared),
        new("MealTicket", RelicRarity.Common, RelicPool.Shared),
        new("MeatOnTheBone", RelicRarity.Rare, RelicPool.Shared),
        new("MembershipCard", RelicRarity.Shop, RelicPool.Shared),
        new("MercuryHourglass", RelicRarity.Uncommon, RelicPool.Shared),
        new("MiniatureCannon", RelicRarity.Uncommon, RelicPool.Shared),
        new("MiniatureTent", RelicRarity.Shop, RelicPool.Shared),
        new("MoltenEgg", RelicRarity.Rare, RelicPool.Shared),
        new("MummifiedHand", RelicRarity.Rare, RelicPool.Shared),
        new("MysticLighter", RelicRarity.Shop, RelicPool.Shared),
        new("Nunchaku", RelicRarity.Uncommon, RelicPool.Shared),
        new("OddlySmoothStone", RelicRarity.Common, RelicPool.Shared),
        new("OldCoin", RelicRarity.Rare, RelicPool.Shared),
        new("Orichalcum", RelicRarity.Uncommon, RelicPool.Shared),
        new("OrnamentalFan", RelicRarity.Uncommon, RelicPool.Shared),
        new("Orrery", RelicRarity.Shop, RelicPool.Shared),
        new("Pantograph", RelicRarity.Uncommon, RelicPool.Shared),
        new("ParryingShield", RelicRarity.Uncommon, RelicPool.Shared),
        new("Pear", RelicRarity.Uncommon, RelicPool.Shared),
        new("PenNib", RelicRarity.Uncommon, RelicPool.Shared),
        new("Pendulum", RelicRarity.Common, RelicPool.Shared),
        new("Permafrost", RelicRarity.Uncommon, RelicPool.Shared),
        new("PetrifiedToad", RelicRarity.Uncommon, RelicPool.Shared),
        new("Planisphere", RelicRarity.Uncommon, RelicPool.Shared),
        new("Pocketwatch", RelicRarity.Rare, RelicPool.Shared),
        new("PotionBelt", RelicRarity.Common, RelicPool.Shared),
        new("PrayerWheel", RelicRarity.Rare, RelicPool.Shared),
        new("PunchDagger", RelicRarity.Shop, RelicPool.Shared),
        new("RainbowRing", RelicRarity.Rare, RelicPool.Shared),
        new("RazorTooth", RelicRarity.Rare, RelicPool.Shared),
        new("RedMask", RelicRarity.Common, RelicPool.Shared),
        new("RegalPillow", RelicRarity.Common, RelicPool.Shared),
        new("ReptileTrinket", RelicRarity.Uncommon, RelicPool.Shared),
        new("RingingTriangle", RelicRarity.Shop, RelicPool.Shared),
        new("RippleBasin", RelicRarity.Uncommon, RelicPool.Shared),
        new("RoyalStamp", RelicRarity.Shop, RelicPool.Shared),
        new("ScreamingFlagon", RelicRarity.Shop, RelicPool.Shared),
        new("Shovel", RelicRarity.Rare, RelicPool.Shared),
        new("Shuriken", RelicRarity.Rare, RelicPool.Shared),
        new("SlingOfCourage", RelicRarity.Shop, RelicPool.Shared),
        new("SparklingRouge", RelicRarity.Uncommon, RelicPool.Shared),
        new("StoneCalendar", RelicRarity.Rare, RelicPool.Shared),
        new("StoneCracker", RelicRarity.Uncommon, RelicPool.Shared),
        new("Strawberry", RelicRarity.Common, RelicPool.Shared),
        new("StrikeDummy", RelicRarity.Common, RelicPool.Shared),
        new("SturdyClamp", RelicRarity.Rare, RelicPool.Shared),
        new("TheAbacus", RelicRarity.Shop, RelicPool.Shared),
        new("TheCourier", RelicRarity.Rare, RelicPool.Shared),
        new("TinyMailbox", RelicRarity.Uncommon, RelicPool.Shared),
        new("Toolbox", RelicRarity.Shop, RelicPool.Shared),
        new("ToxicEgg", RelicRarity.Rare, RelicPool.Shared),
        new("TungstenRod", RelicRarity.Rare, RelicPool.Shared),
        new("TuningFork", RelicRarity.Uncommon, RelicPool.Shared),
        new("UnceasingTop", RelicRarity.Rare, RelicPool.Shared),
        new("UnsettlingLamp", RelicRarity.Rare, RelicPool.Shared),
        new("Vajra", RelicRarity.Common, RelicPool.Shared),
        new("Vambrace", RelicRarity.Uncommon, RelicPool.Shared),
        new("VenerableTeaSet", RelicRarity.Common, RelicPool.Shared),
        new("VeryHotCocoa", RelicRarity.Ancient, RelicPool.Shared),
        new("VexingPuzzlebox", RelicRarity.Rare, RelicPool.Shared),
        new("WarPaint", RelicRarity.Common, RelicPool.Shared),
        new("Whetstone", RelicRarity.Common, RelicPool.Shared),
        new("WhiteBeastStatue", RelicRarity.Rare, RelicPool.Shared),
        new("WhiteStar", RelicRarity.Rare, RelicPool.Shared),
        new("WingCharm", RelicRarity.Shop, RelicPool.Shared),
    };

    // ── Character-specific pools (exact order) ──
    // Starter relics are filtered out by grab bag.

    public static readonly RelicInfo[] IroncladPool = new RelicInfo[]
    {
        new("Brimstone", RelicRarity.Shop, RelicPool.Ironclad),
        new("BurningBlood", RelicRarity.Starter, RelicPool.Ironclad),
        new("CharonsAshes", RelicRarity.Rare, RelicPool.Ironclad),
        new("DemonTongue", RelicRarity.Rare, RelicPool.Ironclad),
        new("PaperPhrog", RelicRarity.Uncommon, RelicPool.Ironclad),
        new("RedSkull", RelicRarity.Common, RelicPool.Ironclad),
        new("RuinedHelmet", RelicRarity.Rare, RelicPool.Ironclad),
        new("SelfFormingClay", RelicRarity.Uncommon, RelicPool.Ironclad),
    };

    public static readonly RelicInfo[] SilentPool = new RelicInfo[]
    {
        new("HelicalDart", RelicRarity.Rare, RelicPool.Silent),
        new("NinjaScroll", RelicRarity.Shop, RelicPool.Silent),
        new("PaperKrane", RelicRarity.Rare, RelicPool.Silent),
        new("RingOfTheSnake", RelicRarity.Starter, RelicPool.Silent),
        new("SneckoSkull", RelicRarity.Common, RelicPool.Silent),
        new("Tingsha", RelicRarity.Uncommon, RelicPool.Silent),
        new("ToughBandages", RelicRarity.Rare, RelicPool.Silent),
        new("TwistedFunnel", RelicRarity.Uncommon, RelicPool.Silent),
    };

    public static readonly RelicInfo[] DefectPool = new RelicInfo[]
    {
        new("CrackedCore", RelicRarity.Starter, RelicPool.Defect),
        new("DataDisk", RelicRarity.Common, RelicPool.Defect),
        new("EmotionChip", RelicRarity.Rare, RelicPool.Defect),
        new("GoldPlatedCables", RelicRarity.Uncommon, RelicPool.Defect),
        new("PowerCell", RelicRarity.Rare, RelicPool.Defect),
        new("Metronome", RelicRarity.Rare, RelicPool.Defect),
        new("RunicCapacitor", RelicRarity.Shop, RelicPool.Defect),
        new("SymbioticVirus", RelicRarity.Uncommon, RelicPool.Defect),
    };

    public static readonly RelicInfo[] NecrobinderPool = new RelicInfo[]
    {
        new("BigHat", RelicRarity.Rare, RelicPool.Necrobinder),
        new("BoneFlute", RelicRarity.Common, RelicPool.Necrobinder),
        new("BookRepairKnife", RelicRarity.Uncommon, RelicPool.Necrobinder),
        new("Bookmark", RelicRarity.Rare, RelicPool.Necrobinder),
        new("BoundPhylactery", RelicRarity.Starter, RelicPool.Necrobinder),
        new("FuneraryMask", RelicRarity.Uncommon, RelicPool.Necrobinder),
        new("IvoryTile", RelicRarity.Rare, RelicPool.Necrobinder),
        new("UndyingSigil", RelicRarity.Shop, RelicPool.Necrobinder),
    };

    public static readonly RelicInfo[] RegentPool = new RelicInfo[]
    {
        new("DivineRight", RelicRarity.Starter, RelicPool.Regent),
        new("FencingManual", RelicRarity.Common, RelicPool.Regent),
        new("GalacticDust", RelicRarity.Uncommon, RelicPool.Regent),
        new("LunarPastry", RelicRarity.Rare, RelicPool.Regent),
        new("MiniRegent", RelicRarity.Rare, RelicPool.Regent),
        new("OrangeDough", RelicRarity.Rare, RelicPool.Regent),
        new("Regalite", RelicRarity.Uncommon, RelicPool.Regent),
        new("VitruvianMinion", RelicRarity.Shop, RelicPool.Regent),
    };

    private static readonly HashSet<RelicRarity> GrabBagRarities = new()
    {
        RelicRarity.Common,
        RelicRarity.Uncommon,
        RelicRarity.Rare,
        RelicRarity.Shop
    };

    public static RelicInfo[] GetCharacterPool(Character character) => character switch
    {
        Character.Ironclad => IroncladPool,
        Character.Silent => SilentPool,
        Character.Defect => DefectPool,
        Character.Necrobinder => NecrobinderPool,
        Character.Regent => RegentPool,
        _ => throw new ArgumentException($"Unknown character: {character}")
    };

    /// <summary>
    /// Returns shared + character relics that can go into the grab bag
    /// (Common, Uncommon, Rare, Shop only), in game order.
    /// </summary>
    public static List<RelicInfo> GetGrabBagRelics(Character character)
    {
        var result = new List<RelicInfo>();
        // Shared pool first
        foreach (var r in SharedPool)
        {
            if (GrabBagRarities.Contains(r.Rarity))
                result.Add(r);
        }
        // Then character pool
        foreach (var r in GetCharacterPool(character))
        {
            if (GrabBagRarities.Contains(r.Rarity))
                result.Add(r);
        }
        return result;
    }

    /// <summary>
    /// Returns ALL shared relics (unfiltered) for SharedRelicGrabBag.Populate.
    /// The game's shared bag uses the IEnumerable overload which does NOT filter by rarity.
    /// All relics are grouped and shuffled, consuming correct RNG calls.
    /// </summary>
    public static List<RelicInfo> GetAllSharedRelics()
    {
        return new List<RelicInfo>(SharedPool);
    }

    /// <summary>
    /// Look up a relic by name (case-insensitive).
    /// </summary>
    public static RelicInfo? FindByName(string name)
    {
        var allPools = SharedPool
            .Concat(IroncladPool)
            .Concat(SilentPool)
            .Concat(DefectPool)
            .Concat(NecrobinderPool)
            .Concat(RegentPool);

        return allPools.FirstOrDefault(r =>
            r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Get all valid relic names for display/autocomplete.
    /// </summary>
    public static IEnumerable<string> GetAllGrabBagRelicNames(Character character)
    {
        return GetGrabBagRelics(character).Select(r => r.Name);
    }
}

