
using System;

namespace Metaheuristics
{
	public class DiscreteHMTSwGRASP2OptFirst4QAP : DiscreteTS
	{
		public QAPInstance Instance { get; protected set; }
		public DiscreteGRASP GRASP { get; protected set; }
		public int GRASPIterations { get; protected set; }
		
		public DiscreteHMTSwGRASP2OptFirst4QAP(QAPInstance instance, double rclThreshold, int graspIterations, int tabuListLength, int neighborChecks) 
			: base(tabuListLength, neighborChecks)
		{
			GRASP = new DiscreteGRASP2OptFirst4QAP(instance, rclThreshold);
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
