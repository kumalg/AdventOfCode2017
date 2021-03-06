﻿/*
--- Day 3: SpiralPartOne Memory ---

You come across an experimental new kind of memory stored on an infinite two-dimensional grid.

Each square on the grid is allocated in a spiral pattern starting at a location marked 1 and then counting up while spiraling outward. For example, the first few squares are allocated like this:

17  16  15  14  13
18   5   4   3  12
19   6   1   2  11
20   7   8   9  10
21  22  23---> ...
While this is very space-efficient (no squares are skipped), requested data must be carried back to square 1 (the location of the only access port for this memory system) by programs that can only move up, down, left, or right. They always take the shortest path: the Manhattan Distance between the location of the data and square 1.

For example:

Data from square 1 is carried 0 steps, since it's at the access port.
Data from square 12 is carried 3 steps, such as: down, left, left.
Data from square 23 is carried only 2 steps: up twice.
Data from square 1024 must be carried 31 steps.
How many steps are required to carry the data from the square identified in your puzzle input all the way to the access port?

Your puzzle answer was 475.

--- Part Two ---

As a stress test on the system, the programs here clear the grid and then store the value 1 in square 1. Then, in the same allocation order as shown above, they store the sum of the values in all adjacent squares, including diagonals.

So, the first few squares' values are chosen as follows:

Square 1 starts with the value 1.
Square 2 has only one adjacent filled square (with value 1), so it also stores 1.
Square 3 has both of the above squares as neighbors and stores the sum of their values, 2.
Square 4 has all three of the aforementioned squares as neighbors and stores the sum of their values, 4.
Square 5 only has the first and fourth squares as neighbors, so it gets the value 5.
Once a square is written, its value does not change. Therefore, the first few squares would receive the following values:

147  142  133  122   59
304    5    4    2   57
330   10    1    1   54
351   11   23   25   26
362  747  806--->   ...
What is the first value written that is larger than your puzzle input?

Your puzzle answer was 279138.
*/

using System;

namespace AdventOfCode.Days {
    public class Day03 {
        private static void Main(string[] args) {
            Console.WriteLine($" Part I: {Spiral.CountSteps(277678)}");
            Console.WriteLine($"Part II: {Spiral.ComputeNextNumber(277678)}");
            Console.ReadKey();
        }

        public class Spiral {
            /* First star */
            public static int CountSteps(int value) {
                var ring = (int)Math.Ceiling(Math.Sqrt(value));
                if (ring % 2 == 0)
                    ring++;

                var maxValueInRing = ring * ring;

                var halfSteps = ring / 2;
                var maxSteps = halfSteps * 2;

                var steps = (maxValueInRing - value) % maxSteps;

                return halfSteps + Math.Abs(steps - halfSteps);
            }


            /* Second star */
            public static int ComputeNextNumber(int number) {
                var ring = (int)Math.Ceiling(Math.Sqrt(number));
                var matrix = new int[ring, ring];

                var center = ring / 2;

                matrix[center, center] = 1;
                var col = center;
                var row = center;

                for (var i = 1; i <= ring; i++) {
                    var offset = IsOdd(i) ? 1 : -1;

                    for (var j = 0; j < 2; j++) {
                        for (var k = 0; k < i; k++) {
                            if (j == 0) row += offset;
                            else col += offset;
                            if ((matrix[row, col] = ComputeValueInCell(matrix, row, col)) > number)
                                return matrix[row, col];
                        }
                    }
                }
                return 0;
            }

            private static bool IsOdd(int number) => number % 2 != 0;

            private static int ComputeValueInCell(int[,] matrix, int row, int col) {
                var result = 0;
                for (var i = -1; i < 2; i++) {
                    for (var j = -1; j < 2; j++)
                        result += matrix[row + i, col + j];
                }
                return result;
            }
        }
    }
}
