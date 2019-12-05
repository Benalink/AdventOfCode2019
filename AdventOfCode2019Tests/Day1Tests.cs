using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode2019.Solvers;
using Xunit;

namespace AdventOfCode2019Tests
{
    public class Day1Tests
    {
        [Fact]
        public void Part1SolvesCorrectly()
        {
            IEnumerable<int> input = File.ReadAllLines($"{AppContext.BaseDirectory}/Inputs/Day1.txt")
                .Select(int.Parse);

            int result = Solvers.Day1.Part1(input);
            Assert.Equal(3278434, result);
        }

        [Fact]
        public void Part2SolvesCorrectly()
        {
            IEnumerable<int> input = File.ReadAllLines($"{AppContext.BaseDirectory}/Inputs/Day1.txt")
                .Select(int.Parse);

            int result = Solvers.Day1.Part2(input);
            Assert.Equal(4914785, result);
        }
    }
}
