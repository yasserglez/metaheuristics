using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public static class TSPUtils
	{
		public static double Fitness(TSPInstance instance, int[] path)
		{
			double cost = 0;
			
			for (int i = 1; i < path.Length; i++) {
				cost += instance.Costs[path[i-1],path[i]];
			}
			cost += instance.Costs[path[path.Length-1],path[0]];
			
			return cost;
		}
		
		public static void Repair(TSPInstance instance, int[] individual)
		{
			int visitedCitiesCount = 0;
			bool[] visitedCities = new bool[instance.NumberCities];
			bool[] repeatedPositions = new bool[instance.NumberCities];
				
			// Get information to decide if the individual is valid.
			for (int i = 0; i < instance.NumberCities; i++) {
				if (!visitedCities[individual[i]]) {
					visitedCitiesCount += 1;
					visitedCities[individual[i]] = true;
				}
				else {
					repeatedPositions[i] = true;
				}
			}
				
			// If the individual is invalid, make it valid.
			if (visitedCitiesCount != instance.NumberCities) {
				for (int i = 0; i < repeatedPositions.Length; i++) {
					if (repeatedPositions[i]) {
						int count = Statistics.RandomDiscreteUniform(1, instance.NumberCities - visitedCitiesCount);
						for (int c = 0; c < visitedCities.Length; c++) {
							if (!visitedCities[c]) {
								count -= 1;
								if (count == 0) {
									individual[i] = c;
									repeatedPositions[i] = false;
									visitedCities[c] = true;
									visitedCitiesCount += 1;
									break;
								}
							}
						}							
					}
				}
			}
		}
		
		// Implementation of the 2-opt (first improvement) local search algorithm.
		public static void LocalSearch2OptFirst(TSPInstance instance, int[] path)
		{
			int tmp;
			double currentFitness, bestFitness;

			bestFitness = Fitness(instance, path);			
			for (int j = 1; j < path.Length; j++) {
				for (int i = 0; i < j; i++) {
					// Swap the items.
					tmp = path[j];
					path[j] = path[i];
					path[i] = tmp;
					
					// Evaluate the fitness of this new solution.
					currentFitness = Fitness(instance, path);
					if (currentFitness < bestFitness) {
						return;
					}
					
					// Undo the swap.
					tmp = path[j];
					path[j] = path[i];
					path[i] = tmp;
				}
			}
		}
		
		// Implementation of the 2-opt (best improvement) local search algorithm.
		public static void LocalSearch2OptBest(TSPInstance instance, int[] path)
		{
			int tmp;
			int firstSwapItem = 0, secondSwapItem = 0;
			double currentFitness, bestFitness;
			
			bestFitness = Fitness(instance, path);			
			for (int j = 1; j < path.Length; j++) {
				for (int i = 0; i < j; i++) {
					// Swap the items.
					tmp = path[j];
					path[j] = path[i];
					path[i] = tmp;
					
					// Evaluate the fitness of this new solution.
					currentFitness = Fitness(instance, path);
					if (currentFitness < bestFitness) {
						firstSwapItem = j;
						secondSwapItem = i;
						bestFitness = currentFitness;
					}
					
					// Undo the swap.
					tmp = path[j];
					path[j] = path[i];
					path[i] = tmp;
				}
			}
			
			// Use the best assignment.
			if (firstSwapItem != secondSwapItem) {
				tmp = path[firstSwapItem];
				path[firstSwapItem] = path[secondSwapItem];
				path[secondSwapItem] = tmp;
			}
		}		
	}
}
