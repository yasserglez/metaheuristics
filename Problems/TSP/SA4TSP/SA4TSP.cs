using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class SA4TSP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 250;
		public int initialSolutions = 2;
		public double levelLengthFactor = 0.75;
		public double tempReduction = 0.85;
		
		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			TSPInstance instance = new TSPInstance(fileInput);
			int levelLength = (int) Math.Ceiling(levelLengthFactor * (instance.NumberCities * (instance.NumberCities - 1)));
			DiscreteSA sa = new DiscreteSA4TSP(instance, initialSolutions, levelLength, tempReduction);
			sa.Run(timeLimit - timePenalty);
			TSPSolution solution = new TSPSolution(instance, sa.BestSolution);
			solution.Write(fileOutput);
		}
		
		public string Name {
			get {
				return "SA for TSP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.SA;
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
			initialSolutions = (int) parameters[1];
			levelLengthFactor = parameters[2];
			tempReduction = parameters[3];
		}		
	}
}
