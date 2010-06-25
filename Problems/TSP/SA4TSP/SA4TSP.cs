using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class SA4TSP : IMetaheuristic, ITunableMetaheuristic
	{
		public int initialSolutions = 5;
		public double levelLengthFactor = 1;
		public double tempReduction = 0.95;
		
		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			TSPInstance instance = new TSPInstance(fileInput);
			int levelLength = (int) Math.Ceiling(levelLengthFactor * (instance.NumberCities * (instance.NumberCities - 1)));
			DiscreteSA sa = new DiscreteSA4TSP(instance, initialSolutions, levelLength, tempReduction);
			sa.Run(timeLimit);
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
			initialSolutions = (int) parameters[0];
			levelLengthFactor = parameters[1];
			tempReduction = parameters[2];
		}		
	}
}
