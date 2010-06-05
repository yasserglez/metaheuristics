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
		
		// Implementation of the 2-opt (first improvement) local search algorithm.
		public static void LocalSearch2OptFirst(SPPInstance instance, int[] assignment)
		{
			int tmp;
			double currentFitness, bestFitness;

			bestFitness = Fitness(instance, assignment);			
			for (int j = 1; j < assignment.Length; j++) {
				for (int i = 0; i < j; i++) {
					if (assignment[i] != assignment[j]) {
						// Swap the items.
						tmp = assignment[j];
						assignment[j] = assignment[i];
						assignment[i] = tmp;
						
						// Evaluate the fitness of this new solution.
						currentFitness = Fitness(instance, assignment);
						if (currentFitness < bestFitness) {
							return;
						}
						
						// Undo the swap.
						tmp = assignment[j];
						assignment[j] = assignment[i];
						assignment[i] = tmp;
					}
				}
			}
		}
		
		// Implementation of the 2-opt (best improvement) local search algorithm.
		public static void LocalSearch2OptBest(SPPInstance instance, int[] assignment)
		{
			int tmp;
			int firstSwapItem = 0, secondSwapItem = 0;
			double currentFitness, bestFitness;
			
			bestFitness = Fitness(instance, assignment);			
			for (int j = 1; j < assignment.Length; j++) {
				for (int i = 0; i < j; i++) {
					if (assignment[i] != assignment[j]) {
						// Swap the items.
						tmp = assignment[j];
						assignment[j] = assignment[i];
						assignment[i] = tmp;
						
						// Evaluate the fitness of this new solution.
						currentFitness = Fitness(instance, assignment);
						if (currentFitness < bestFitness) {
							firstSwapItem = j;
							secondSwapItem = i;
							bestFitness = currentFitness;
						}
						
						// Undo the swap.
						tmp = assignment[j];
						assignment[j] = assignment[i];
						assignment[i] = tmp;
					}
				}
			}
			
			// Use the best assignment.
			if (firstSwapItem != secondSwapItem) {
				tmp = assignment[firstSwapItem];
				assignment[firstSwapItem] = assignment[secondSwapItem];
				assignment[secondSwapItem] = tmp;
			}
		}		
	}
}
