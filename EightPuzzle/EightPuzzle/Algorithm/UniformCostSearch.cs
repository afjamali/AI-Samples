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
    class UniformCostSearch
    {
        //declare variables
        private Board board;
        Queue<Node> queue;
        int nodesPoppedOff;
        int maxQueue;

        public UniformCostSearch(int[] d, int[] g)
        {
            board = new Board(d, g);
            queue = new Queue<Node>();
        }

        //run the search algorithm
        public void run()
        {
            //add root to queue
            queue.Enqueue(new Node(null, new State(board.getDifficulty()), NodeAction.NA, 0,0, 0));

            while (queue.Count != 0)
            {
                //calls method to pop node
                Node node = dequeue();

                //check wether is goal
                if (!board.isGoal(node))
                {
                    //generate successors
                    node.generateSuccessors();
                    List<Node> successors = new List<Node>();

                    //if node no already added to queue, add node
                    foreach (Node n in node.getSuccessors())
                        if (!previouslyAdded(n))
                            successors.Add(n);

                    if (successors.Count == 0) continue;

                    //sort the nodes based on shortest path cost
                    successors = successors.OrderBy(x => x.getGCost()).ToList();

                    //add successors to queue
                    foreach (Node s in successors)
                            queue.Enqueue(s);

                    //calculate max space
                    if (maxQueue < queue.Count) maxQueue = queue.Count;
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
        private Node dequeue()
        {            
            Node n = queue.Dequeue();
            nodesPoppedOff++;
            return n;
        }
    }
}
