using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class MaxMinAntSystem2OptFirst4TSP : MaxMinAntSystem
	{
		public TSPInstance Instance { get; protected set; }
		
		protected List<Tuple<double,int>>[] candidateLists;
		protected double candidateWeight;
		
		public MaxMinAntSystem2OptFirst4TSP(TSPInstance instance, int numberAnts, double rho, 
		                                    double alpha, double beta, int maxReinit, 
		                                    int candidateLength, double candidateWeight)
			: base(instance.NumberCities, TSPUtils.Fitness(instance, TSPUtils.RandomSolution(instance)),
			       numberAnts, rho, alpha, beta, maxReinit)
		{
			Instance = instance;
			this.candidateWeight = candidateWeight;
			// Build the candidate list.
			this.candidateLists = new List<Tuple<double,int>>[Instance.NumberCities];
			for (int i = 0; i < Instance.NumberCities; i++) {
				this.candidateLists[i] = new List<Tuple<double,int>>();
				for (int j = 0; j < Instance.NumberCities; j++) {
					if (i != j) {
						this.candidateLists[i].Add(new Tuple<double,int>(Instance.Costs[i,j], j));
					}
				}
				this.candidateLists[i].Sort();
				this.candidateLists[i].RemoveRange(candidateLength, this.candidateLists[i].Count - candidateLength);
			}			
		}
		
		protected override double Fitness (int[] solution)
		{
			return TSPUtils.Fitness(Instance, solution);
		}
		
		protected override void InitializeHeuristic (double[,] heuristic)
		{
			for (int i = 0; i < heuristic.GetLength(0); i++) {
				for (int j = 0; j < heuristic.GetLength(1); j++) {
					heuristic[i,j] = 1.0 / Instance.Costs[i,j];
					heuristic[j,i] = heuristic[i,j];
				}
			}
		}
		
		protected override List<int> FactibleNeighbors (int i, bool[] visited)
		{
			List<int> neighbors = new List<int>();
			
			if (Statistics.RandomUniform() <= this.candidateWeight) {
				// Only checking the candidate list.
				foreach (Tuple<double,int> candidate in this.candidateLists[i]) {
					int j = candidate.Val2;
					if (i != j && !visited[j]) {
						neighbors.Add(j);
					}
				}
			}
			
			if (neighbors.Count == 0) {
				// Checking all the neighbors.
				for (int j = 0; j < Instance.NumberCities; j++) {
					if (i != j && !visited[j]) {
						neighbors.Add(j);
					}
				}
			}
			
			return neighbors;
		}
	}
}
