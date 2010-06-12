using System;

namespace Metaheuristics
{
	public class ACO4SPP : IMetaheuristic
	{
		public void Start(string inputFile, string outputFile, int timeLimit)
		{
			throw new NotImplementedException();
		}

		public string Name {
			get {
				return "ACO for SPP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.ACO;
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
	}
}
