using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class MaxMinAntSystemBL2OptFirst42SP : MaxMinAntSystem
	{
		public TwoSPInstance Instance { get; protected set; }
		
		public MaxMinAntSystemBL2OptFirst42SP(TwoSPInstance instance, int numberAnts, double rho, double alpha, double beta, int maxReinit)
			: base(instance.NumberItems, TwoSPUtils.Fitness(instance, TwoSPUtils.BLCoordinates(instance, TwoSPUtils.RandomSolution(instance))), 
			       numberAnts, rho, alpha, beta, maxReinit)
		{
			Instance = instance;
		}
		
		protected override double Fitness (int[] solution)
		{
			return TwoSPUtils.Fitness(Instance, TwoSPUtils.BLCoordinates(Instance, solution));
		}
		
		protected override void InitializeHeuristic (double[,] heuristic)
		{
			for (int i = 0; i < heuristic.GetLength(0); i++) {
				for (int j = 0; j < heuristic.GetLength(1); j++) {
					heuristic[i,j] = 0;
					heuristic[j,i] = heuristic[i,j];
				}
			}
		}
		
		public override void LocalSearch (int[] solution)
		{
			TwoSPUtils.BLLocalSearch2OptFirst(Instance, solution);
		}
		
		protected override List<int> FactibleNeighbors (int i, bool[] visited)
		{
			List<int> neighbors = new List<int>();
			
			// Checking all the neighbors.
			for (int j = 0; j < Instance.NumberItems; j++) {
				if (i != j && !visited[j]) {
					neighbors.Add(j);
				}
			}
			
			return neighbors;
		}
	}
}
