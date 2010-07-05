
using System;

namespace Metaheuristics
{
	public class HMSAwGRASP2OptBest4SPP : IMetaheuristic, ITunableMetaheuristic
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
			SPPInstance instance = new SPPInstance(inputFile);
			int levelLength = (int) Math.Ceiling(levelLengthFactor * (instance.NumberSubsets - 1));
			DiscreteHMSAwGRASP2OptBest4SPP hm = new DiscreteHMSAwGRASP2OptBest4SPP(instance, rclTreshold, graspIterations, initialSolutions, levelLength, tempReduction);
			hm.Run(timeLimit - timePenalty);
			SPPSolution solution = new SPPSolution(instance, hm.BestSolution);
			solution.Write(outputFile);
		}
		
		public string Name {
			get {
				return "SA with GRASP with 2-opt (best improvement) local search for SPP";
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
