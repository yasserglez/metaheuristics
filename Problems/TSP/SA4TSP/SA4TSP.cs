using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class SA4TSP : IMetaheuristic
	{
		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			TSPInstance instance = new TSPInstance(fileInput);
			DiscreteSA sa = new DiscreteSA4TSP(instance);
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
	}
}
