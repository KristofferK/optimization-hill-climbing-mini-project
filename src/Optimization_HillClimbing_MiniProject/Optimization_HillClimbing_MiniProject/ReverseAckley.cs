using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimization_HillClimbing_MiniProject
{
    public class ReverseAckley : Problem
    {
        public ReverseAckley() : base()
        {
            MaxValues = new List<double>()
            {
                5.0,
                5.0
            };

            MinValues = new List<double>()
            {
                -5.0,
                -5.0
            };

            Dimensions = 2;
        }

        public override double Eval(List<double> paramVals)
        {
            var sum1 = 0.0d;
            var sum2 = 0.0d;

            for (var i = 0; i < Dimensions; i++)
            {
                sum1 += Math.Pow(paramVals[i], 2);
                sum2 += (Math.Cos(2 * Math.PI * paramVals[i]));
            }

            return -(-20.0 * Math.Exp(-0.2 * Math.Sqrt(sum1 / Dimensions)) + 20 - Math.Exp(sum2 / Dimensions)
                    + Math.Exp(1.0));
        }
    }
}
