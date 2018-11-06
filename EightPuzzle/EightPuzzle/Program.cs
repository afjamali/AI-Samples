/*
 * Afshin Jamali
 * SE 480
 * Assignment 1
 */

using EightPuzzle.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EightPuzzle
{
    class Program
    {
        // main class, gets input from user
        static void Main(string[] args)
        {
            int[] goal = { 1, 2, 3, 8, 0, 4, 7, 6, 5 };
            int[] easyDifficutly = { 1, 3, 4, 8, 6, 2, 7, 0, 5 };
            int[] mediumDifficulty = { 2, 8, 1, 0, 4, 3, 7, 6, 5 };
            int[] hardDifficulty = { 5, 6, 7, 4, 0, 8, 3, 2, 1 };
            int[] board = new int[9];

            Console.WriteLine("8-puzzle\n");
            Console.WriteLine("Choose search below:\n");

            Console.WriteLine("1. Breadth-first" + "\n"
                + "2. Depth-first search" + "\n"
                + "3. Iterative deepening search" + "\n"
                + "4. Uniform-cost" + "\n"
                + "5. Best-first" + "\n"
                + "6. A*1 out of place" + "\n"
                + "7. A*2 manhattan distance" + "\n"
                + "8. A*3 combined h's" + "\n");

            int searchType = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nChoose difficulty below:\n");            

            Console.WriteLine("goal = { 1, 2, 3, 8, 0, 4, 7, 6, 5 }" + "\n"
                + "1.  easyDifficutly = { 1, 3, 4, 8, 6, 2, 7, 0, 5 }" + "\n"
                + "2.  mediumDifficulty = { 2, 8, 1, 0, 4, 3, 7, 6, 5 }" + "\n"
                + "3.  hardDifficulty = { 5, 6, 7, 4, 0, 8, 3, 2, 1 }" + "\n");

            int diff = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n\n");

            switch (diff)
            {
                case 1:
                    board = easyDifficutly;
                    break;
                case 2:
                    board = mediumDifficulty;
                    break;
                case 3:
                    board = hardDifficulty;
                    break;
            }

            switch (searchType)
            {
                case 1:
                    new BreadthFirstSearch(board, goal).run();
                    break;
                case 2:
                    new DepthFirstSearch(board, goal).run();
                    break;
                case 3:
                    new IterativeDeepeningSearch(board, goal).run();
                    break;
                case 4:
                    new UniformCostSearch(board, goal).run();
                    break;
                case 5:
                    new BestFirstSearch(board, goal, 1).run();
                    break;
                case 6:
                    new AStarSearch(board, goal, 2).run();
                    break;
                case 7:
                    new AStarSearch(board, goal, 2).run();
                    break;
                case 8:
                    new AStarSearch(board, goal, 3).run();
                    break;
            }
        }
    }
}
