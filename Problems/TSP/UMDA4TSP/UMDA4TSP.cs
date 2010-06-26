using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class UMDA4TSP : IMetaheuristic, ITunableMetaheuristic
	{
		protected double popFactor = 50;
		protected double truncFactor = 0.3;
		protected double timePenalty = 250;

		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			TSPInstance instance = new TSPInstance(fileInput);
			
			// Setting the parameters of the UMDA for this instance of the problem.
			int popSize = (int) Math.Ceiling(popFactor * instance.NumberCities);
			int[] lowerBounds = new int[instance.NumberCities];
			int[] upperBounds = new int[instance.NumberCities];
			for (int i = 0; i < instance.NumberCities; i++) {
				lowerBounds[i] = 0;
				upperBounds[i] = instance.NumberCities - 1;
			}
			DiscreteUMDA umda = new DiscreteUMDA4TSP(instance, popSize, truncFactor, lowerBounds, upperBounds);
			
			// Solving the problem and writing the best solution found.
			umda.Run(timeLimit - (int)timePenalty);
			TSPSolution solution = new TSPSolution(instance, umda.BestIndividual);
			solution.Write(fileOutput);
		}

		public string Name {
			get {
				return "UMDA for TSP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.EDA;
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
			popFactor = parameters[1];
			truncFactor = parameters[2];
		}
	}
}
