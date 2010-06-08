using System;

namespace Metaheuristics
{
	public class DiscreteGRASP2OptFirst4QAP: DiscreteGRASP
	{
		public QAPInstance Instance { get; protected set; }
		
		public DiscreteGRASP2OptFirst4QAP ( QAPInstance instance, double rclThreshold, int[] lowerBounds, int[] upperBounds)
			:base(rclThreshold, lowerBounds, upperBounds)
		{
			Instance = instance;
		}
		
		protected override double Fitness (int[] solution)
		{
			return QAPUtils.Fitness(Instance, solution);
		}
		
		protected override void GRCSolution (int[] solution)
		{
			QAPUtils.GRCSolution(Instance, solution, RCLThreshold);
		}
		
		protected override void LocalSearch (int[] solution)
		{
			QAPUtils.LocalSearch2OptFirst(Instance, solution);
		}
	}
}
