using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simacogo
{
    class Board
    {
        //declare variables
        public const int ROWS = 9;
        public const int COLUMNS = 9;
        public const int SCORE_INDEX_MAX = 0;
        public const int SCORE_INDEX_MIN = 1;
        public static StreamWriter output = new StreamWriter("output.csv");
        
        //calculates MIN's and MAX's scores
        public static int[] getScores(string[,] currentBoard)
        {
            int[] scores = new int[2];

            //calculate rows
            for (int row = 0; row < Board.ROWS; row++)
            {
                for (int col = 1; col < Board.COLUMNS; col++)
                {
                    int maxMarkCounter = 0;
                    int minMarkCounter = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        string mark = currentBoard[row, col - i];
                        if (mark == "X")
                            maxMarkCounter++;
                        else if (mark == "O")
                            minMarkCounter++;
                    }
                    if (maxMarkCounter == 2)
                        scores[SCORE_INDEX_MAX] += 2;
                    if (minMarkCounter == 2)
                        scores[SCORE_INDEX_MIN] += 2;
                }
            }

            //calculate columns
            for (int col = 0; col < Board.COLUMNS; col++)
            {
                for (int row = 1; row < Board.ROWS; row++)
                {
                    int maxMarkCounter = 0;
                    int minMarkCounter = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        string mark = currentBoard[row - i, col];
                        if (mark == "X")
                            maxMarkCounter++;
                        else if (mark == "O")
                            minMarkCounter++;
                    }
                    if (maxMarkCounter == 2)
                        scores[SCORE_INDEX_MAX] += 2;
                    if (minMarkCounter == 2)
                        scores[SCORE_INDEX_MIN] += 2;
                }
            }

            //calculate major diagonal
            for (int row = Board.ROWS - 2; row >= 0; row--)
            {
                for (int col = Board.COLUMNS - 2; col >= 0; col--)
                {
                    int maxMarkCounter = 0;
                    int minMarkCounter = 0;
                    for (int i = 1; i >= 0; i--)
                    {
                        string mark = currentBoard[row + i, col + i];
                        if (mark == "X")
                            maxMarkCounter++;
                        else if (mark == "O")
                            minMarkCounter++;
                    }
                    if (maxMarkCounter == 2)
                        scores[SCORE_INDEX_MAX] += 1;
                    if (minMarkCounter == 2)
                        scores[SCORE_INDEX_MIN] += 1;
                }
            }

            //calculate minor diagonals
            for (int row = Board.ROWS - 2; row >= 0; row--)
            {
                for (int col = Board.COLUMNS - 2; col >= 0; col--)
                {
                    int maxMarkCounter = 0;
                    int minMarkCounter = 0;
                    for (int i = 1; i >= 0; i--)
                    {
                        string mark = currentBoard[row + i, col - i + 1];
                        if (mark == "X")
                            maxMarkCounter++;
                        else if (mark == "O")
                            minMarkCounter++;
                    }
                    if (maxMarkCounter == 2)
                        scores[SCORE_INDEX_MAX] += 1;
                    if (minMarkCounter == 2)
                        scores[SCORE_INDEX_MIN] += 1;
                }
            }

            return scores;
        }

        //returns an updated board
        public static string[,] updateBoard(int column, string[,] parentBoard, Player player)
        {
            string[,] currentBoard = copyBoard(parentBoard);

            for (int r = ROWS-1; r >= 0; r--)
            {
                if (currentBoard[r, column] == null)
                {
                    currentBoard[r, column] = player == Player.MAX ? "X" : "O";
                    break;
                }
            }
            return currentBoard;
        }

        //returns empty board
        public string[,] getDefaultBoard()
        {
            return new string[ROWS,COLUMNS];
        }

        public static void displayBoard(string[,] currentBoard)
        {
            int colCounter = 1;
            Console.WriteLine(" -------------------------------------");
            foreach (string i in currentBoard)
            {
                string space = i == null ? " |  " : " | ";
                if (colCounter > COLUMNS) colCounter = 1;
                Console.Write(colCounter == COLUMNS ? (space + i + " |\n" + " -------------------------------------\n") : (space + i));
                colCounter++;
            }
        }

        //return allowed numbers as inputs
        public static List<int> getInputsAllowed()
        {
            List<int> inputsAllowed = new List<int>();

            for (int i = 1; i <= COLUMNS; i++)
                inputsAllowed.Add(i);

            return inputsAllowed;
        }

        //outputs board to a file
        public static void outputBoard(string[,] currentBoard)
        {
            int colCounter = 1;
            output.WriteLine("---------------------------------");
            //output.WriteLine(1 + "," + 2 + "," + 3 + "," + 4 + "," + 5);// + "," + 6 + "," + 7 + "," + 8 + "," + 9);
            foreach (string i in currentBoard)
            {
                if (colCounter > COLUMNS) colCounter = 1;


                if (i == null) output.Write("-" + ",");
                else output.Write(i + ",");


                if (colCounter == COLUMNS) output.WriteLine();
                colCounter++;
            }
        }

        // creates a copy of board
        public static string[,] copyBoard(string[,] b)
        {
            string[,] temp = new string[ROWS, COLUMNS];
            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLUMNS; col++)
                {
                    temp[row, col] = b[row, col];
                }
            }

            return temp;
        }

        //checks if board is full
        public bool isFull(string[,] currentBoard)
        {
            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLUMNS; col++)
                {
                    if (currentBoard[row,col] == null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //checks if column is available
        public static bool isColumnAvailable(int column, string[,] currentBoard)
        {
            for (int r = ROWS-1; r >= 0; r--)
            {
                if (currentBoard[r, column] == null)
                    return true;
            }
            return false;
        }
    }
}
