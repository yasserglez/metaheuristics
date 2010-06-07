
using System;

namespace Metaheuristics
{


	public class DiscreteGenetic4QAP : DiscreteGenetic
	{
		public QAPInstance Instance { get; protected set; }
		
		public DiscreteGenetic4QAP (QAPInstance instance, int popSize, double mutationProbability,
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
			return QAPUtils.Cost(Instance, individual);
		}
	}
}
