using System;

namespace Metaheuristics
{
	public class TS42SP : IMetaheuristic
	{
		public void Start(string inputFile, string outputFile, int timeLimit)
		{
			throw new NotImplementedException();
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
	}
}
