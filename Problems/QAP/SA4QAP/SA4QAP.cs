using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class SA4QAP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 250;
		public int initialSolutions = 10;
		public double levelLengthFactor = 0.05;
		public double tempReduction = 0.85;
		public double rclTreshold = 0.2;

		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			QAPInstance instance = new QAPInstance(fileInput);
			int levelLength = (int) Math.Ceiling(levelLengthFactor * (instance.NumberFacilities * (instance.NumberFacilities - 1)));
			DiscreteSA sa = new DiscreteSA4QAP(instance, rclTreshold, initialSolutions, levelLength, tempReduction);
			sa.Run(timeLimit - timePenalty);
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
		
		public void UpdateParameters(double[] parameters)
		{
			timePenalty = (int) parameters[0];			
			initialSolutions = (int) parameters[1];
			levelLengthFactor = parameters[2];
			tempReduction = parameters[3];
			rclTreshold = parameters[4];
		}		
	}
}
