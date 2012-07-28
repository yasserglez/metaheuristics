using System;

namespace Metaheuristics
{
	public class DiscreteHMSAwGRASP2OptBest4TSP : DiscreteSA
	{
		public TSPInstance Instance { get; protected set; }
		public DiscreteGRASP GRASP { get; protected set; }
		public int GRASPIterations { get; protected set; }
		
		public DiscreteHMSAwGRASP2OptBest4TSP(TSPInstance instance, double rclThreshold, 
		                                      int graspIterations, int initialSolutions, 
		                                      int levelLength, double tempReduction)
			: base(initialSolutions, levelLength, tempReduction)
		{
			GRASP = new DiscreteGRASP2OptBest4TSP(instance, rclThreshold);
			Instance = instance;
			GRASPIterations = graspIterations;
		}
		
		protected override double Fitness(int[] solution)
		{
			return TSPUtils.Fitness(Instance, solution);
		}
		
		protected override int[] InitialSolution()
		{
			GRASP.Run(GRASPIterations, RunType.IterationsLimit);
			return GRASP.BestSolution;
		}
		
		protected override int[] GetNeighbor(int[] solution)
		{
			return TSPUtils.GetNeighbor(Instance, solution);
		}
	}
}
