
using System;

namespace Metaheuristics
{
	public class DiscreteTS4SPP : DiscreteTS
	{
		public SPPInstance Instance { get; protected set; }
		public  double RclTreshold { get; protected set; }
		
		public DiscreteTS4SPP (SPPInstance instance, double rclTreshold, int tabuListLength, int neighborChecks) 
			: base(tabuListLength, neighborChecks)
		{
			Instance = instance;
			RclTreshold = rclTreshold;
		}
		
		protected override int[] InitialSolution ()
		{
			int[] res = new int[Instance.NumberItems];
			SPPUtils.GRCSolution(Instance, res, RclTreshold);
			return res;
		}
		
		protected override double Fitness(int[] individual)
		{
			return SPPUtils.Fitness(Instance, individual);
		}
		
		protected override Tuple<int, int> GetTabu (int[] current, int[] neighbor)
		{
			return SPPUtils.GetTabu(current, neighbor);
		}
		
		protected override int[] GetNeighbor (int[] solution)
		{
			return SPPUtils.GetNeighbor(Instance, solution);
		}
	}
}
