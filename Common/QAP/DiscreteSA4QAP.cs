using System;

namespace Metaheuristics
{
	public class DiscreteSA4QAP : DiscreteSA
	{
		public QAPInstance Instance { get; protected set; }
		
		public DiscreteSA4QAP(QAPInstance instance)
			: base(instance.NumberFacilities * (instance.NumberFacilities - 1), 0.95)
		{
			Instance = instance;
		}
		
		protected override double Fitness(int[] solution)
		{
			return QAPUtils.Fitness(Instance, solution);
		}
		
		protected override int[] InitialSolution()
		{
			return QAPUtils.RandomSolution(Instance);
		}
		
		protected override int[] GetNeighbor(int[] solution)
		{
			return QAPUtils.GetNeighbor(Instance, solution);
		}
	}
}
