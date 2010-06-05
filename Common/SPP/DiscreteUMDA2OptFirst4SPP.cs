using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class DiscreteUMDA2OptFirst4SPP : DiscreteUMDA
	{
		public SPPInstance Instance { get; protected set; }
		
		public DiscreteUMDA2OptFirst4SPP(SPPInstance instance, int popSize, double truncFactor, int[] lowerBounds, int[] upperBounds)
			: base(popSize, truncFactor, lowerBounds, upperBounds)
		{
			Instance = instance;
			LocalSearchEnabled = true;
		}
		
		protected override void LocalSearch(int[] individual)
		{
			SPPUtils.LocalSearch2OptFirst(Instance, individual);
		}
		
		protected override double Fitness(int[] individual)
		{
			return SPPUtils.Fitness(Instance, individual);
		}
	}
}
