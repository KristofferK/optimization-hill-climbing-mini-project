using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimization_HillClimbing_MiniProject
{
    public class SimpleHillClimbing
    {
        private List<double> universe;
        private static Random random = new Random();
        private Problem problem;

        public int SolutionsGenerated { get; private set; }

        public SimpleHillClimbing(List<double> universe, Problem problem)
        {
            this.universe = universe;
            this.problem = problem;
            Console.WriteLine("Initialized using " + problem.GetType().Name);
        }

        public List<double> FindOptima(int iterations, int neighbors, double stepsize)
        {
            SolutionsGenerated = 0;

            Console.WriteLine($"Finding optima using {iterations} iterations and {neighbors} neighbors");
            var globalBestPoint = new List<double>();
            double? globalBestPointValue = null;

            for (var i = 0; i < iterations; i++)
            {
                var optima = _FindOptima(neighbors, stepsize);
                Console.WriteLine($"Iteration {i+1} found optima: {optima.Item2}");
                if (globalBestPointValue == null || optima.Item2 > globalBestPointValue)
                {
                    globalBestPointValue = optima.Item2;
                    globalBestPoint = optima.Item1;
                    Console.WriteLine($"Global optima updated to {globalBestPointValue} {PointAsString(globalBestPoint)}");
                }
                else
                {
                    Console.WriteLine("That's worse than global optima. Not updating");
                }
                Console.WriteLine();
            }
            
            return globalBestPoint;
        }

        private Tuple<List<double>, double> _FindOptima(int neighbors, double stepsize)
        {
            var point = GetRandomInitialPoints();
            var fitness = problem.Eval(point);
            Console.WriteLine($"Starting at {PointAsString(point)} ({fitness})");
            var shouldContinue = true;
            while (shouldContinue)
            {
                SolutionsGenerated++;
                var newPoint = GetNeighborPoint(point, neighbors, stepsize);
                var fitnessOfNewPoint = problem.Eval(newPoint);
                if (fitnessOfNewPoint > fitness)
                {
                    Console.WriteLine($"Moving to neighbor with value {PointAsString(newPoint)} ({fitnessOfNewPoint})");
                    point = newPoint;
                    fitness = fitnessOfNewPoint;
                    continue;
                }
                Console.WriteLine("No better neighbor found in the sample");
                shouldContinue = false;
            }

            return new Tuple<List<double>, double>(point, fitness);
        }

        private List<double> GetRandomInitialPoints()
        {
            List<double> points = new List<double>();
            for (var i = 0; i < problem.Dimensions; i++)
            {
                points.Add(problem.MinValues[i] + random.NextDouble() * (problem.MaxValues[i] - problem.MinValues[i]));
            }
            return points;
        }

        private List<double> GetNeighborPoint(List<double> point, int neighbors, double stepsize)
        {
            if (neighbors < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(neighbors) + " should be non-zero and non-negative");
            }

            var bestNeighborPoint = new List<double>();
            double? bestNeighborPointValue = null;
            for (int i = 0; i < neighbors; i++)
            {
                var neighborPoint = GetNeighborPoint(point, stepsize);
                var neighborPointValue = problem.Eval(neighborPoint);

                if (bestNeighborPointValue == null || neighborPointValue > bestNeighborPointValue)
                {
                    bestNeighborPointValue = neighborPointValue;
                    bestNeighborPoint = neighborPoint;
                }
            }
            return bestNeighborPoint;
        }

        private List<double> GetNeighborPoint(List<double> point, double stepsize)
        {
            // Console.WriteLine("Starting point: " + PointAsString(point));
            var newPoint = new List<double>(point);
            for (var dimension = 0; dimension < newPoint.Count(); dimension++)
            {
                if (random.Next(0, 2) == 1)
                {
                    newPoint[dimension] += stepsize;
                }
                else
                {
                    newPoint[dimension] += stepsize * -1;
                }
                newPoint[dimension] = Math.Max(newPoint[dimension], problem.MinValues[dimension]);
                newPoint[dimension] = Math.Min(newPoint[dimension], problem.MaxValues[dimension]);
            }
            // Console.WriteLine("New point: " + PointAsString(newPoint));
            return newPoint;
        }

        public static string PointAsString(List<double> point)
        {
            return "[" + string.Join(", ", point.ToArray()) + "]";
        }
    }
}
