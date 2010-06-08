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
	
		// Implementation of the GRC solution's construction algorithm.
		public static void GRCSolution(SPPInstance instance, int[] assigment, double rclThreshold)
		{
			int numItems = instance.NumberItems;
			int numSets = instance.NumberSubsets;
			int totalItems = numItems;
			int index = 0;
			double best = 0;
			double cost = 0;
			int setItem = 0;
			double[] setWeigths = new double[instance.NumberSubsets];
			// Restricted Candidate List.
			SortedList<double, int> rcl = new SortedList<double, int>();
			
			assigment[0] = Statistics.RandomDiscreteUniform(0, numSets-1);
			setWeigths[assigment[0]] += instance.ItemsWeight[0];
			index++;
			numItems --;
			
			while (numItems > 0) {
				rcl = new SortedList<double, int>();
				for (int i = 0; i < numSets; i++) {
					cost = Math.Abs(setWeigths[i] + instance.ItemsWeight[index] - instance.SubsetsWeight[i]);
					if(rcl.Count == 0) {	
						best = cost;
						rcl.Add(cost, i);
					}
					else if( cost < best) {
						// The new assignment is the new best;
						best = cost;
						for (int j = rcl.Count-1; j > 0; j--) {
							if (rcl.Keys[j] > rclThreshold * best) {
								rcl.RemoveAt(j);
							}
							else {
								break;
							}
						}
						rcl.Add(cost, i);
					}
					else if (cost < rclThreshold * best) {
						// The new assigment is a mostly good candidate.
						rcl.Add(cost, i);
					}							
				}
				setItem = rcl.Values[Statistics.RandomDiscreteUniform(0, rcl.Count-1)];
				assigment[index] = setItem;
				setWeigths[setItem] += instance.ItemsWeight[index];
				index++;
				numItems--;
			}
		}
	}
}
