using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EightPuzzle
{
    class Heuristic
    {
        //code to be used for determining h
        public static int Code { get; set; }

        //find h
        public static int getHeuristic(State state)
        {
            switch (Heuristic.Code)
            {
                case 1:
                    return state.getOutOfPlace();
                case 2:
                    return state.getManhattanDistance();
                case 3:
                    return state.getOutOfPlace() + state.getManhattanDistance();
            }
            return 0;
        }
    }

}
