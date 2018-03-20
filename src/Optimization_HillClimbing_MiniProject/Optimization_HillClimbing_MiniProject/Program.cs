using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimization_HillClimbing_MiniProject
{
    class Program
    {
        public static void Main(String[] args)
        {

            var list = new List<double>()
            {
                12.3,
                1.45,
                0.34,
                65.65,
                8.3,
                5.45,
                34.34,
                1023.65
            };

            var problem = new ReverseAckley();
            var iterations = 10;
            var neighbors = 1;
            var stepsize = 0.01;

            UpdateTitle(problem.GetType().Name, iterations, neighbors, stepsize);

            var test = new SimpleHillClimbing(list, problem);
            var optima = test.FindOptima(iterations, neighbors, stepsize);
            Console.WriteLine("Solutions generated: " + test.SolutionsGenerated + ". Best fitness: " + SimpleHillClimbing.PointAsString(optima) + " has value " + problem.Eval(optima));
        }

        private static void UpdateTitle(string problem, int iterations, int neighbors, double stepsize)
        {
            Console.Title = $"{problem} | {iterations} iterations | {neighbors} neighbors | {stepsize} stepsize";
        }
    }
}
