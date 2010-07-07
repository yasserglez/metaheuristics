using System;

namespace Metaheuristics
{
	public class TS4TSP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 250;
		public double neighborChecksFactor = 0.1;
		public double tabuListFactor = 0.50;
		public double rclTreshold = 1.0;
		
		public void Start(string inputFile, string outputFile, int timeLimit)
		{
			TSPInstance instance = new TSPInstance(inputFile);
			int neighborChecks = (int) Math.Ceiling(neighborChecksFactor * (instance.NumberCities * (instance.NumberCities - 1)));
			int tabuListLength = (int) Math.Ceiling(tabuListFactor * instance.NumberCities);
			DiscreteTS ts = new DiscreteTS4TSP(instance, rclTreshold, tabuListLength, neighborChecks);
			ts.Run(timeLimit - timePenalty);
			TSPSolution solution = new TSPSolution(instance, ts.BestSolution);
			solution.Write(outputFile);
		}
		
		public string Name {
			get {
				return "TS for TSP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.TS;
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
			neighborChecksFactor = parameters[1];
			tabuListFactor = parameters[2];
			rclTreshold = parameters[3];
		}
	}
}
