using System;

namespace Metaheuristics
{
	public class DiscreteSA4SPP : DiscreteSA
	{
		public SPPInstance Instance { get; protected set; }
		public  double RclTreshold { get; protected set; }
		
		public DiscreteSA4SPP(SPPInstance instance, double rclTreshold, int initialSolutions, int levelLength, double tempReduction)
			: base(initialSolutions, levelLength, tempReduction)
		{
			Instance = instance;
			RclTreshold = rclTreshold;
		}
		
		protected override double Fitness(int[] solution)
		{
			return SPPUtils.Fitness(Instance, solution);
		}
		
		protected override int[] InitialSolution()
		{
			int[] res = new int[Instance.NumberItems];
			SPPUtils.GRCSolution(Instance, res, RclTreshold);
			return res;
		}
		
		protected override int[] GetNeighbor(int[] solution)
		{
			return SPPUtils.GetNeighbor(Instance, solution);
		}
	}
}
