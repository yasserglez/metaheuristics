using System;

namespace Metaheuristics
{
	public class TS4SPP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 100;
		public double neighborChecksFactor = 0.25;
		public double tabuListFactor = 0.50;
		public double rclTreshold = 1.0;
		
		public void Start(string inputFile, string outputFile, int timeLimit)
		{
			SPPInstance instance = new SPPInstance(inputFile);
			int neighborChecks = (int) Math.Ceiling(neighborChecksFactor * (instance.NumberSubsets - 1));
			int tabuListLength = (int) Math.Ceiling(tabuListFactor * instance.NumberItems);
			DiscreteTS ts = new DiscreteTS4SPP(instance, rclTreshold, tabuListLength, neighborChecks);
			ts.Run(timeLimit - timePenalty);
			SPPSolution solution = new SPPSolution(instance, ts.BestSolution);
			solution.Write(outputFile);
		}
		
		public string Name {
			get {
				return "TS for SPP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.TS;
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
			neighborChecksFactor = parameters[1];
			tabuListFactor = parameters[2];
			rclTreshold = parameters[3];
		}
	}
}
