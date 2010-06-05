using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public static class SPPUtils
	{
		public static double Fitness(SPPInstance instance, int[] assignment)
		{
			double deviation = 0;
			
			for (int subset = 1; subset < instance.NumberSubsets; subset++) {
				double subsetWeight = 0;
				for (int item = 0; item < instance.NumberItems; item++) {
					if (subset == assignment[item]) {
						subsetWeight += instance.ItemsWeight[item];
					}
				}
				deviation += Math.Abs(subsetWeight - instance.SubsetsWeight[subset]);
			}
			
			return deviation;
		}		
	}
}
