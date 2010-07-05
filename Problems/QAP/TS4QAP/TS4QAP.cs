using System;

namespace Metaheuristics
{
	public class TS4QAP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 100;
		public double neighborChecksFactor = 0.25;
		public double tabuListFactor = 0.50;
		public double rclTreshold = 0.3;
		
		public void Start(string inputFile, string outputFile, int timeLimit)
		{
			QAPInstance instance = new QAPInstance(inputFile);
			int neighborChecks = (int) Math.Ceiling(neighborChecksFactor * (instance.NumberFacilities * (instance.NumberFacilities - 1)));
			int tabuListLength = (int) Math.Ceiling(tabuListFactor * instance.NumberFacilities);
			DiscreteTS ts = new DiscreteTS4QAP(instance, rclTreshold, tabuListLength, neighborChecks);
			ts.Run(timeLimit - timePenalty);
			QAPSolution solution = new QAPSolution(instance, ts.BestSolution);
			solution.Write(outputFile);
		}
		
		public string Name {
			get {
				return "TS for QAP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.TS;
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
			neighborChecksFactor = parameters[1];
			tabuListFactor = parameters[2];
			rclTreshold = parameters[3];
		}
	}
}
