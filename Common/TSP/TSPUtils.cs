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
		
		public static int[] RandomSolution(TSPInstance instance)
		{
			int[] solution = new int[instance.NumberCities];
			List<int> cities = new List<int>();
			
			for (int city = 0; city < instance.NumberCities; city++) {
				cities.Add(city);
			}
			for (int i = 0; i < instance.NumberCities; i++) {
				int cityIndex = Statistics.RandomDiscreteUniform(0, cities.Count - 1);
				int city = cities[cityIndex];
				cities.RemoveAt(cityIndex);				
				solution[i] = city;
			}
			
			return solution;
		}
		
		public static int[] GreedySolution(TSPInstance instance)
		{
			int[] solution = new int[instance.NumberCities];
			bool[] visited = new bool[instance.NumberCities];
			
			for (int i = 0; i < instance.NumberCities; i++) {
				if (i == 0) {
					solution[i] = 0;
				}
				else {
					int currentCity = solution[i-1];
					int nextCity;
					double bestCost = double.MaxValue;
					for (nextCity = 1; nextCity < instance.NumberCities; nextCity++) {
						if (!visited[nextCity] && instance.Costs[currentCity,nextCity] < bestCost) {
							solution[i] = nextCity;
							bestCost = instance.Costs[currentCity,nextCity];
						}
					}
				}
				visited[solution[i]] = true;
			}

			return solution;
		}		
		
		public static int[] GetNeighbor(TSPInstance instance, int[] solution)
		{
			int[] neighbor = new int[instance.NumberCities];
			int a = Statistics.RandomDiscreteUniform(0, solution.Length - 1);
			int b = a;
			while (b == a) {
				b = Statistics.RandomDiscreteUniform(0, solution.Length - 1);
			}
			for (int i = 0; i < solution.Length; i++) {
				if (i == a) {
					neighbor[i] = solution[b];
				}
				else if (i == b) {
					neighbor[i] = solution[a];
				}
				else {
					neighbor[i] = solution[i];
				}
			}
			
			return neighbor;
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
	
		// Implementation of the Tabu Movement of two movements.
		public static Tuple<int, int> GetTabu(int[] source, int[] destiny)
		{
			Tuple<int, int> tabu = new Tuple<int, int>(-1, -1);
			
			for (int i = 0; i < source.Length; i++) {
				if (source[i] != destiny[i]) {
					tabu.Val1 = Math.Min(source[i],destiny[i]);
					tabu.Val2 = Math.Max(source[i],destiny[i]);
					break;
				}
			}
			
			return tabu;
		}
		
		// Implementation of the GRC solution's construction algorithm.
		public static int[] GRCSolution(TSPInstance instance, double rclThreshold)
		{
			int numCities = instance.NumberCities;
			int[] path = new int[instance.NumberCities];
			int totalCities = numCities;
			int index = 0;
			double best = 0;
			double cost = 0;
			int city = 0;
			// Restricted Candidate List.
			SortedList<double, int> rcl = new SortedList<double, int>();
			// Available cities.
			bool[] visited = new bool[numCities];
			
			path[0] = Statistics.RandomDiscreteUniform(0, numCities-1);
			visited[path[0]] = true;
			numCities --;
			
			while (numCities > 0) {
				rcl = new SortedList<double, int>();
				for (int i = 0; i < totalCities; i++) {
					if (!visited[i]) {
						cost = instance.Costs[path[index], i];
						if(rcl.Count == 0) {	
							best = cost;
							rcl.Add(cost, i);
						}
						else if( cost < best) {
							// The new city is the new best;
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
							// The new city is a mostly good candidate.
							rcl.Add(cost, i);
						}							
					}
				}
				city = rcl.Values[Statistics.RandomDiscreteUniform(0, rcl.Count-1)];
				index++;
				visited[city] = true;
				path[index] = city;
				numCities--;
			}
			
			return path;
		}
		
		public static double Distance(TSPInstance instance, int[] a, int[] b)
		{
			double distance = 0;
			
			for (int i = 0; i < a.Length - 1; i++) {
				if (a[i] != b[i] || a[i+1] != b[i+1]) {
					distance += 1;
				}
			}
			
			return distance;
		}
	}
}
