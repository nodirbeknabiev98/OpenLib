using System;
using System.Collections.Generic;
using System.Linq;

namespace Zxcvbn.Matcher
{
    /// <summary>
    /// A match found with the RepeatMatcher
    /// </summary>
    public class RepeatMatch : Match
    {
        /// <summary>
        /// The character that was repeated
        /// </summary>
        public char RepeatChar { get; set; }
    }

    /// <inheritdoc />
    /// <summary>
    /// Match repeated characters in the password (repeats must be more than two characters long to count)
    /// </summary>
    public class RepeatMatcher : IMatcher
    {
        private const string RepeatPattern = "repeat";

        /// <inheritdoc />
        /// <summary>
        /// Find repeat matches in <paramref name="password" />
        /// </summary>
        /// <param name="password">The password to check</param>
        /// <returns>List of repeat matches</returns>
        /// <seealso cref="T:Zxcvbn.Matcher.RepeatMatch" />
        public IEnumerable<Match> MatchPassword(string password)
        {
            // Be sure to not count groups of one or two characters
            return password.GroupAdjacent(c => c).Where(g => g.Count() > 2).Select(g => new RepeatMatch
            {
                Pattern = RepeatPattern,
                Token = password.Substring(g.StartIndex, g.EndIndex - g.StartIndex + 1),
                i = g.StartIndex,
                j = g.EndIndex,
                Entropy = CalculateEntropy(password.Substring(g.StartIndex, g.EndIndex - g.StartIndex + 1)),
                RepeatChar = g.Key
            });
        }

        private static double CalculateEntropy(string match)
        {
            return Math.Log(PasswordScoring.PasswordCardinality(match) * match.Length, 2);
        }
    }
}