using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simacogo
{
    class Node
    {
        //declare variables
        private int depth;
        private Node parent;
        private List<Node> children;
        private State state;

        //constructor for new node
        public Node(Node parent, State state)
        {
            this.parent = parent;
            this.state = state;            
            setDepth();

            children = new List<Node>();
        }

        // sets the length path
        private void setDepth()
        {
            Node n = this;
            while (n.getParent() != null)
            {
                depth++;
                n = n.getParent();
            }
        }

        public int getDepth()
        {
            return depth;
        }

        //gets the nodes current state
        public State getState()
        {
            return state;
        }

        //gets the nodes state of whos turn to move
        public Player getPlayer()
        {
            return this.getState().getPlayer();
        }

        public Node getParent()
        {
            return parent;
        }

        public void addChild(Node n)
        {
            children.Add(n);
        }

        public List<Node> getSuccessors()
        {
            return children;
        }

        //generates child nodes
        public void generateSuccessors()
        {
            //foreach colum generates the next possible move and adds to children
            for (int c = 0; c < Board.COLUMNS; c++)
            {
                if (Board.isColumnAvailable(c, this.getState().getcurrentBoard()))
                {
                    //create new board
                    string[,] newBoard = Board.updateBoard(c, this.getState().getcurrentBoard(), this.getPlayer());

                    //add node with next players turn
                    Node node = new Node(this, new State(newBoard, this.getPlayer() == Player.MAX ? Player.MIN : Player.MAX));

                    //set column number
                    node.getState().setAction(c);

                    this.addChild(node);
                }
            }
        }
    }
}
