using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class NPS2OptBest42SP : IMetaheuristic
	{
		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			TwoSPInstance instance = new TwoSPInstance(fileInput);
			int[] ordering = TwoSPUtils.RandomSolution(instance);
			TwoSPUtils.NPSLocalSearch2OptBest(instance, ordering);
			int[,] coordinates = TwoSPUtils.NPSCoordinates(instance, ordering);
			TwoSPSolution solution = new TwoSPSolution(instance, coordinates);
			solution.Write(fileOutput);
		}

		public string Name {
			get {
				return "2-opt (best improvement) with the NPS heuristic for 2SP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.SH;
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
	}
}
