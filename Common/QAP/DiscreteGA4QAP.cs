using System;

namespace Metaheuristics
{
	public class DiscreteGA4QAP : DiscreteGA
	{
		public QAPInstance Instance { get; protected set; }
		
		public DiscreteGA4QAP (QAPInstance instance, int popSize, double mutationProbability,
		                       int[] lowerBounds, int[] upperBounds)
			: base(popSize, mutationProbability, lowerBounds, upperBounds)
		{
			Instance = instance;
			RepairEnabled = true;
		}
		
		protected override void Repair(int[] individual)
		{
			QAPUtils.Repair(Instance, individual);
		}
		
		protected override double Fitness(int[] individual)
		{
			return QAPUtils.Fitness(Instance, individual);
		}
		
		protected override int[] InitialSolution ()
		{
			return QAPUtils.RandomSolution(Instance);
		}

	}
}
