using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimization_HillClimbing_MiniProject
{
    public class P1 : Problem
    {
        public P1() : base()
        {
            MaxValues = new List<double>()
            {
                2.0,
                2.0
            };

            MinValues = new List<double>()
            {
                -2.0,
                -2.0
            };

            Dimensions = 2;
        }

        public override double Eval(List<double> paramVals)
        {
            var x = paramVals[0];
            var y = paramVals[1];
            return Math.Pow(Math.E, -(Math.Pow(x, 2.0) + Math.Pow(y, 2.0)));
        }
    }
}
