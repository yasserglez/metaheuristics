using System;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class GA4SPP : IMetaheuristic, ITunableMetaheuristic
	{
		protected double popSize = 10;
		protected double mutProbability = 0.2;
		protected double timePenalty = 500;

		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			SPPInstance instance = new SPPInstance(fileInput);
			
			// Setting the parameters of the GA for this instance of the problem.
			int[] lowerBounds = new int[instance.NumberItems];
			int[] upperBounds = new int[instance.NumberItems];
			for (int i = 0; i < instance.NumberItems; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberSubsets - 1;
			}
			DiscreteGA genetic = new DiscreteGA4SPP(instance, (int)popSize, mutProbability, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			genetic.Run(timeLimit);
			SPPSolution solution = new SPPSolution(instance, genetic.BestIndividual);
			solution.Write(fileOutput);
		}
		
		public string Name {
			get {
				return "GA for SPP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.GA;
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

		public void UpdateParameters(double[] parameters)
		{
			timePenalty = parameters[0];
			popSize = parameters[1];
			mutProbability = parameters[2];
		}			
	}
}
