using System;

using Metaheuristics;

namespace Tune
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			ITunableMetaheuristic[] algorithms = new ITunableMetaheuristic[] {
				new UMDA4TSP(),
				new UMDABL2OptBest42SP(),
				new UMDABL2OptFirst42SP(),
				new GABL42SP(),
				new GABL2OptBest42SP(),
				new GABL2OptFirst42SP(),
			};
			
			algorithms.ParallelForEach(algorithm => {
				string dir = "";
				switch (algorithm.Problem) {
				case ProblemType.QAP:
					dir = "../../../Problems/QAP/Instances";
					break;
				case ProblemType.SPP:
					dir = "../../../Problems/SPP/Instances";
					break;
				case ProblemType.TSP:
					dir = "../../../Problems/TSP/Instances";
					break;
				case ProblemType.TwoSP:
					dir = "../../../Problems/2SP/Instances";
					break;	
				default:
					break;
				}
				
				Tuner tuner = null;
				switch(algorithm.Type) {
				case MetaheuristicType.EDA:
					tuner = new UMDATuner(algorithm, dir);
					break;
				case MetaheuristicType.GA:
					tuner = new GATuner(algorithm, dir);
					break;
				case MetaheuristicType.SA:
					tuner = new SATuner(algorithm, dir);
					break;
				default:
					break;
				}
				
				tuner.Start(algorithm.GetType().Name + ".txt");
			});
		}
	}
}
