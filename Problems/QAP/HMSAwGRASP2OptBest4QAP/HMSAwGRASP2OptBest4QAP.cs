
using System;

namespace Metaheuristics
{
	public class HMSAwGRASP2OptBest4QAP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 250;
		protected int graspIterations = 3;
		//SA Params
		public int initialSolutions = 3;
		public double levelLengthFactor = 0.25;
		public double tempReduction = 0.85;
		//GRASP Params
		public double rclTreshold = 0.80;
		
		public void Start(string inputFile, string outputFile, int timeLimit)
		{
			QAPInstance instance = new QAPInstance(inputFile);
				int levelLength = (int) Math.Ceiling(levelLengthFactor * (instance.NumberFacilities * (instance.NumberFacilities - 1)));
			DiscreteHMSAwGRASP2OptBest4QAP hm = new DiscreteHMSAwGRASP2OptBest4QAP(instance, rclTreshold, graspIterations, initialSolutions, levelLength, tempReduction);
			hm.Run(timeLimit - timePenalty);
			QAPSolution solution = new QAPSolution(instance, hm.BestSolution);
			solution.Write(outputFile);
		}
		
		public string Name {
			get {
				return "SA with GRASP with 2-opt (best improvement) local search for QAP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.HM;
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
			
		public void UpdateParameters (double[] parameters)	{
			timePenalty = (int) parameters[0];
			graspIterations = (int) parameters[1];
		}
	}
}
