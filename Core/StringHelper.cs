using System.Text.RegularExpressions;

namespace StS2SeedRoller.Core;

/// <summary>
/// Exact replica of MegaCrit.Sts2.Core.Helpers.StringHelper (hash and naming functions).
/// </summary>
public static class StringHelper
{
    private static readonly Regex CamelCaseRegex = new(@"([A-Za-z0-9]|\G(?!^))([A-Z])", RegexOptions.Compiled);

    public static int GetDeterministicHashCode(string str)
    {
        int num = 352654597;
        int num2 = num;
        for (int i = 0; i < str.Length; i += 2)
        {
            num = ((num << 5) + num) ^ str[i];
            if (i == str.Length - 1)
                break;
            num2 = ((num2 << 5) + num2) ^ str[i + 1];
        }
        return num + num2 * 1566083941;
    }

    public static string SnakeCase(string txt)
    {
        return CamelCaseRegex.Replace(txt.Trim(), "$1_$2").ToLowerInvariant();
    }

    public static string Slugify(string txt)
    {
        string text = CamelCaseRegex.Replace(txt.Trim(), "$1_$2");
        string upper = Regex.Replace(text.ToUpperInvariant(), @"\s+", "_");
        return Regex.Replace(upper, @"[^A-Z0-9_]", "");
    }
}
