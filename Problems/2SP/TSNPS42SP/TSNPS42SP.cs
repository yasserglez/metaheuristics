using System;

namespace Metaheuristics
{
	public class TSNPS42SP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 250;
		public double neighborChecksFactor = 0.75;
		public double tabuListFactor = 0.85;
		public double rclTreshold = 0.2;
		
		public void Start(string inputFile, string outputFile, int timeLimit)
		{
			TwoSPInstance instance = new TwoSPInstance(inputFile);
			int neighborChecks = (int) Math.Ceiling(neighborChecksFactor * (2 * instance.NumberItems));
			int tabuListLength = (int) Math.Ceiling(tabuListFactor * instance.NumberItems);
			DiscreteTS ts = new DiscreteTSNPS42SP(instance, tabuListLength, neighborChecks);
			ts.Run(timeLimit - timePenalty);
			int[,] coordinates = TwoSPUtils.NPSCoordinates(instance, ts.BestSolution);
			TwoSPSolution solution = new TwoSPSolution(instance, coordinates);
			solution.Write(outputFile);
		}

		public string Name {
			get {
				return "TS for 2SP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.TS;
			}
		}
		
		public ProblemType Problem {
			get {
				return ProblemType.TwoSP;
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
		}
	}
}
