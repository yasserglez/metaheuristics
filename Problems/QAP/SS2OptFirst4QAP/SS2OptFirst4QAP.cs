using System;

namespace Metaheuristics
{
	public class SS2OptFirst4QAP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 250;
		public int poolSize = 100;
		public int refSetSize = 10;
		public double explorationFactor = 0.5;
		
		public void Start(string inputFile, string outputFile, int timeLimit)
		{
			QAPInstance instance = new QAPInstance(inputFile);
			DiscreteSS ss = new DiscreteSS2OptFirst4QAP(instance, poolSize, refSetSize, explorationFactor);
			ss.Run(timeLimit - timePenalty);
			QAPSolution solution = new QAPSolution(instance, ss.BestSolution);
			solution.Write(outputFile);
		}

		public string Name {
			get {
				return "SS with 2-opt (first improvement) local search for QAP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.SS;
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
		
		public void UpdateParameters(double[] parameters)
		{
			timePenalty = (int) parameters[0];
			poolSize = (int) parameters[1];
			refSetSize = (int) parameters[2];
			explorationFactor = parameters[3];
		}
	}
}
