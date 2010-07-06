using System;

namespace Metaheuristics
{
	public class SS4QAP : IMetaheuristic, ITunableMetaheuristic
	{
		protected int timePenalty = 50;
		public int poolSize = 200;
		public int refSetSize = 10;
		public double explorationFactor = 0.75;
		
		public void Start(string inputFile, string outputFile, int timeLimit)
		{
			QAPInstance instance = new QAPInstance(inputFile);
			DiscreteSS ss = new DiscreteSS4QAP(instance, poolSize, refSetSize, explorationFactor);
			ss.Run(timeLimit - timePenalty);
			QAPSolution solution = new QAPSolution(instance, ss.BestSolution);
			solution.Write(outputFile);
		}

		public string Name {
			get {
				return "SS for QAP";
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
