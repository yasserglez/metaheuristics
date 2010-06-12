using System;

namespace Metaheuristics
{
	public class ACO42SP : IMetaheuristic
	{
		public void Start(string inputFile, string outputFile, int timeLimit)
		{
			throw new NotImplementedException();
		}

		public string Name {
			get {
				return "ACO for 2SP";
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
