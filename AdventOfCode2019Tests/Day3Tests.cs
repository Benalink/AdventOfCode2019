using System;
using System.IO;
using System.Linq;
using AdventOfCode2019.Solvers;
using Xunit;

namespace AdventOfCode2019Tests
{
    public class Day3Tests
    {
        [Fact]
        public void Example1OutputMatches()
        {
            var a = new [] { "R75", "D30", "R83", "U83", "L12", "D49", "R71", "U7", "L72" };
            var b = new[] {"U62", "R66", "U55", "R34", "D71", "R55", "D58", "R83"};
            int closestIntersectionDistance = Solvers.Day3.Part1(new[] {a, b});

            Assert.Equal(159, closestIntersectionDistance);
        }

        [Fact]
        public void Example2OutputMatches()
        {
            var a = new[] { "R98", "U47", "R26", "D63", "R33", "U87", "L62", "D20", "R33", "U53", "R51" };
            var b = new[] { "U98", "R91", "D20", "R16", "D67", "R40", "U7", "R15", "U6", "R7" };
            int closestIntersectionDistance = Solvers.Day3.Part1(new[] { a, b });

            Assert.Equal(135, closestIntersectionDistance);
        }

        [Fact]
        public void Part1SolvesCorrectly()
        {
            string[][] input = File.ReadAllLines($"{AppContext.BaseDirectory}/Inputs/Day3.txt").Select(line => line.Split(',')).ToArray();
            int closestIntersectionDistance = Solvers.Day3.Part1(input);

            Assert.Equal(0, closestIntersectionDistance);
        }
    }
}
