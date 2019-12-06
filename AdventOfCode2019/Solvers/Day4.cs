namespace AdventOfCode2019.Solvers
{
    internal static partial class Solvers
    {
        internal static class Day4
        {
            internal static int Part1(int min, int max)
            {
                int valid = 0;

                for (int i = min; i < max; i++)
                {
                    char[] candidate = i.ToString().ToCharArray();
                    char lastSeen = candidate[0];
                    bool hasADouble = false;
                    bool neverDecreases = true;

                    for (int j = 1; j < candidate.Length; j++)
                    {
                        if (candidate[j] == lastSeen)
                        {
                            hasADouble = true;
                        }

                        if (int.Parse(candidate[j].ToString()) < int.Parse(lastSeen.ToString()))
                        {
                            neverDecreases = false;
                        }

                        lastSeen = candidate[j];
                    }

                    if (hasADouble && neverDecreases)
                    {
                        valid++;
                    }
                }

                return valid;
            }

            internal static int Part2(int min, int max)
            {
                int valid = 0;

                for (int i = min; i < max; i++)
                {
                    char[] candidate = i.ToString().ToCharArray();
                    char lastSeen = candidate[0];
                    int repeated = 0;
                    bool hasADoubleStrict = false;
                    bool neverDecreases = true;

                    for (int j = 1; j < candidate.Length; j++)
                    {
                        if (lastSeen == candidate[j])
                        {
                            repeated++;
                        }
                        else
                        {
                            if (repeated == 1)
                            {
                                hasADoubleStrict = true;
                            }

                            repeated = 0;
                        }

                        if (int.Parse(candidate[j].ToString()) < int.Parse(lastSeen.ToString()))
                        {
                            neverDecreases = false;
                        }
                        
                        lastSeen = candidate[j];
                    }

                    if (repeated == 1)
                    {
                        hasADoubleStrict = true;
                    }

                    if (hasADoubleStrict && neverDecreases)
                    {
                        valid++;
                    }
                }

                return valid;
            }
        }
    }
}
