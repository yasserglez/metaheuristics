using System;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class GA2OptBest4SPP : IMetaheuristic, ITunableMetaheuristic
	{
		protected double popFactor = 2;
		protected double mutProbability = 0.2;
		protected double timePenalty = 500;
		
		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			SPPInstance instance = new SPPInstance(fileInput);
			
			// Setting the parameters of the GA for this instance of the problem.
			int popSize = (int) Math.Ceiling(popFactor * instance.NumberItems);
			int[] lowerBounds = new int[instance.NumberItems];
			int[] upperBounds = new int[instance.NumberItems];
			for (int i = 0; i < instance.NumberItems; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberSubsets - 1;
			}
			DiscreteGA genetic = new DiscreteGA2OptBest4SPP(instance, popSize, mutProbability, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			genetic.Run(timeLimit);
			SPPSolution solution = new SPPSolution(instance, genetic.BestIndividual);
			solution.Write(fileOutput);
		}

		public string Name {
			get {
				return "GA with 2-opt (best improvement) local search for SPP";
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
			popFactor = parameters[1];
			mutProbability = parameters[2];
		}			
	}
}
