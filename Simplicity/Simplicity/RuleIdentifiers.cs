using System.Globalization;

namespace Simplicity
{
    /// <summary>
    /// Contains identifiers for all analyzer rules.
    /// </summary>
    internal static class RuleIdentifiers
    {
        /// <summary>
        /// Detects all usages of the greater than sign ('>') in C# code.
        /// </summary>
        public const string GreaterThan = "SIMP1001";

        public static string GetHelpUri(string identifier)
        {
            return string.Format(CultureInfo.InvariantCulture, "https://github.com/strudso/Analyzers/blob/main/docs/Rules/{0}.md", identifier);
        }
    }
}
