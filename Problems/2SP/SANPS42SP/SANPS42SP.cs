using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class SANPS42SP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 250;
		public int initialSolutions = 5;
		public double levelLengthFactor = 1;
		public double tempReduction = 0.95;

		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			TwoSPInstance instance = new TwoSPInstance(fileInput);
			int levelLength = (int) Math.Ceiling(levelLengthFactor * (2 * instance.NumberItems));
			DiscreteSA sa = new DiscreteSANPS42SP(instance, initialSolutions, levelLength, tempReduction);
			sa.Run(timeLimit - timePenalty);
			int[,] coordinates = TwoSPUtils.NPSCoordinates(instance, sa.BestSolution);
			TwoSPSolution solution = new TwoSPSolution(instance, coordinates);
			solution.Write(fileOutput);
		}
		
		public string Name {
			get {
				return "SA using the NPS heuristic for 2SP";
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
			timePenalty = (int) parameters[0];			
			initialSolutions = (int) parameters[1];
			levelLengthFactor = parameters[2];
			tempReduction = parameters[3];
		}				
	}
}
