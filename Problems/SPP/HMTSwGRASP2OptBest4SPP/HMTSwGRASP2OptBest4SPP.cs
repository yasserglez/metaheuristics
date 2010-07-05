
using System;

namespace Metaheuristics
{
	public class HMTSwGRASP2OptBest4SPP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 250;
		protected int graspIterations = 3;
		//TS Params
		public double neighborChecksFactor = 0.1;
		public double tabuListFactor = 0.50;
		//GRASP Params
		public double rclTreshold = 0.40;
		
		public void Start(string inputFile, string outputFile, int timeLimit)
		{
			SPPInstance instance = new SPPInstance(inputFile);
			int neighborChecks = (int) Math.Ceiling(neighborChecksFactor * (instance.NumberSubsets - 1));
			int tabuListLength = (int) Math.Ceiling(tabuListFactor * instance.NumberItems);
			DiscreteHMTSwGRASP2OptBest4SPP hm = new DiscreteHMTSwGRASP2OptBest4SPP(instance, rclTreshold, graspIterations, tabuListLength, neighborChecks);
			hm.Run(timeLimit - timePenalty);
			SPPSolution solution = new SPPSolution(instance, hm.BestSolution);
			solution.Write(outputFile);
		}
		
		public string Name {
			get {
				return "TS with GRASP with 2-opt (best improvement) local search for SPP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.HM;
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
			
		public void UpdateParameters (double[] parameters)	{
			timePenalty = (int) parameters[0];
			graspIterations = (int) parameters[1];
		}
	}
}
	