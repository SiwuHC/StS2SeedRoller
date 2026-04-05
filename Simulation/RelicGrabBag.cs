using StS2SeedRoller.Core;
using StS2SeedRoller.Data;

namespace StS2SeedRoller.Simulation;

/// <summary>
/// Simulates the game's RelicGrabBag: relics grouped by rarity, shuffled, pulled from front.
/// </summary>
public class RelicGrabBag
{
    private static readonly HashSet<RelicRarity> ValidRarities = new()
    {
        RelicRarity.Common,
        RelicRarity.Uncommon,
        RelicRarity.Rare,
        RelicRarity.Shop
    };

    private readonly Dictionary<RelicRarity, List<RelicInfo>> _deques = new();

    /// <summary>
    /// Populate matching game's Populate(IEnumerable) overload - NO rarity filtering.
    /// Used for SharedRelicGrabBag. All relics are grouped and shuffled to consume
    /// correct RNG calls, even though only Common/Uncommon/Rare/Shop are pullable.
    /// </summary>
    public void PopulateUnfiltered(IEnumerable<RelicInfo> relics, Rng rng)
    {
        foreach (var relic in relics)
        {
            if (!_deques.TryGetValue(relic.Rarity, out var list))
            {
                list = new List<RelicInfo>();
                _deques[relic.Rarity] = list;
            }
            list.Add(relic);
        }

        foreach (var list in _deques.Values)
        {
            UnstableShuffle(list, rng);
        }
    }

    /// <summary>
    /// Populate matching game's Populate(Player, Rng) overload - filters to grab bag rarities.
    /// Used for player's personal RelicGrabBag.
    /// </summary>
    public void Populate(IEnumerable<RelicInfo> relics, Rng rng)
    {
        foreach (var relic in relics)
        {
            if (!ValidRarities.Contains(relic.Rarity))
                continue;

            if (!_deques.TryGetValue(relic.Rarity, out var list))
            {
                list = new List<RelicInfo>();
                _deques[relic.Rarity] = list;
            }
            list.Add(relic);
        }

        // Shuffle each rarity group using the same RNG (Fisher-Yates via UnstableShuffle)
        foreach (var list in _deques.Values)
        {
            UnstableShuffle(list, rng);
        }
    }

    /// <summary>
    /// Pull the first relic of the given rarity from the front.
    /// If that rarity is empty, fall through: Shop → Common → Uncommon → Rare.
    /// </summary>
    public RelicInfo? PullFromFront(RelicRarity rarity)
    {
        var deque = GetAvailableDeque(rarity);
        if (deque == null || deque.Count == 0)
            return null;

        var relic = deque[0];
        deque.RemoveAt(0);
        return relic;
    }

    /// <summary>
    /// Remove a relic from all queues (used when shared bag syncs with player bag).
    /// </summary>
    public void Remove(string relicName)
    {
        foreach (var list in _deques.Values)
        {
            list.RemoveAll(r => r.Name == relicName);
        }
    }

    /// <summary>
    /// Peek at what the first relic of a given rarity would be without removing it.
    /// </summary>
    public RelicInfo? PeekFront(RelicRarity rarity)
    {
        var deque = GetAvailableDeque(rarity);
        if (deque == null || deque.Count == 0)
            return null;
        return deque[0];
    }

    private List<RelicInfo>? GetAvailableDeque(RelicRarity rarity)
    {
        var list = GetDeque(rarity);
        while (list != null && list.Count == 0)
        {
            rarity = rarity switch
            {
                RelicRarity.Shop => RelicRarity.Common,
                RelicRarity.Common => RelicRarity.Uncommon,
                RelicRarity.Uncommon => RelicRarity.Rare,
                _ => RelicRarity.None,
            };
            list = rarity == RelicRarity.None ? null : GetDeque(rarity);
        }
        return list;
    }

    private List<RelicInfo> GetDeque(RelicRarity rarity)
    {
        if (_deques.TryGetValue(rarity, out var list))
            return list;
        return new List<RelicInfo>();
    }

    /// <summary>
    /// Exact replica of game's UnstableShuffle (Fisher-Yates, backwards).
    /// </summary>
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
