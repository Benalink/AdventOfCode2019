using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode2019.Solvers;
using Xunit;

namespace AdventOfCode2019Tests
{
    public class Day4Tests
    {
        [Fact]
        public void Part1SolvesCorrectly()
        {
            int validPasswords = Solvers.Day4.Part1(134792, 675810);
            Assert.Equal(1955, validPasswords);
        }

        [Fact]
        public void Part2SolvesCorrectly()
        {
            int validPasswords = Solvers.Day4.Part2(134792, 675810);
            Assert.Equal(1319, validPasswords);

        }
    }
}
