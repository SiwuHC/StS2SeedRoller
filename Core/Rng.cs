namespace StS2SeedRoller.Core;

/// <summary>
/// Exact replica of MegaCrit.Sts2.Core.Random.Rng
/// Uses System.Random with int seed to match game behavior.
/// </summary>
public class Rng
{
    private readonly Random _random;

    public int Counter { get; private set; }
    public uint Seed { get; }

    public Rng(uint seed, int counter = 0)
    {
        Counter = 0;
        Seed = seed;
        _random = new Random((int)seed);
        FastForwardCounter(counter);
    }

    public Rng(uint seed, string name)
        : this(seed + (uint)StringHelper.GetDeterministicHashCode(name))
    {
    }

    public void FastForwardCounter(int targetCount)
    {
        while (Counter < targetCount)
        {
            Counter++;
            _random.Next();
        }
    }

    public bool NextBool()
    {
        Counter++;
        return _random.Next(2) == 0;
    }

    public int NextInt(int maxExclusive = int.MaxValue)
    {
        Counter++;
        return _random.Next(maxExclusive);
    }

    public int NextInt(int minInclusive, int maxExclusive)
    {
        Counter++;
        return _random.Next(minInclusive, maxExclusive);
    }

    public float NextFloat(float max = 1f)
    {
        return NextFloat(0f, max);
    }

    public float NextFloat(float min, float max)
    {
        Counter++;
        return (float)(_random.NextDouble() * (double)(max - min) + (double)min);
    }

    public T? NextItem<T>(IList<T> items)
    {
        int count = items.Count;
        if (count == 0) return default;
        int index = NextInt(0, count);
        return items[index];
    }

    public T? NextItem<T>(IReadOnlyList<T> items)
    {
        int count = items.Count;
        if (count == 0) return default;
        int index = NextInt(0, count);
        return items[index];
    }

    public void Shuffle<T>(IList<T> list)
    {
        int i=list.Count;
        while(i>1)
        {
            i--;
            int j = NextInt(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
