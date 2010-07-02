using System;

namespace Metaheuristics
{
	public class DiscreteGRASP2OptFirst4TSP: DiscreteGRASP
	{
		
		public TSPInstance Instance { get; protected set; }
		
		
		public DiscreteGRASP2OptFirst4TSP (TSPInstance instance, double rclThreshold)
			:base(rclThreshold)
		{
			Instance = instance;
		}
		
		protected override double Fitness (int[] solution)
		{
			return TSPUtils.Fitness(Instance, solution);
		}
		
		protected override int[] GRCSolution ()
		{
			return TSPUtils.GRCSolution(Instance, RCLThreshold);
		}
		
		protected override void LocalSearch (int[] solution)
		{
			TSPUtils.LocalSearch2OptFirst(Instance, solution);
		}
		
		
		
	}
}
