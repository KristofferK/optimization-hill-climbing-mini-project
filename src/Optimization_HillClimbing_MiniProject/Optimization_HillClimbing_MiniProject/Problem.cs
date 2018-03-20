using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimization_HillClimbing_MiniProject
{
    public abstract class Problem
    {
        public List<double> MaxValues { get; set; }
        public List<double> MinValues { get; set; }
        public int Dimensions { get; protected set; }

        public abstract double Eval(List<double> paramVals);
    }
}
