using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class TwoOptFirst4SPP : IMetaheuristic
	{
		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			SPPInstance instance = new SPPInstance(fileInput);
			int[] assignment = SPPUtils.GRCSolution(instance, 1.0);
			SPPUtils.LocalSearch2OptFirst(instance, assignment);
			SPPSolution solution = new SPPSolution(instance, assignment);
			solution.Write(fileOutput);
		}

		public string Name {
			get {
				return "2-opt (first improvement) for SPP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.SH;
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
