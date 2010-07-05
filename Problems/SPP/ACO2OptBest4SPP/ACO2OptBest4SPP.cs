using System;

namespace Metaheuristics
{
	public class ACO2OptBest4SPP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 500;
		protected double rho = 0.1;
		protected double alpha = 5;
		protected double beta = 0;
		protected int maxReinit = 10;
		protected int numberAnts = 2;				
		
		public void Start(string inputFile, string outputFile, int timeLimit)
		{
			SPPInstance instance = new SPPInstance(inputFile);
			MaxMinAntSystem aco = new MaxMinAntSystem2OptBest4SPP(instance, numberAnts, rho, alpha, beta, maxReinit);
			// Solving the problem and writing the best solution found.
			aco.Run(timeLimit - timePenalty);
			SPPSolution solution = new SPPSolution(instance, aco.BestSolution);
			solution.Write(outputFile);
		}

		public string Name {
			get {
				return "ACO with 2-opt (best improvement) local search for SPP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.ACO;
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
			timePenalty = (int) parameters[0];
			rho = parameters[1];
			alpha = parameters[2];
			beta = parameters[3];
			maxReinit = (int) parameters[4];
			numberAnts = (int) parameters[5];
		}			
	}
}
