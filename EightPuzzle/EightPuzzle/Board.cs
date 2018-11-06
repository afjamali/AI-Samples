using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EightPuzzle
{
    /**
     * Class for checking goal, updating board, calculating tile position, and tile number
     * 
     * Also displays board history and outputs results to a file
     * 
     */
    class Board
    {
        private static int[] goal;
        private int[] difficulty;
        StreamWriter output;

        public Board(int[] d, int[] g)
        {
            difficulty = d;
            goal = g;            
        }

        // Updates board. To be used for finding childs state
        public static int[] updateBoard(int[] parentBoard, NodeAction action)
        {
            int[] currentBoard = copyBoard(parentBoard);
            int currentPosition = getBlankIndex(currentBoard);
            int nextPosition = getNextPosition(currentBoard, action);


            if (nextPosition == -1)
                return currentBoard;

            int temp = currentBoard[nextPosition];
            currentBoard[nextPosition] = currentBoard[currentPosition];
            currentBoard[currentPosition] = temp;

            return currentBoard;
        }

        // gets tile number for calculating cost
        public static int getTileCost(int[] parentBoard, NodeAction action)
        {
            return parentBoard[getNextPosition(parentBoard, action)];
        }

        // gets next index to move blank in index
        public static int getNextPosition(int[] parentBoard, NodeAction action)
        {
            int currentPosition = getBlankIndex(parentBoard);
            int nextPosition = -1;

            switch (action)
            {
                case NodeAction.LEFT:
                    nextPosition = currentPosition - 1;
                    break;
                case NodeAction.RIGHT:
                    nextPosition = currentPosition + 1;
                    break;
                case NodeAction.UP:
                    nextPosition = currentPosition - 3;
                    break;
                case NodeAction.DOWN:
                    nextPosition = currentPosition + 3;
                    break;
            }

            return nextPosition;
        }

        public int[] getDifficulty()
        {
            return difficulty;
        }

        // checks if board macthes goal
        public bool isGoal(Node n)
        {
            return n.getState().getcurrentBoard().SequenceEqual(goal);
        }

        // creates a copy of board
        public static int[] copyBoard(int[] b)
        {
            int[] temp = new int[9];
            for (int i = 0; i < b.Length; i++)
                temp[i] = b[i];
            return temp;
        }

        // gets the position of blank
        public static int getBlankIndex(int[] currentBoard)
        {
            for (int i = 0; i < currentBoard.Length; i++)
            {
                if (currentBoard[i] == 0)
                    return i;
            }

            return -1;
        }

        // shows solution path from start of node tree
        public void showSolution(Node n)
        {
            Stack<Node> solution = getPath(n);
            output = new StreamWriter("output.txt");

            foreach (Node s in solution)
            {
                displayBoard(s.getState().getcurrentBoard());
                Console.WriteLine(s.getAction() + ", depth " + s.getDepth() + ", cost = " + s.getCost() + ", total cost = " + s.getGCost() + "\n");
                output.WriteLine(s.getAction() + ", depth " + s.getDepth() + ", cost = " + s.getCost() + ", total cost = " + s.getGCost());
                output.WriteLine();
            }
            output.Close();
        }

        // retrieves path from start of node tree
        private Stack<Node> getPath(Node n)
        {
            Stack<Node> path = new Stack<Node>();
            path.Push(n);
            n = n.getParent();

            while (n != null && n.getParent() != null)
            {
                path.Push(n);
                n = n.getParent();
            }

            if (n != null)
                path.Push(n);

            return path;
        }

        //displays current board
        private void displayBoard(int[] b)
        {
            int colCounter = 1;
            Console.WriteLine(" -------------");
            foreach (int i in b)
            {
                if (colCounter > 3) colCounter = 1;
                Console.Write(colCounter == 3 ? (" | " + i + " |\n" + " -------------\n") : (" | " + i));
                output.Write(i + " ");
                colCounter++;
            }
            output.WriteLine();
        }

        public static int[] getGoal()
        {
            return goal;
        }
    }
}
