using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class DiscreteUMDA4QAP : DiscreteUMDA
	{
		public QAPInstance Instance { get; protected set; }
		
		public DiscreteUMDA4QAP(QAPInstance instance, int popSize, double truncFactor, int[] lowerBounds, int[] upperBounds)
			: base(popSize, truncFactor, lowerBounds, upperBounds)
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
	}
}
