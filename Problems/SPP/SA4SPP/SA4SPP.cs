using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class SA4SPP : IMetaheuristic
	{
		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			SPPInstance instance = new SPPInstance(fileInput);
			DiscreteSA sa = new DiscreteSA4SPP(instance);
			sa.Run(timeLimit);
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
	}
}
