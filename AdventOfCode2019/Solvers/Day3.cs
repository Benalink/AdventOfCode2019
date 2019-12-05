using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Solvers
{
    internal static partial class Solvers
    {
        internal static class Day3
        {
            internal struct Vector2
            {

                public int X;
                public int Y;

                public Vector2(int x, int y)
                {
                    this.X = x;
                    this.Y = y;
                }

                public bool Equals(Vector2 other)
                {
                    return X == other.X && Y == other.Y;
                }

                public override bool Equals(object obj)
                {
                    return obj is Vector2 other && Equals(other);
                }

                public override int GetHashCode()
                {
                    unchecked
                    {
                        return (X * 397) ^ Y;
                    }
                }

                public static bool operator ==(Vector2 left, Vector2 right)
                {
                    return left.Equals(right);
                }

                public static bool operator !=(Vector2 left, Vector2 right)
                {
                    return !left.Equals(right);
                }

                public static Vector2 operator +(Vector2 a, Vector2 b)
                {
                    return new Vector2(a.X + b.X, a.Y + b.Y);
                }

                public static Vector2 operator -(Vector2 a, Vector2 b)
                {
                    return new Vector2(a.X - b.X, a.Y - b.Y);
                }
            }

            internal struct LineSegment
            {
                public Vector2 Start;
                public Vector2 End;
                public bool IsHorizontal => this.Start.Y == this.End.Y;
                public bool IsVertical => this.Start.X == this.End.X;

                public LineSegment(Vector2 start, Vector2 end)
                {
                    Start = start;
                    End = end;
                }
            }

            internal static class LineUtils
            {
                private static readonly Vector2 Origin = new Vector2(0, 0);

                internal static List<LineSegment> GetLineSegmentsFromMoves(string[] moves)
                {
                    Vector2 currentPosition = Origin;
                    var segments = new List<LineSegment>();

                    foreach (var move in moves)
                    {
                        Vector2 moveVector = ParseMove(move);
                        Vector2 newPosition = currentPosition + moveVector;

                        segments.Add(new LineSegment(currentPosition, newPosition));
                        currentPosition = newPosition;
                    }

                    return segments;
                }

                internal static int CountStepsTillPoint(List<LineSegment> lineA, List<LineSegment> lineB, Vector2 intersectionPoint)
                {
                    var lastSegment = lineA.Last();
                    lineA.Add(new LineSegment(lastSegment.End, intersectionPoint));
                    lastSegment = lineB.Last();
                    lineB.Add(new LineSegment(lastSegment.End, intersectionPoint));

                    return lineA.Union(lineB).Select(segment =>
                    {
                        var delta = segment.End - segment.Start;
                        return Math.Abs(delta.X) + Math.Abs(delta.Y);
                    }).Sum();

                }

                internal static Vector2? GetIntersectionPointOfLineSegments(LineSegment a, LineSegment b)
                {
                    bool parallel = a.IsVertical == b.IsVertical;

                    if (parallel)
                        return null;

                    LineSegment horizontal = a.IsHorizontal ? a : b;
                    LineSegment vertical = a.IsVertical ? a : b;

                    if (!(Math.Min(vertical.Start.Y, vertical.End.Y) <= horizontal.Start.Y && Math.Max(vertical.Start.Y, vertical.End.Y) >= horizontal.Start.Y))
                        return null;

                    if (!(Math.Min(horizontal.Start.X, horizontal.End.X) <= vertical.Start.X && Math.Max(horizontal.Start.X, horizontal.End.X) >= vertical.Start.X))
                        return null;

                    return new Vector2(a.IsVertical ? a.Start.X : b.Start.X, a.IsHorizontal ? a.Start.Y : b.Start.Y);
                }

                private static Vector2 ParseMove(string move)
                {
                    var directionFunctionLookup = new Dictionary<char, Func<int, Vector2>>
                    {
                        {'U', mag => new Vector2(0, mag)},
                        {'D', mag => new Vector2(0, mag)},
                        {'L', mag => new Vector2(mag, 0)},
                        {'R', mag => new Vector2(mag, 0)}
                    };

                    var magnitudeFunctionLookup = new Dictionary<char, Func<int, int>>
                    {
                        {'U', dist => dist},
                        {'D', dist => -dist},
                        {'L', dist => -dist},
                        {'R', dist => dist},
                    };

                    char direction = move[0];
                    int distance = int.Parse(move[1..]);

                    int magnitude = magnitudeFunctionLookup[direction](distance);
                    Vector2 vector = directionFunctionLookup[direction](magnitude);

                    return vector;
                }
            }

            internal static int Part1(string[][] lines)
            {
                List<List<LineSegment>> segmentsPerLine = lines.Select(LineUtils.GetLineSegmentsFromMoves).ToList();
                var intersectionPoints = new List<Vector2>();

                foreach (var line1Segment in segmentsPerLine[0])
                {
                    foreach (var line2Segment in segmentsPerLine[1])
                    {
                        Vector2? intersection =
                            LineUtils.GetIntersectionPointOfLineSegments(line1Segment, line2Segment);

                        if (intersection.HasValue && intersection.Value.X != 0 &&
                            intersection.Value.X != intersection.Value.Y)
                        {
                            intersectionPoints.Add(intersection.Value);
                        }
                    }
                }

                return intersectionPoints.Select(ip => Math.Abs(ip.X) + Math.Abs(ip.Y)).OrderBy(dist => dist).First();
            }

            internal static int Part2(string[][] lines)
            {
                List<List<LineSegment>> segmentsPerLine = lines.Select(LineUtils.GetLineSegmentsFromMoves).ToList();
                var intersectionPoints = new List<(Vector2 ip, List<LineSegment> line1, List<LineSegment> line2)>();

                int line1SegmentIndex = 0;
                foreach (var line1Segment in segmentsPerLine[0])
                {
                    int line2SegmentIndex = 0;
                    foreach (var line2Segment in segmentsPerLine[1])
                    {
                        Vector2? intersection =
                            LineUtils.GetIntersectionPointOfLineSegments(line1Segment, line2Segment);

                        if (intersection.HasValue && intersection.Value.X != 0 &&
                            intersection.Value.X != intersection.Value.Y)
                        {
                            intersectionPoints.Add((intersection.Value, segmentsPerLine[0].GetRange(0, line1SegmentIndex), segmentsPerLine[1].GetRange(0, line2SegmentIndex)));
                        }

                        line2SegmentIndex++;
                    }

                    line1SegmentIndex++;
                }

                return intersectionPoints.Select(ip => LineUtils.CountStepsTillPoint(ip.line1, ip.line2, ip.ip)).OrderBy(dist => dist).First();
            }
        }
    }
}