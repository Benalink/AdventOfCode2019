using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode2019.Solvers
{
    internal static partial class Solvers
    {

        internal class IntcodeComputer
        {
            private int[] memory;

            public IntcodeComputer(int[] memory)
            {
                this.memory = memory;
            }

            public int[] RunProgram()
            {
                for (int i = 0; i < memory.Length; i += 4)
                {
                    if (!RunInstruction(i))
                        break;
                }

                return memory;
            }

            private bool RunInstruction(int opcodeIndex)
            {
                var instruction = (InstructionCode)memory[opcodeIndex];
                if (instruction == InstructionCode.Halt)
                    return false;

                var operands = GetOperands(opcodeIndex);

                if (instruction == InstructionCode.Addition)
                    SetResult(opcodeIndex, operands.A + operands.B);
                else if (instruction == InstructionCode.Multiplication)
                    SetResult(opcodeIndex, operands.A * operands.B);
                else
                    throw new ArgumentOutOfRangeException(nameof(opcodeIndex), opcodeIndex,
                        "Instruction is not supported");

                return true;
            }

            private (int A, int B) GetOperands(int instructionIndex)
            {
                var (addressA, addressB) = (memory[instructionIndex + 1], memory[instructionIndex + 2]);
                return (memory[addressA], memory[addressB]);
            }

            private void SetResult(int instructionIndex, int result)
            {
                int outputAddress = memory[instructionIndex + 3];
                memory[outputAddress] = result;
            }
        }

        private enum InstructionCode
        {
            Addition = 1,
            Multiplication = 2,
            Halt = 99
        }

        internal static class Day2
        {
            internal static int Part1(int[] memory)
            {
                memory[1] = 12;
                memory[2] = 2;

                var computer = new IntcodeComputer(memory);
                computer.RunProgram();

                return memory[0];
            }

            internal static int? Part2(int[] memory, int targetOutput)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    for (int noun = 0; noun < 100; noun++)
                    {
                        int[] localMemory = memory.ToArray();
                        localMemory[1] = noun;
                        localMemory[2] = verb;
                        var computer = new IntcodeComputer(localMemory);
                        if (computer.RunProgram()[0] == targetOutput)
                        {
                            return 100 * noun + verb;
                        }
                    }
                }

                return null;
            }
        }
    }
}