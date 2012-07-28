using System;

namespace Metaheuristics
{
	public class DiscreteTS4QAP : DiscreteTS
	{
		public QAPInstance Instance { get; protected set; }
		public  double RclTreshold { get; protected set; }
		
		public DiscreteTS4QAP (QAPInstance instance, double rclTreshold, 
		                       int tabuListLength, int neighborChecks) 
			: base(tabuListLength, neighborChecks)
		{
			Instance = instance;
			RclTreshold = rclTreshold;
		}
		
		protected override int[] InitialSolution ()
		{
			return QAPUtils.GRCSolution(Instance, RclTreshold);
		}
		
		protected override double Fitness(int[] individual)
		{
			return QAPUtils.Fitness(Instance, individual);
		}
		
		protected override Tuple<int, int> GetTabu (int[] current, int[] neighbor)
		{
			return QAPUtils.GetTabu(current, neighbor);
		}
		
		protected override int[] GetNeighbor (int[] solution)
		{
			return QAPUtils.GetNeighbor(Instance, solution);
		}
	}
}
