using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class DiscreteUMDA4TSP : DiscreteUMDA
	{
		public TSPInstance Instance { get; protected set; }
		
		public DiscreteUMDA4TSP(TSPInstance instance, int popSize, 
		                        double truncFactor, int[] lowerBounds, 
		                        int[] upperBounds)
			: base(popSize, truncFactor, lowerBounds, upperBounds)
		{
			Instance = instance;
			RepairEnabled = true;
		}
		
		protected override void Repair(int[] individual)
		{
			TSPUtils.Repair(Instance, individual);
		}
		
		protected override double Fitness(int[] individual)
		{
			return TSPUtils.Fitness(Instance, individual);
		}
	}
}
