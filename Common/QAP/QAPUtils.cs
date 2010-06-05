using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public static class QAPUtils
	{
		public static double Fitness(QAPInstance instance, int[] assignment)
		{
			double cost = 0;
			
			for (int i = 1; i < instance.NumberFacilities; i++) {
				for (int j = 0; j < instance.NumberFacilities; j++) {
					cost += instance.Distances[i,j] * instance.Flows[assignment[i],assignment[j]];
				}
			}
			
			return cost;
		}
		
		public static void Repair(QAPInstance instance, int[] individual)
		{
			int facilitiesCount = 0;
			bool[] facilitiesUsed = new bool[instance.NumberFacilities];
			bool[] facilitiesRepeated = new bool[instance.NumberFacilities];
				
			// Get information to decide if the individual is valid.
			for (int i = 0; i < instance.NumberFacilities; i++) {
				if (!facilitiesUsed[individual[i]]) {
					facilitiesCount += 1;
					facilitiesUsed[individual[i]] = true;
				}
				else {
					facilitiesRepeated[i] = true;
				}
			}
				
			// If the individual is invalid, make it valid.
			if (facilitiesCount != instance.NumberFacilities) {
				for (int i = 0; i < facilitiesRepeated.Length; i++) {
					if (facilitiesRepeated[i]) {
						int count = Statistics.RandomDiscreteUniform(1, instance.NumberFacilities - facilitiesCount);
						for (int f = 0; f < facilitiesUsed.Length; f++) {
							if (!facilitiesUsed[f]) {
								count -= 1;
								if (count == 0) {
									individual[i] = f;
									facilitiesRepeated[i] = false;
									facilitiesUsed[f] = true;
									facilitiesCount += 1;
									break;
								}
							}
						}							
					}
				}
			}				
		}
	}
}