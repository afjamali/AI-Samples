using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simacogo
{
    class Game
    {
        //declare variables
        private Board board;
        Stack<Node> stack;

        /// <summary>
        /// sets the ply level
        /// </summary>
        int ply = 6;

        Node bestMove = null;

        public Game()
        {
            board = new Board();
            stack = new Stack<Node>();
        }

        //run the game
        public void run()
        {
            int[] scores = new int[2]; //scores for displaying player scores
            string[,] currentBoard = new string[Board.ROWS, Board.COLUMNS];
            Board.displayBoard(currentBoard);

            //check if board is full
            while (!board.isFull(currentBoard))
            {
                int col = -1; //declare column variable used to get input

                //while input is incorrect ask user for allowed inputs
                bool wrongInput = true;
                while (wrongInput)
                {
                    Console.WriteLine("Your turn!");
                    Console.WriteLine("Pick column 1-9...");

                    try
                    {
                        col = Convert.ToInt32(Console.ReadLine());
                    }
                    catch { Console.WriteLine("Wrong input!"); continue; }

                    if (wrongInput = checkInput(col) == true) continue;

                    col -= 1;

                    //if column is available add to board
                    if (Board.isColumnAvailable(col, currentBoard))
                    {
                        currentBoard = Board.updateBoard(col, currentBoard, Player.MIN);
                    }
                    else
                    {
                        wrongInput = true;
                        Console.WriteLine("Cannot insert in column beacause it is full!");
                    }
                }

                //create root node for MAX
                Node node = new Node(null, new State(currentBoard, Player.MAX));

                //Get minimax score (best move)
                int ai = minimax(node, ply, Int32.MinValue, Int32.MaxValue);

                //update board with MAX's move
                currentBoard = Board.updateBoard(bestMove.getState().getAction(), currentBoard, Player.MAX);

                //get current scores
                scores = Board.getScores(currentBoard);

                Board.displayBoard(currentBoard);
                Console.WriteLine("AI score : " + scores[Board.SCORE_INDEX_MAX] + "  Your score : " + scores[Board.SCORE_INDEX_MIN]);
                Board.output.Close();
            }
            Console.WriteLine("Game Over!");

            //check who one and display it
            whoOne(scores);
            Console.ReadLine();
        }

        // recursive method get get best move for AI
        public int minimax(Node node,int depth, int alpha, int beta)
        {
            //if leaf node, or board is full evaluate board
            if (depth == 0 || board.isFull(node.getState().getcurrentBoard()))
                return node.getState().evaluate();

            Player player = node.getPlayer();

            //generate successors
            node.generateSuccessors();
            
            if (player == Player.MAX)
            {
                //loop through children and set best utility for max
                foreach (Node child in node.getSuccessors())
                {
                    int value = minimax(child, depth -1, alpha, beta);
                    if (value > alpha)
                    {
                        alpha = value;
                        bestMove = child;
                    }

                    //cut off other children
                    if (alpha >= beta)
                        break;
                }
                return alpha;
            }
            else
            {
                //loop through children and set best utility for min
                foreach (Node child in node.getSuccessors())
                {
                    int value = minimax(child,depth -1, alpha, beta);
                    if (value < beta)
                    {
                        beta = value;
                        bestMove = child;
                    }

                    //cut off other children
                    if (alpha >= beta)
                        break;
                }
                return beta;
            }
        }

        //Checks if input is correct
        public bool checkInput(int col)
        {
            if (Board.getInputsAllowed().Contains(col))
                return false;
            Console.WriteLine("Wrong input!");
            return true;
        }

        //calculate who won
        public void whoOne(int[] scores)
        {
            if (scores[Board.SCORE_INDEX_MAX] > scores[Board.SCORE_INDEX_MIN])
                Console.WriteLine("X WON.");
            else if (scores[Board.SCORE_INDEX_MAX] < scores[Board.SCORE_INDEX_MIN])
                Console.WriteLine("YOU WON!");
            else
                Console.WriteLine("It's a draw.");
        }
    }
}
