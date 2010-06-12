using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class SA4QAP : IMetaheuristic
	{
		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			QAPInstance instance = new QAPInstance(fileInput);
			DiscreteSA sa = new DiscreteSA4QAP(instance);
			sa.Run(timeLimit);
			QAPSolution solution = new QAPSolution(instance, sa.BestSolution);
			solution.Write(fileOutput);
		}

		public string Name {
			get {
				return "SA for QAP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.SA;
			}
		}
		
		public ProblemType Problem {
			get {
				return ProblemType.QAP;
			}
		}
		
		public string[] Team {
			get {
				return About.Team;
			}
		}
	}
}
