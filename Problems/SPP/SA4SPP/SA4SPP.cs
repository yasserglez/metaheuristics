using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class SA4SPP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 250;
		public int initialSolutions = 10;
		public double levelLengthFactor = 0.75;
		public double tempReduction = 0.95;
		public double rclTreshold = 0.2;

		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			SPPInstance instance = new SPPInstance(fileInput);
			int levelLength = (int) Math.Ceiling(levelLengthFactor * (instance.NumberSubsets - 1));
			DiscreteSA sa = new DiscreteSA4SPP(instance, rclTreshold, initialSolutions, levelLength, tempReduction);
			sa.Run(timeLimit - timePenalty);
			SPPSolution solution = new SPPSolution(instance, sa.BestSolution);
			solution.Write(fileOutput);
		}

		public string Name {
			get {
				return "SA for SPP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.SA;
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
			initialSolutions = (int) parameters[1];
			levelLengthFactor = parameters[2];
			tempReduction = parameters[3];
			rclTreshold = parameters[4];
		}				
	}
}
