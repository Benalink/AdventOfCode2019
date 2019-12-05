using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Solvers
{
    internal static partial class Solvers
    {
        internal static class Day1
        {
            internal static int Part1(IEnumerable<int> masses) =>
                masses.Select(CalculateFuelRequiredForMass).Sum();

            internal static int Part2(IEnumerable<int> masses) =>
                masses.Select(CalculateFuelRequiredForMassAndFuelMass).Sum();

            private static int CalculateFuelRequiredForMass(int mass) => (int) Math.Floor((float) mass / 3) - 2;

            private static int CalculateFuelRequiredForMassAndFuelMass(int mass)
            {
                int fuelRequired = 0;
                do
                {
                    mass = Math.Clamp(CalculateFuelRequiredForMass(mass), 0, Int32.MaxValue);
                    fuelRequired += mass;
                } while (mass > 8);

                return fuelRequired;
            }
        }
    }
}
