using System;

namespace Metaheuristics
{
	public class DiscreteHMSAwGRASP2OptFirst4QAP : DiscreteSA
	{
		public QAPInstance Instance { get; protected set; }
		public DiscreteGRASP GRASP { get; protected set; }
		public int GRASPIterations { get; protected set; }
		
		public DiscreteHMSAwGRASP2OptFirst4QAP(QAPInstance instance, double rclThreshold, int graspIterations, int initialSolutions, int levelLength, double tempReduction)
			: base(initialSolutions, levelLength, tempReduction)
		{
			GRASP = new DiscreteGRASP2OptFirst4QAP(instance, rclThreshold);
			Instance = instance;
			GRASPIterations = graspIterations;
		}
		
		protected override double Fitness(int[] solution)
		{
			return QAPUtils.Fitness(Instance, solution);
		}
		
		protected override int[] InitialSolution()
		{
			GRASP.Run(GRASPIterations, RunType.IterationsLimit);
			return GRASP.BestSolution;
		}
		
		protected override int[] GetNeighbor(int[] solution)
		{
			return QAPUtils.GetNeighbor(Instance, solution);
		}
	}
}
