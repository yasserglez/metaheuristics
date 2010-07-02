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
			
			for (int subset = 0; subset < instance.NumberSubsets; subset++) {
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
	
		// Implementation of the Tabu Movement of two movements.
		public static Tuple<int, int> GetTabu(int[] source, int[] destiny)
		{
			Tuple<int, int> tabu = new Tuple<int, int>(-1, -1);
			
			for (int i = 0; i < source.Length; i++) {
				if (source[i] != destiny[i]) {
					tabu.Val1 = i;
					tabu.Val2 = destiny[i];
					break;
				}
			}
			
			return tabu;
		}
		
		// Implementation of the GRC solution's construction algorithm.
		public static void GRCSolution(SPPInstance instance, int[] assigment, double rclThreshold)
		{
			int numItems = instance.NumberItems;
			int numSets = instance.NumberSubsets;
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
					cost = Math.Abs((setWeigths[i] + instance.ItemsWeight[index]) - instance.SubsetsWeight[i]);
					if(rcl.Count == 0) {
						best = cost;
						rcl.Add(cost, i);
					}
					else if(cost < best) {
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
		
		public static int[] RandomSolution(SPPInstance instance)
		{
			int[] solution = new int[instance.NumberItems];
			
			for (int i = 0; i < instance.NumberItems; i++) {
				solution[i] = Statistics.RandomDiscreteUniform(0, instance.NumberSubsets - 1);
			}
			
			return solution;
		}
		
		public static int[] GetNeighbor(SPPInstance instance, int[] solution)
		{
			int[] neighbor = new int[instance.NumberItems];
			int index = Statistics.RandomDiscreteUniform(0, solution.Length - 1);
			int oldSubset = solution[index];
			int newSubset = oldSubset;
			while (newSubset == oldSubset) {
				newSubset = Statistics.RandomDiscreteUniform(0, instance.NumberSubsets - 1);
			}
			for (int i = 0; i < solution.Length; i++) {
				if (i == index) {
					neighbor[i] = newSubset;
				}
				else {
					neighbor[i] = solution[i];
				}
			}
			
			return neighbor;
		}

		public static double Distance(SPPInstance instance, int[] a, int[] b)
		{
			double distance = 0;
			
			for (int i = 0; i < a.Length; i++) {
				if (a[i] != b[i]) {
					distance += 1;
				}
			}
			
			return distance;
		}		
	}
}
