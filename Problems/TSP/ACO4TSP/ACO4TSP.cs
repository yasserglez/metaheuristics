using System;

namespace Metaheuristics
{
	public class ACO4TSP : IMetaheuristic
	{
		protected int timePenalty = 250;
		protected double beta = 3;
		protected int maxReinit = 5;
		protected int candidateLength = 40; // Between 15 and 40.
		protected double candidateWeight = 0.5;
		
		public void Start(string inputFile, string outputFile, int timeLimit)
		{
			TSPInstance instance = new TSPInstance(inputFile);
			MaxMinAntSystem aco = new MaxMinAntSystem4TSP(instance, instance.NumberCities, beta, maxReinit, candidateLength, candidateWeight);
			// Solving the problem and writing the best solution found.
			aco.Run(timeLimit - timePenalty);
			TSPSolution solution = new TSPSolution(instance, aco.BestSolution);
			solution.Write(outputFile);
		}

		public string Name {
			get {
				return "ACO for TSP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.ACO;
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
	}
}
