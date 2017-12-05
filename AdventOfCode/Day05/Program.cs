﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/*
--- Day 5: A Maze of Twisty Trampolines, All Alike ---

An urgent interrupt arrives from the CPU: it's trapped in a maze of jump instructions, and it would like assistance from any programs with spare cycles to help find the exit.

The message includes a list of the offsets for each jump. Jumps are relative: -1 moves to the previous instruction, and 2 skips the next one. Start at the first instruction in the list. The goal is to follow the jumps until one leads outside the list.

In addition, these instructions are a little strange; after each jump, the offset of that instruction increases by 1. So, if you come across an offset of 3, you would move three instructions forward, but change it to a 4 for the next time it is encountered.

For example, consider the following list of jump offsets:

0
3
0
1
-3
Positive jumps ("forward") move downward; negative jumps move upward. For legibility in this example, these offset values will be written all on one line, with the current instruction marked in parentheses. The following steps would be taken before an exit is found:

(0) 3  0  1  -3  - before we have taken any steps.
(1) 3  0  1  -3  - jump with offset 0 (that is, don't jump at all). Fortunately, the instruction is then incremented to 1.
 2 (3) 0  1  -3  - step forward because of the instruction we just modified. The first instruction is incremented again, now to 2.
 2  4  0  1 (-3) - jump all the way to the end; leave a 4 behind.
 2 (4) 0  1  -2  - go back to where we just were; increment -3 to -2.
 2  5  0  1  -2  - jump 4 steps forward, escaping the maze.
In this example, the exit is reached in 5 steps.

How many steps does it take to reach the exit?

Your puzzle answer was 318883.

--- Part Two ---

Now, the jumps are even stranger: after each jump, if the offset was three or more, instead decrease it by 1. Otherwise, increase it by 1 as before.

Using this rule with the above example, the process now takes 10 steps, and the offset values after finding the exit are left as 2 3 2 3 -1.

How many steps does it now take to reach the exit?

Your puzzle answer was 23948711.
*/

namespace Day05 {
    public class Program {
        static void Main(string[] args) {
            var instructions = GetInstructions("input.txt");
            
            Console.WriteLine($" Part I: {Instructions.CountStepsForOne(instructions).Key}");
            Console.WriteLine($"Part II: {Instructions.CountStepsForTwo(instructions).Key}");
            Console.ReadKey();
        }

        static List<int> GetInstructions(string fileName) {
            var list = new List<int>();
            try {
                using (var sr = new StreamReader(fileName)) {
                    try {
                        var line = sr.ReadLine();
                        while (line != null) {
                            list.Add(int.Parse(line));
                            line = sr.ReadLine();
                        }
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
            return list;
        }
    }
    public class Instructions {
        public static KeyValuePair<int, List<int>> CountStepsForOne(List<int> input) {
            var list = input.ToList();
            var steps = 0;
            var positon = 0;

            while (positon < list.Count) {
                var tempPos = positon;
                positon += list.ElementAt(positon);
                list[tempPos] += 1;
                steps++;
            }

            return new KeyValuePair<int, List<int>>(steps, list);
        }

        public static KeyValuePair<int, List<int>> CountStepsForTwo(List<int> input) {
            var list = input.ToList();
            var steps = 0;
            var positon = 0;

            while (positon < list.Count) {
                var tempPos = positon;
                positon += list.ElementAt(positon);

                if (list[tempPos] <= 0)
                    list[tempPos] += 1;
                else
                    list[tempPos] += Math.Abs(list[tempPos]) > 2 ? -1 : 1;

                steps++;
            }

            return new KeyValuePair<int, List<int>>(steps, list);
        }
    }
}
