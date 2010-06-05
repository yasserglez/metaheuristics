using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class DiscreteUMDA4SPP : DiscreteUMDA
	{
		public SPPInstance Instance { get; protected set; }
		
		public DiscreteUMDA4SPP(SPPInstance instance, int popSize, double truncFactor, int[] lowerBounds, int[] upperBounds)
			: base(popSize, truncFactor, lowerBounds, upperBounds)
		{
			Instance = instance;
		}
		
		protected override double Fitness(int[] individual)
		{
			return SPPUtils.Fitness(Instance, individual);
		}
	}
}
