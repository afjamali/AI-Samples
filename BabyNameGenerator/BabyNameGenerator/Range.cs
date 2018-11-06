using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyNameGenerator
{
    //This class holds the beginning and end range for each letter's probability
    class Range
    {
        private double begRange;
        private double endRange;

        public Range(double begRange, double endRange)
        {
            this.begRange = begRange;
            this.endRange = endRange;
        }

        public double getBegRange() { return begRange; }
        public double getEndRange() { return endRange; }
    }
}
