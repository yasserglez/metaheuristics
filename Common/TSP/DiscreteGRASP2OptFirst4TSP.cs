using System;

namespace Metaheuristics
{
	public class DiscreteGRASP2OptFirst4TSP: DiscreteGRASP
	{
		
		public TSPInstance Instance { get; protected set; }
		
		
		public DiscreteGRASP2OptFirst4TSP (TSPInstance instance, double rclThreshold, int[] lowerBounds, int[] upperBounds)
			:base(rclThreshold, lowerBounds, upperBounds)
		{
			Instance = instance;
		}
		
		protected override double Fitness (int[] solution)
		{
			return TSPUtils.Fitness(Instance, solution);
		}
		
		protected override void GRCSolution (int[] solution)
		{
			TSPUtils.GRCSolution(Instance, solution, RCLThreshold);
		}
		
		protected override void LocalSearch (int[] solution)
		{
			TSPUtils.LocalSearch2OptFirst(Instance, solution);
		}
		
		
		
	}
}
