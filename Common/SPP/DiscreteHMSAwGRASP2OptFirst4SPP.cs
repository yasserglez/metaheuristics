using System;

namespace Metaheuristics
{
	public class DiscreteHMSAwGRASP2OptFirst4SPP : DiscreteSA
	{
		public SPPInstance Instance { get; protected set; }
		public DiscreteGRASP GRASP { get; protected set; }
		public int GRASPIterations { get; protected set; }
		
		public DiscreteHMSAwGRASP2OptFirst4SPP(SPPInstance instance, double rclThreshold, int graspIterations, int initialSolutions, int levelLength, double tempReduction)
			: base(initialSolutions, levelLength, tempReduction)
		{
			GRASP = new DiscreteGRASP2OptFirst4SPP(instance, rclThreshold);
			Instance = instance;
			GRASPIterations = graspIterations;
		}
		
		protected override double Fitness(int[] solution)
		{
			return SPPUtils.Fitness(Instance, solution);
		}
		
		protected override int[] InitialSolution()
		{
			GRASP.Run(GRASPIterations, RunType.IterationsLimit);
			return GRASP.BestSolution;
		}
		
		protected override int[] GetNeighbor(int[] solution)
		{
			return SPPUtils.GetNeighbor(Instance, solution);
		}
	}
}
