using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simacogo
{
    class State
    {
        //declare variables
        private Player player;
        private int scoreMax;
        private int scoreMin;
        private int action;
        private string[,] currentBoard;

        //initialize board and state
        public State(string[,] b, Player player)
        {
            currentBoard = b;
            this.player = player;
        }

        public string[,] getcurrentBoard()
        {
            return currentBoard;
        }

        public Player getPlayer()
        {
            return player;
        }
        
        //return player label
        public string getMark()
        {
            return player == Player.MAX ? "X" : "O";
        }

        public int getAction()
        {
            return action;
        }

        //sets the column number
        public void setAction(int m)
        {
            action = m;
        }

        //stores Max's score
        public void setMaxScore(int s)
        {
            scoreMax = s;
        }

        //stores MIN's score
        public void setMinScore(int s)
        {
            scoreMin = s;
        }

        public int getMaxScore()
        {
            return scoreMax;
        }

        public int getMinScore()
        {
            return scoreMin;
        }

        //When ply is reached, this function is called to determine heuristic score
        public int evaluate()
        {
            int[] scores = Board.getScores(currentBoard);

            setMaxScore(scores[Board.SCORE_INDEX_MAX]);
            setMinScore(scores[Board.SCORE_INDEX_MIN]);

            return scoreMax - scoreMin;
        }

        //tostring function wich displays board in this state
        public override string ToString()
        {
            string result = "";
            int colCounter = 1;
            result += " -------------------------------------\n";
            foreach (string i in currentBoard)
            {
                string space = i == null ? " |  " : " | ";
                if (colCounter > 9) colCounter = 1;
                result += colCounter == 9 ? (space + i + " |\n" + " -------------------------------------\n") : (space + i);
                colCounter++;
            }
            return result;
        }
    }
}
