using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class UMDA2OptBest4SPP : IMetaheuristic
	{
		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			SPPInstance instance = new SPPInstance(fileInput);
			
			// Setting the parameters of the UMDA for this instance of the problem.
			int popSize = 50 * instance.NumberItems;
			double truncFactor = 0.3;
			int[] lowerBounds = new int[instance.NumberItems];
			int[] upperBounds = new int[instance.NumberItems];
			for (int i = 0; i < instance.NumberItems; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberSubsets - 1;
			}
			DiscreteUMDA umda = new DiscreteUMDA2OptBest4SPP(instance, popSize, truncFactor, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			umda.Run(timeLimit);
			SPPSolution solution = new SPPSolution(instance, umda.BestIndividual);
			solution.Write(fileOutput);
		}

		public string Name {
			get {
				return "UMDA with 2-opt (best improvement) local search for SPP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.EDA;
			}
		}
		
		public ProblemType Problem {
			get {
				return ProblemType.SPP;
			}
		}
		
		public string[] Team {
			get {
				return About.Team;
			}
		}
	}
}
