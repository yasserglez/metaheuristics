using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class DiscreteGRASP2OptBest4SPP : DiscreteGRASP
	{
		public SPPInstance Instance { get; protected set; }
		
		public DiscreteGRASP2OptBest4SPP ( SPPInstance instance, double rclThreshold)
			:base(rclThreshold)
		{
			Instance = instance;
		}
		
		protected override double Fitness (int[] solution)
		{
			return SPPUtils.Fitness(Instance, solution);
		}
		
		protected override int[] GRCSolution ()
		{
			return SPPUtils.GRCSolution(Instance, RCLThreshold);
		}
		
		protected override void LocalSearch (int[] solution)
		{
			SPPUtils.LocalSearch2OptBest(Instance, solution);
		}
	}
}
