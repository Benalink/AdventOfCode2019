using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AdventOfCode2019.Solvers;
using Xunit;

namespace AdventOfCode2019Tests
{
    public class Day2Tests
    {
        [Fact]
        public void Part1SolvesCorrectly()
        {
            int[] input = File.ReadAllText($"{AppContext.BaseDirectory}/Inputs/Day2.txt").Split(',')
                .Select(int.Parse).ToArray();

            int result = Solvers.Day2.Part1(input);
            Assert.Equal(4576384, result);
        }

        [Fact]
        public void Part2SolvesCorrectly()
        {
            int[] input = File.ReadAllText($"{AppContext.BaseDirectory}/Inputs/Day2.txt").Split(',')
                .Select(int.Parse).ToArray();

            int? result = Solvers.Day2.Part2(input, 19690720);
            Assert.Equal(5398, result);
        }

        [Fact]
        public void ExampleProgramOutputMatches()
        {
            int[] input = {1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50};
            int[] expectedResult = {3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50};

            var computer = new Solvers.IntcodeComputer(input);

            int[] result = computer.RunProgram();
            Assert.Equal(expectedResult, input);

        }
    }
}
