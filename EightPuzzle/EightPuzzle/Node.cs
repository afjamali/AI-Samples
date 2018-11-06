using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EightPuzzle
{
    class Node
    {
        private NodeAction action;
        private int depth;
        private int gCost;
        private int cost;
        private int hCost;
        private int fCost;
        public bool expanded { get; set; }
        public Node parent;
        public List<Node> children;
        public State state;
                
        //constructor for new node
        public Node(Node parent, State state, NodeAction action, int cost, int pathCost, int hCost)
        {
            this.parent = parent;
            this.state = state;
            this.action = action;
            this.cost = cost;
            this.gCost = pathCost;
            this.hCost = hCost;
            fCost = hCost + pathCost; //calculating fCost for A* algo
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

        //gets the estimated cost to goal
        public int getHCost()
        {
            return hCost;
        }

        //gets the path cost
        public int getGCost()
        {
            return gCost;
        }

        //gets the nodes current state
        public State getState()
        {
            return state;
        }

        //gets the nodes action: up left down or right
        public NodeAction getAction()
        {
            return action;
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
            //get index of blank space
            int blankIndx = Board.getBlankIndex(this.getState().getcurrentBoard());

            // move blank left
            if (blankIndx != 0 && blankIndx != 3 && blankIndx != 6)
            {
                int[] newBoard = Board.updateBoard(this.getState().getcurrentBoard(), NodeAction.LEFT); //creates a new board 
                int cost = Board.getTileCost(this.getState().getcurrentBoard(), NodeAction.LEFT); //gets cost
                State state = new State(newBoard);//create state for child
                int h = Heuristic.getHeuristic(state);//find h
                this.addChild(new Node(this, state, NodeAction.LEFT, cost, cost + this.getGCost(), h));//create new child and add info to child
            }

            // move blank right
            if (blankIndx != 2 && blankIndx != 5 && blankIndx != 8)
            {
                int[] newBoard = Board.updateBoard(this.getState().getcurrentBoard(), NodeAction.RIGHT);
                int cost = Board.getTileCost(this.getState().getcurrentBoard(), NodeAction.RIGHT);
                State state = new State(newBoard);
                int h = Heuristic.getHeuristic(state);
                this.addChild(new Node(this, state, NodeAction.RIGHT, cost, cost + this.getGCost(), h));
            }

            // move blank up
            if (blankIndx != 0 && blankIndx != 1 && blankIndx != 2)
            {
                int[] newBoard = Board.updateBoard(this.getState().getcurrentBoard(), NodeAction.UP);
                int cost = Board.getTileCost(this.getState().getcurrentBoard(), NodeAction.UP);
                State state = new State(newBoard);
                int h = Heuristic.getHeuristic(state);
                this.addChild(new Node(this, state, NodeAction.UP, cost, cost + this.getGCost(), h));
            }

            // move blank down
            if (blankIndx != 6 && blankIndx != 7 && blankIndx != 8)
            {
                int[] newBoard = Board.updateBoard(this.getState().getcurrentBoard(), NodeAction.DOWN);
                int cost = Board.getTileCost(this.getState().getcurrentBoard(), NodeAction.DOWN);
                State state = new State(newBoard);
                int h = Heuristic.getHeuristic(state);
                this.addChild(new Node(this, state, NodeAction.DOWN, cost, cost + this.getGCost(), h));
            }

            this.expanded = true;
        }

        public int getCost()
        {
            return cost;
        }

        //gets fCost: g + h
        public int getFCost()
        {
            return fCost;
        }

        //equals method to check wether state of 2 boards are equal
        public bool Equals(Node n)
        {
            return n.getState().getcurrentBoard().SequenceEqual(this.getState().getcurrentBoard());
        }
    }
}
