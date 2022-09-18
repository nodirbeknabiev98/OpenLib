﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Zxcvbn.Matcher
{
    /// <inheritdoc />
    /// <summary>
    /// <para>A matcher that checks for keyboard layout patterns (e.g. 78523 on a keypad, or plkmn on a QWERTY keyboard).</para>
    /// <para>Has patterns for QWERTY, DVORAK, numeric keybad and mac numeric keypad</para>
    /// <para>The matcher accounts for shifted characters (e.g. qwErt or po9*7y) when detecting patterns as well as multiple changes in direction.</para>
    /// </summary>
    public class SpatialMatcher : IMatcher
    {
        private const string SpatialPattern = "spatial";

        private readonly Lazy<List<SpatialGraph>> _spatialGraphs = new Lazy<List<SpatialGraph>>(GenerateSpatialGraphs);

        /// <inheritdoc />
        /// <summary>
        /// Match the password against the known keyboard layouts
        /// </summary>
        /// <param name="password">Password to match</param>
        /// <returns>List of matching patterns</returns>
        /// <seealso cref="M:Zxcvbn.Matcher.SpatialMatcher.SpatialMatch(Zxcvbn.Matcher.SpatialMatcher.SpatialGraph,System.String)" />
        public IEnumerable<Match> MatchPassword(string password)
        {
            return _spatialGraphs.Value.SelectMany((g) => SpatialMatch(g, password)).ToList();
        }

        // In the JS version these are precomputed, but for now we'll generate them here when they are first needed.
        private static List<SpatialGraph> GenerateSpatialGraphs()
        {
            // Kwyboard layouts directly from zxcvbn's build_keyboard_adjacency_graph.py script
            const string qwerty = @"
`~ 1! 2@ 3# 4$ 5% 6^ 7& 8* 9( 0) -_ =+
    qQ wW eE rR tT yY uU iI oO pP [{ ]} \|
     aA sS dD fF gG hH jJ kK lL ;: '""
      zZ xX cC vV bB nN mM ,< .> /?
";

            const string dvorak = @"
`~ 1! 2@ 3# 4$ 5% 6^ 7& 8* 9( 0) [{ ]}
    '"" ,< .> pP yY fF gG cC rR lL /? =+ \|
     aA oO eE uU iI dD hH tT nN sS -_
      ;: qQ jJ kK xX bB mM wW vV zZ
";

            const string keypad = @"
  / * -
7 8 9 +
4 5 6
1 2 3
  0 .
";

            const string macKeypad = @"
  = / *
7 8 9 -
4 5 6 +
1 2 3
  0 .
";

            return new List<SpatialGraph> { new SpatialGraph("qwerty", qwerty, true),
                    new SpatialGraph("dvorak", dvorak, true),
                    new SpatialGraph("keypad", keypad, false),
                    new SpatialGraph("mac_keypad", macKeypad, false)
                };
        }

        /// <summary>
        /// Match the password against a single pattern
        /// </summary>
        /// <param name="graph">Adjacency graph for this key layout</param>
        /// <param name="password">Password to match</param>
        /// <returns>List of matching patterns</returns>
        private static IEnumerable<Match> SpatialMatch(SpatialGraph graph, string password)
        {
            var matches = new List<Match>();

            var i = 0;
            while (i < password.Length - 1)
            {
                int turns = 0, shiftedCount = 0;
                var lastDirection = -1;

                var j = i + 1;
                for (; j < password.Length; ++j)
                {
                    var foundDirection = graph.GetAdjacentCharDirection(password[j - 1], password[j], out var shifted);

                    if (foundDirection != -1)
                    {
                        // Spatial match continues
                        if (shifted) shiftedCount++;
                        if (lastDirection != foundDirection)
                        {
                            turns++;
                            lastDirection = foundDirection;
                        }
                    }
                    else break; // This character not a spatial match
                }

                // Only consider runs of greater than two
                if (j - i > 2)
                {
                    matches.Add(new SpatialMatch()
                    {
                        Pattern = SpatialPattern,
                        i = i,
                        j = j - 1,
                        Token = password.Substring(i, j - i),
                        Graph = graph.Name,
                        Entropy = graph.CalculateEntropy(j - i, turns, shiftedCount),
                        Turns = turns,
                        ShiftedCount = shiftedCount
                    });
                }

                i = j;
            }

            return matches;
        }

        // Instances of Point or Pair in the standard library are in UI assemblies, so define our own version to reduce dependencies
        private struct Point
        {
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; }
            public int Y { get; }

            public override string ToString()
            {
                return "{" + X + ", " + Y + "}";
            }
        }

        // See build_keyboard_adjacency_graph.py in zxcvbn for how these are generated
        private class SpatialGraph
        {
            public SpatialGraph(string name, string layout, bool slanted)
            {
                Name = name;
                BuildGraph(layout, slanted);
            }

            public string Name { get; }
            private Dictionary<char, List<string>> AdjacencyGraph { get; set; }
            private double AverageDegree { get; set; }
            private int StartingPositions { get; set; }

            /// <summary>
            /// Calculate entropy for a math that was found on this adjacency graph
            /// </summary>
            public double CalculateEntropy(int matchLength, int turns, int shiftedCount)
            {
                // This is an estimation of the number of patterns with length of matchLength or less with turns turns or less
                var possibilities = Enumerable.Range(2, matchLength - 1).Sum(i =>
                {
                    var possibleTurns = Math.Min(turns, i - 1);
                    return Enumerable.Range(1, possibleTurns).Sum(j => StartingPositions * Math.Pow(AverageDegree, j) * PasswordScoring.Binomial(i - 1, j - 1));
                });

                var entropy = Math.Log(possibilities, 2);

                // Entropy increaeses for a mix of shifted and unshifted
                if (shiftedCount > 0)
                {
                    var unshifted = matchLength - shiftedCount;
                    entropy += Math.Log(Enumerable.Range(0, Math.Min(shiftedCount, unshifted) + 1).Sum(i => PasswordScoring.Binomial(matchLength, i)), 2);
                }

                return entropy;
            }

            /// <summary>
            /// Returns the 'direction' of the adjacent character (i.e. index in the adjacency list).
            /// If the character is not adjacent, -1 is returned
            ///
            /// Uses the 'shifted' out parameter to let the caller know if the matched character is shifted
            /// </summary>
            public int GetAdjacentCharDirection(char c, char adjacent, out bool shifted)
            {
                //XXX: This function is a bit strange, with an out parameter this should be refactored into something sensible

                shifted = false;

                if (!AdjacencyGraph.ContainsKey(c)) return -1;

                var adjacentEntry = AdjacencyGraph[c].FirstOrDefault(s => s != null && s.Contains(adjacent));
                if (adjacentEntry == null) return -1;

                shifted = adjacentEntry.IndexOf(adjacent) > 0; // i.e. shifted if not first character in the adjacency
                return AdjacencyGraph[c].IndexOf(adjacentEntry);
            }

            private static Point[] GetAlignedAdjacent(Point c)
            {
                var x = c.X;
                var y = c.Y;

                return new[] { new Point(x - 1, y), new Point(x - 1, y - 1), new Point(x, y - 1), new Point(x + 1, y - 1), new Point(x + 1, y), new Point(x + 1, y + 1), new Point(x, y + 1), new Point(x - 1, y + 1) };
            }

            private static Point[] GetSlantedAdjacent(Point c)
            {
                var x = c.X;
                var y = c.Y;

                return new[] { new Point(x - 1, y), new Point(x, y - 1), new Point(x + 1, y - 1), new Point(x + 1, y), new Point(x, y + 1), new Point(x - 1, y + 1) };
            }

            private void BuildGraph(string layout, bool slanted)
            {
                var tokens = layout.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                var tokenSize = tokens[0].Length;

                // Put the characters in each keyboard cell into the map agains t their coordinates
                var positionTable = new Dictionary<Point, string>();
                var lines = layout.Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                for (var y = 0; y < lines.Length; ++y)
                {
                    var line = lines[y];
                    var slant = slanted ? y - 1 : 0;

                    foreach (var token in line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries))
                    {
                        var x = (line.IndexOf(token, StringComparison.Ordinal) - slant) / (tokenSize + 1);
                        var p = new Point(x, y);
                        positionTable[p] = token;
                    }
                }

                AdjacencyGraph = new Dictionary<char, List<string>>();
                foreach (var pair in positionTable)
                {
                    var p = pair.Key;
                    foreach (var c in pair.Value)
                    {
                        AdjacencyGraph[c] = new List<string>();
                        var adjacentPoints = slanted ? GetSlantedAdjacent(p) : GetAlignedAdjacent(p);

                        foreach (var adjacent in adjacentPoints)
                        {
                            // We want to include nulls so that direction is correspondent with index in the list
                            AdjacencyGraph[c].Add(positionTable.ContainsKey(adjacent) ? positionTable[adjacent] : null);
                        }
                    }
                }

                // Calculate average degree and starting positions, cf. init.coffee
                StartingPositions = AdjacencyGraph.Count;
                AverageDegree = AdjacencyGraph.Sum(adj => adj.Value.Count(a => a != null)) * 1.0 / StartingPositions;
            }
        }
    }
}