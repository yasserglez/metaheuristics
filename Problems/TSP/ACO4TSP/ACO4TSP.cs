using System;

namespace Metaheuristics
{
	public class ACO4TSP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 250;
		protected double rho = 0.02;
		protected double alpha = 1;
		protected double beta = 10;
		protected int maxReinit = 5;
		protected int candidateLength = 15;
		protected double candidateWeight = 0.99;
		
		public void Start(string inputFile, string outputFile, int timeLimit)
		{
			TSPInstance instance = new TSPInstance(inputFile);
			MaxMinAntSystem aco = new MaxMinAntSystem4TSP(instance, instance.NumberCities, rho, alpha, beta, maxReinit, candidateLength, candidateWeight);
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
		
		public void UpdateParameters(double[] parameters)
		{
			timePenalty = (int) parameters[0];
			rho = parameters[1];
			alpha = parameters[2];
			beta = parameters[3];
			maxReinit = (int) parameters[4];
			candidateLength = (int) parameters[5];
			candidateWeight = parameters[6];
		}
	}
}
