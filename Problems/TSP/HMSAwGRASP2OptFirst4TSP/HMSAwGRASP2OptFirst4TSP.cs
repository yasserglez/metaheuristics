
using System;

namespace Metaheuristics
{
	public class HMSAwGRASP2OptFirst4TSP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 250;
		protected int graspIterations = 3;
		//SA Params
		public int initialSolutions = 2;
		public double levelLengthFactor = 0.75;
		public double tempReduction = 0.85;
		//GRASP Params
		public double rclTreshold = 0.40;
		
		public void Start(string inputFile, string outputFile, int timeLimit)
		{
			TSPInstance instance = new TSPInstance(inputFile);
			int levelLength = (int) Math.Ceiling(levelLengthFactor * (instance.NumberCities * (instance.NumberCities - 1)));
			DiscreteHMSAwGRASP2OptFirst4TSP hm = new DiscreteHMSAwGRASP2OptFirst4TSP(instance, rclTreshold, graspIterations, initialSolutions, levelLength, tempReduction);
			hm.Run(timeLimit - timePenalty);
			TSPSolution solution = new TSPSolution(instance, hm.BestSolution);
			solution.Write(outputFile);
		}
		
		public string Name {
			get {
				return "SA with GRASP with 2-opt (first improvement) local search for TSP";
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
