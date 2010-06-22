using System;

namespace Metaheuristics
{
	public class ACOBL42SP : IMetaheuristic
	{
		public void Start(string inputFile, string outputFile, int timeLimit)
		{
			throw new NotImplementedException();
		}

		public string Name {
			get {
				return "ACO using the BL heuristic for 2SP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.ACO;
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
