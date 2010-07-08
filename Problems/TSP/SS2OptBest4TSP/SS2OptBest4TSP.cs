using System;

namespace Metaheuristics
{
	public class SS2OptBest4TSP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 250;
		public int poolSize = 10;
		public int refSetSize = 4;
		public double explorationFactor = 0.25;
		
		public void Start(string inputFile, string outputFile, int timeLimit)
		{
			TSPInstance instance = new TSPInstance(inputFile);
			DiscreteSS ss = new DiscreteSS2OptBest4TSP(instance, poolSize, refSetSize, explorationFactor);
			ss.Run(timeLimit - timePenalty);
			TSPSolution solution = new TSPSolution(instance, ss.BestSolution);
			solution.Write(outputFile);
		}

		public string Name {
			get {
				return "SS with 2-opt (best improvement) local search for TSP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.SS;
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
		
		public void UpdateParameters(double[] parameters)
		{
			timePenalty = (int) parameters[0];
			poolSize = (int) parameters[1];
			refSetSize = (int) parameters[2];
			explorationFactor = parameters[3];
		}		
	}
}
