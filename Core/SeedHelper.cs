using System.Text;

namespace StS2SeedRoller.Core;

/// <summary>
/// Seed generation and canonicalization matching game logic.
/// </summary>
public static class SeedHelper
{
    private const string Characters = "0123456789ABCDEFGHJKLMNPQRSTUVWXYZ";

    public static string CanonicalizeSeed(string seed)
    {
        seed = seed.ToUpperInvariant();
        seed = seed.Replace('O', '0');
        seed = seed.Replace('I', '1');
        seed = seed.Trim();
        return seed;
    }

    public static string GenerateRandomSeed(Random random, int length = 10)
    {
        var sb = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            sb.Append(Characters[random.Next(Characters.Length)]);
        }
        return sb.ToString();
    }

    /// <summary>
    /// Generates a sequential seed from a counter for exhaustive search.
    /// Encodes the counter in the game's character set (base-33).
    /// </summary>
    public static string CounterToSeed(long counter, int length = 10)
    {
        var chars = new char[length];
        for (int i = length - 1; i >= 0; i--)
        {
            chars[i] = Characters[(int)(counter % Characters.Length)];
            counter /= Characters.Length;
        }
        return new string(chars);
    }
}
