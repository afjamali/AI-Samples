/*
 * Afshin Jamali
 * SE 480
 * Assignment 1
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EightPuzzle.Algorithm
{
    class IterativeDeepeningSearch
    {
        //declare variables
        private Board board;
        Stack<Node> stack;
        int nodesPoppedOff;
        int maxQueue;

        public IterativeDeepeningSearch(int[] d, int[] g)
        {
            board = new Board(d, g);
            stack = new Stack<Node>();
        }

        //run the search algorithm
        public void run()
        {
            //cannot explore node beyod cutoff length
            int cutOff = 0;
            bool exit = false;

            while (cutOff <= int.MaxValue)
            {
                //add root to stack
                stack.Push(new Node(null, new State(board.getDifficulty()), NodeAction.NA, 0, 0, 0));

                while (stack.Count != 0)
                {
                    Node node = pop();

                    //check wether is goal
                    if (!board.isGoal(node))
                    {
                        //generate successors
                        node.generateSuccessors();
                        List<Node> successors = new List<Node>();

                        //if node no already added to stack, add node
                        foreach (Node n in node.getSuccessors())
                            if (!previouslyAdded(n) && n.getDepth() <= cutOff)
                                successors.Add(n);

                        //add successors to stack
                        foreach (Node s in successors)
                            stack.Push(s);

                        //calculate max space
                        if (maxQueue < stack.Count) maxQueue = stack.Count;
                    }
                    else
                    {
                        // if in here, that means goal has been acheived. Show board history next
                        board.showSolution(node);
                        Console.WriteLine("nodes popped off " + nodesPoppedOff + ", max queue size " + maxQueue);
                        Console.ReadLine();
                        exit = true;
                        break;
                    }
                }
                if (exit) break;//either solution has been found or stack is empty so we must exit
                cutOff++;
            }
        }

        //check wether node has been previously added
        private bool previouslyAdded(Node node)
        {
            bool isRepeat = false;
            Node n = node;

            while (n.getParent() != null && isRepeat == false)
            {
                if (n.getParent().Equals(node))
                {
                    isRepeat = true;
                }
                n = n.getParent();
            }

            return isRepeat;
        }

        //pop node
        private Node pop()
        {
            Node n = stack.Pop();
            nodesPoppedOff++;
            return n;
        }
    }
}
