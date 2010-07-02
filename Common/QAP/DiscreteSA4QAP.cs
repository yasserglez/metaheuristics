using System;

namespace Metaheuristics
{
	public class DiscreteSA4QAP : DiscreteSA
	{
		public QAPInstance Instance { get; protected set; }
		public  double RclTreshold { get; protected set; }
		
		public DiscreteSA4QAP(QAPInstance instance, double rclTreshold, int initialSolutions, int levelLength, double tempReduction)
			: base(initialSolutions, levelLength, tempReduction)
		{
			Instance = instance;
			RclTreshold = rclTreshold;
		}
		
		protected override double Fitness(int[] solution)
		{
			return QAPUtils.Fitness(Instance, solution);
		}
		
		protected override int[] InitialSolution()
		{
			int[] res = new int[Instance.NumberFacilities];
			QAPUtils.GRCSolution(Instance, res, RclTreshold);
			return res;
		}
		
		protected override int[] GetNeighbor(int[] solution)
		{
			return QAPUtils.GetNeighbor(Instance, solution);
		}
	}
}
