using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class BL2OptFirst42SP : IMetaheuristic
	{
		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			TwoSPInstance instance = new TwoSPInstance(fileInput);
			int[] ordering = TwoSPUtils.DecreasingWidth(instance);
			TwoSPUtils.BLLocalSearch2OptFirst(instance, ordering);
			int[,] coordinates = TwoSPUtils.BLCoordinates(instance, ordering);
			TwoSPSolution solution = new TwoSPSolution(instance, coordinates);
			solution.Write(fileOutput);
		}

		public string Name {
			get {
				return "2-opt (first improvement) with the BL heuristic for 2SP";
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
