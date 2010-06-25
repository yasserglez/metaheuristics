using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class SABL42SP : IMetaheuristic, ITunableMetaheuristic
	{
		public int initialSolutions = 5;
		public double levelLengthFactor = 1;
		public double tempReduction = 0.95;

		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			TwoSPInstance instance = new TwoSPInstance(fileInput);
			int levelLength = (int) Math.Ceiling(levelLengthFactor * (2 * instance.NumberItems));
			DiscreteSA sa = new DiscreteSABL42SP(instance, initialSolutions, levelLength, tempReduction);
			sa.Run(timeLimit);
			int[,] coordinates = TwoSPUtils.BLCoordinates(instance, sa.BestSolution);
			TwoSPSolution solution = new TwoSPSolution(instance, coordinates);
			solution.Write(fileOutput);
		}
		
		public string Name {
			get {
				return "SA using the BL heuristic for 2SP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.SA;
			}
		}
		
		public ProblemType Problem {
			get {
				return ProblemType.TwoSP;
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
