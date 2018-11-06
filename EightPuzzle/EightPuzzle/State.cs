using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EightPuzzle
{
    class State
    {
        private int[] currentBoard;
        private int outOfPlace;
        private int manhattanDistance;

        public State(int[] b)
        {
            currentBoard = b;
            calculateManhattanDistance();
            setOutOfPlace();
        }

        public int[] getcurrentBoard()
        {
            return currentBoard;
        }

        // gets number of incorrect tiles
        public int getOutOfPlace()
        {
            return outOfPlace;
        }

        //gets Manhattan distance
        public int getManhattanDistance()
        {
            return manhattanDistance;
        }

        // calculates number of incorrect tiles
        private void setOutOfPlace()
        {
            for (int i = 0; i < currentBoard.Length; i++)
            {
                if (currentBoard[i] != Board.getGoal()[i])
                {
                    outOfPlace++;
                }
            }
        }

        // calculates the Manhattan distance to correct tile
        private void calculateManhattanDistance()
        {
            int index = -1;
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    index++;
                    int val = (currentBoard[index] - 1);

                    if (val != -1)
                    {
                        int horiz = val % 3;
                        int vert = val / 3;

                        manhattanDistance += Math.Abs(vert - (y)) + Math.Abs(horiz - (x));
                    }
                }
            }
        }
    }
}
