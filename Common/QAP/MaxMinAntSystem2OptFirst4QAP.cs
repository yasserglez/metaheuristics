using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class MaxMinAntSystem2OptFirst4QAP : MaxMinAntSystem
	{
		public QAPInstance Instance { get; protected set; }
		
		public MaxMinAntSystem2OptFirst4QAP(QAPInstance instance, int numberAnts, double rho, 
		                                    double alpha, double beta, int maxReinit)
			: base(instance.NumberFacilities, QAPUtils.Fitness(instance, QAPUtils.RandomSolution(instance)),
			       numberAnts, rho, alpha, beta, maxReinit)
		{
			Instance = instance;
		}
		
		protected override double Fitness (int[] solution)
		{
			return QAPUtils.Fitness(Instance, solution);
		}
		
		protected override void InitializeHeuristic (double[,] heuristic)
		{
			for (int i = 0; i < heuristic.GetLength(0); i++) {
				for (int j = 0; j < heuristic.GetLength(1); j++) {
					heuristic[i,j] = 1.0 / Instance.Distances[i,j];
					heuristic[j,i] = heuristic[i,j];
				}
			}
		}
		
		public override void LocalSearch (int[] solution)
		{
			QAPUtils.LocalSearch2OptFirst(Instance, solution);
		}
		
		protected override List<int> FactibleNeighbors (int i, bool[] visited)
		{
			List<int> neighbors = new List<int>();
			
			// Checking all the neighbors.
			for (int j = 0; j < Instance.NumberFacilities; j++) {
				if (i != j && !visited[j]) {
					neighbors.Add(j);
				}
			}
			
			return neighbors;
		}
	}
}
