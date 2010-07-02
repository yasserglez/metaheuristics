
using System;

namespace Metaheuristics
{
	public class DiscreteTS4TSP : DiscreteTS
	{
		public TSPInstance Instance { get; protected set; }
		public  double RclTreshold { get; protected set; }
		
		public DiscreteTS4TSP (TSPInstance instance, double rclTreshold, int tabuListLength, int neighborChecks) 
			: base(tabuListLength, neighborChecks)
		{
			Instance = instance;
			RclTreshold = rclTreshold;
		}
		
		protected override int[] InitialSolution ()
		{
			int[] res = new int[Instance.NumberCities];
			TSPUtils.GRCSolution(Instance, res, RclTreshold);
			return res;
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
