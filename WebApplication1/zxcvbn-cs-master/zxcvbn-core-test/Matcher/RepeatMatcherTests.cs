using System.Linq;
using FluentAssertions;
using Xunit;
using Zxcvbn.Matcher;

namespace Zxcvbn.Tests.Matcher
{
    public class RepeatMatcherTests
    {
        [Fact]
        public void MatchNoRepeatedCharacters()
        {
            var repeat = new RepeatMatcher();

            var res = repeat.MatchPassword("asdf").ToList();

            res.Count.Should().Be(0);
        }

        [Fact]
        public void MatchRepeatedCharacters()
        {
            var repeat = new RepeatMatcher();

            var res = repeat.MatchPassword("aaasdffff").ToList();

            res.Count.Should().Be(2);

            res[0].i.Should().Be(0);
            res[0].j.Should().Be(2);
            res[0].Token.Should().Be("aaa");

            res[1].i.Should().Be(5);
            res[1].j.Should().Be(8);
            res[1].Token.Should().Be("ffff");
        }
    }
}