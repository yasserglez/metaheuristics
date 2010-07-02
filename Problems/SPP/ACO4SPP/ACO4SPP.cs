using System;

namespace Metaheuristics
{
	public class ACO4SPP : IMetaheuristic
	{
		protected int timePenalty = 250;
		protected double rho = 0.02;
		protected double alpha = 1;
		protected double beta = 3;
		protected int maxReinit = 5;
		
		public void Start(string inputFile, string outputFile, int timeLimit)
		{
			SPPInstance instance = new SPPInstance(inputFile);
			MaxMinAntSystem aco = new MaxMinAntSystem4SPP(instance, instance.NumberItems, rho, alpha, beta, maxReinit);
			// Solving the problem and writing the best solution found.
			aco.Run(timeLimit - timePenalty);
			SPPSolution solution = new SPPSolution(instance, aco.BestSolution);
			solution.Write(outputFile);
		}

		public string Name {
			get {
				return "ACO for SPP";
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
	}
}
