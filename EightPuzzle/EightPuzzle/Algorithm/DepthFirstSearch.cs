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
    class DepthFirstSearch
    {
        //declare variables
        private Board board;
        Stack<Node> stack;
        int nodesPoppedOff;
        int maxQueue;

        public DepthFirstSearch(int[] d, int[] g)
        {
            board = new Board(d, g);
            stack = new Stack<Node>();
        }

        //run the search algorithm
        public void run()
        {
            //add root to stack
            stack.Push(new Node(null, new State(board.getDifficulty()), NodeAction.NA,0,0,0));

            while (stack.Count != 0)
            {
                //calls method to pop node
                Node node = pop();

                //check wether is goal
                if (!board.isGoal(node))
                {
                    //generate successors
                    node.generateSuccessors();
                    List<Node> successors = new List<Node>();

                    //if node no already added to stack, add node
                    foreach (Node n in node.getSuccessors())
                        if (!previouslyAdded(n))
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
                    break;
                }
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
