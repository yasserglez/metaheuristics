
using System;

namespace Metaheuristics
{
	public class HMTSwGRASP2OptFirst4TSP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 50;
		protected int graspIterations = 8;
		//TS Params
		public double neighborChecksFactor = 0.1;
		public double tabuListFactor = 0.50;
		//GRASP Params
		public double rclTreshold = 0.40;
		
		public void Start(string inputFile, string outputFile, int timeLimit)
		{
			TSPInstance instance = new TSPInstance(inputFile);
			int neighborChecks = (int) Math.Ceiling(neighborChecksFactor * (instance.NumberCities * (instance.NumberCities - 1)));
			int tabuListLength = (int) Math.Ceiling(tabuListFactor * instance.NumberCities);
			DiscreteHMTSwGRASP2OptFirst4TSP hm = new DiscreteHMTSwGRASP2OptFirst4TSP(instance, rclTreshold, graspIterations, tabuListLength, neighborChecks);
			hm.Run(timeLimit - timePenalty);
			TSPSolution solution = new TSPSolution(instance, hm.BestSolution);
			solution.Write(outputFile);
		}
		
		public string Name {
			get {
				return "TS with GRASP with 2-opt (first improvement) local search for TSP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.HM;
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
			
		public void UpdateParameters (double[] parameters)	{
			timePenalty = (int) parameters[0];
			graspIterations = (int) parameters[1];
		}
	}
}
