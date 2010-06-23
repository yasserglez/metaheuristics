using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class TwoOptBest4QAP : IMetaheuristic
	{
		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			QAPInstance instance = new QAPInstance(fileInput);
			int[] assignment = QAPUtils.RandomSolution(instance);
			QAPUtils.LocalSearch2OptBest(instance, assignment);
			QAPSolution solution = new QAPSolution(instance, assignment);
			solution.Write(fileOutput);
		}

		public string Name {
			get {
				return "2-opt (best improvement) for QAP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.SH;
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
