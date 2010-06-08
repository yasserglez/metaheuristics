using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class DiscreteGRASP2OptBest4SPP : DiscreteGRASP
	{
		public SPPInstance Instance { get; protected set; }
		
		public DiscreteGRASP2OptBest4SPP ( SPPInstance instance, double rclThreshold, int[] lowerBounds, int[] upperBounds)
			:base(rclThreshold, lowerBounds, upperBounds)
		{
			Instance = instance;
		}
		
		protected override double Fitness (int[] solution)
		{
			return SPPUtils.Fitness(Instance, solution);
		}
		
		protected override void GRCSolution (int[] solution)
		{
			SPPUtils.GRCSolution(Instance, solution, RCLThreshold);
		}
		
		protected override void LocalSearch (int[] solution)
		{
			SPPUtils.LocalSearch2OptBest(Instance, solution);
		}
	}
}
