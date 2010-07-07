using System;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class MA4TSP : IMetaheuristic, ITunableMetaheuristic
	{
		protected double timePenalty = 250;
		protected double popSize = 2;
		protected double mutProbability = 0.3;
	
		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			TSPInstance instance = new TSPInstance(fileInput);
			
			// Setting the parameters of the MA for this instance of the problem.
			int[] lowerBounds = new int[instance.NumberCities];
			int[] upperBounds = new int[instance.NumberCities];
			for (int i = 0; i < instance.NumberCities; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberCities - 1;
			}
			DiscreteMA memetic = new DiscreteMA4TSP(instance, (int)popSize, mutProbability, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			memetic.Run(timeLimit - (int)timePenalty);
			TSPSolution solution = new TSPSolution(instance, memetic.BestIndividual);
			solution.Write(fileOutput);
		}

		public string Name {
			get {
				return "MA for TSP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.MA;
			}
		}
		
		public ProblemType Problem {
			get {
				return ProblemType.TSP;
			}
		}
		
		public string[] Team {
			get {
				return About.Team;
			}
		}
		
		public void UpdateParameters(double[] parameters)
		{
			timePenalty = parameters[0];
			popSize = parameters[1];
			mutProbability = parameters[2];
		}		
	}
}
