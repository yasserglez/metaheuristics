
using System;

namespace Metaheuristics
{
	public class DiscreteHMTSwGRASP2OptFirst4TSP : DiscreteTS
	{
		public TSPInstance Instance { get; protected set; }
		public DiscreteGRASP GRASP { get; protected set; }
		public int GRASPIterations { get; protected set; }
		
		public DiscreteHMTSwGRASP2OptFirst4TSP(TSPInstance instance, double rclThreshold, int graspIterations, int tabuListLength, int neighborChecks) 
			: base(tabuListLength, neighborChecks)
		{
			GRASP = new DiscreteGRASP2OptFirst4TSP(instance, rclThreshold);
			Instance = instance;
			GRASPIterations = graspIterations;
		}
		
		protected override int[] InitialSolution ()
		{
			GRASP.Run(GRASPIterations, RunType.IterationsLimit);
			return GRASP.BestSolution;
		}
		
		protected override double Fitness(int[] individual)
		{
			return TSPUtils.Fitness(Instance, individual);
		}
		
		protected override Tuple<int, int> GetTabu (int[] current, int[] neighbor)
		{
			return TSPUtils.GetTabu(current, neighbor);
		}
		
		protected override int[] GetNeighbor (int[] solution)
		{
			return TSPUtils.GetNeighbor(Instance, solution);
		}
	}
}
