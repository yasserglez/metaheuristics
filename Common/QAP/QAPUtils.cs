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
		
		// Implementation of the 2-opt (first improvement) local search algorithm.
		public static void LocalSearch2OptFirst(QAPInstance instance, int[] assignment)
		{
			int tmp;
			double currentFitness, bestFitness;

			bestFitness = Fitness(instance, assignment);			
			for (int j = 1; j < assignment.Length; j++) {
				for (int i = 0; i < j; i++) {
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
		
		// Implementation of the 2-opt (best improvement) local search algorithm.
		public static void LocalSearch2OptBest(QAPInstance instance, int[] assignment)
		{
			int tmp;
			int firstSwapItem = 0, secondSwapItem = 0;
			double currentFitness, bestFitness;
			
			bestFitness = Fitness(instance, assignment);			
			for (int j = 1; j < assignment.Length; j++) {
				for (int i = 0; i < j; i++) {
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
			
			// Use the best assignment.
			if (firstSwapItem != secondSwapItem) {
				tmp = assignment[firstSwapItem];
				assignment[firstSwapItem] = assignment[secondSwapItem];
				assignment[secondSwapItem] = tmp;	
			}
		}
	
		// Implementation of the GRC solution's construction algorithm.
		public static void GRCSolution(QAPInstance instance, int[] assigment, double rclThreshold)
		{
			int numFacilities = instance.NumberFacilities;
			int totalFacilities = numFacilities;
			int index = 0;
			double best = 0;
			double cost = 0;
			int facility = 0;
			// Restricted Candidate List.
			SortedList<double, int> rcl = new SortedList<double, int>();
			// Available cities.
			bool[] assigned = new bool[numFacilities];
			
			assigment[0] = Statistics.RandomDiscreteUniform(0, numFacilities-1);
			assigned[assigment[0]] = true;
			index++;
			numFacilities --;
			
			while (numFacilities > 0) {
				rcl = new SortedList<double, int>();
				for (int i = 0; i < totalFacilities; i++) {
					if (!assigned[i]) {
						cost = 0;
						for (int j = 0; j < index; j++) {
							cost += instance.Distances[j,index] * instance.Flows[assigment[j],i];
						}
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
				}
				facility = rcl.Values[Statistics.RandomDiscreteUniform(0, rcl.Count-1)];
				assigned[facility] = true;
				assigment[index] = facility;
				index++;
				numFacilities--;
			}
		}
	
	}
}