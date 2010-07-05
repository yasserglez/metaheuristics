
using System;

namespace Metaheuristics
{
	public class HMTSwGRASP2OptBest4QAP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 50;
		protected int graspIterations = 2;
		//TS Params
		public double neighborChecksFactor = 0.1;
		public double tabuListFactor = 0.50;
		//GRASP Params
		public double rclTreshold = 0.40;
		
		public void Start(string inputFile, string outputFile, int timeLimit)
		{
			QAPInstance instance = new QAPInstance(inputFile);
			int neighborChecks = (int) Math.Ceiling(neighborChecksFactor * (instance.NumberFacilities * (instance.NumberFacilities - 1)));
			int tabuListLength = (int) Math.Ceiling(tabuListFactor * instance.NumberFacilities);
			DiscreteHMTSwGRASP2OptBest4QAP hm = new DiscreteHMTSwGRASP2OptBest4QAP(instance, rclTreshold, graspIterations, tabuListLength, neighborChecks);
			hm.Run(timeLimit - timePenalty);
			QAPSolution solution = new QAPSolution(instance, hm.BestSolution);
			solution.Write(outputFile);
		}
		
		public string Name {
			get {
				return "TS with GRASP with 2-opt (best improvement) local search for QAP";
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
