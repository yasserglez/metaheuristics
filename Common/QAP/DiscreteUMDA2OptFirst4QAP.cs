using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class DiscreteUMDA2OptFirst4QAP : DiscreteUMDA
	{
		public QAPInstance Instance { get; protected set; }
		
		public DiscreteUMDA2OptFirst4QAP(QAPInstance instance, int popSize, 
		                                 double truncFactor, int[] lowerBounds, 
		                                 int[] upperBounds)
			: base(popSize, truncFactor, lowerBounds, upperBounds)
		{
			Instance = instance;
			RepairEnabled = true;
			LocalSearchEnabled = true;
		}
		
		protected override void Repair(int[] individual)
		{
			QAPUtils.Repair(Instance, individual);
		}
		
		protected override void LocalSearch(int[] individual)
		{
			QAPUtils.LocalSearch2OptFirst(Instance, individual);
		}
		
		protected override double Fitness(int[] individual)
		{
			return QAPUtils.Fitness(Instance, individual);
		}
	}
}
