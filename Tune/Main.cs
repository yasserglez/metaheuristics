using System;

using Metaheuristics;

namespace Tune
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			ITunableMetaheuristic[] algorithms = new ITunableMetaheuristic[] {
				new GA4TSP(),
				new GA2OptFirst4TSP(),
//				new UMDA4QAP(),
//				new UMDA2OptFirst4QAP(),
			};
			
			algorithms.ParallelForEach(algorithm => {
				string instancesDir = "";
				switch (algorithm.Problem) {
				case ProblemType.QAP:
					instancesDir = "../../../Problems/QAP/Instances";
					break;
				case ProblemType.SPP:
					instancesDir = "../../../Problems/SPP/Instances";
					break;
				case ProblemType.TSP:
					instancesDir = "../../../Problems/TSP/Instances";
					break;
				case ProblemType.TwoSP:
					instancesDir = "../../../Problems/2SP/Instances";
					break;	
				default:
					break;
				}
				
				Tuner tuner = null;
				switch(algorithm.Type) {
				case MetaheuristicType.EDA:
					tuner = new UMDATuner(algorithm, instancesDir);
					break;
				case MetaheuristicType.GA:
					tuner = new GATuner(algorithm, instancesDir);
					break;
				case MetaheuristicType.SA:
					tuner = new SATuner(algorithm, instancesDir);
					break;
				case MetaheuristicType.GRASP:
					tuner = new GRASPTuner(algorithm, instancesDir);
					break;
				default:
					break;
				}
				
				tuner.Start(algorithm.GetType().Name + ".txt");
			});
		}
	}
}
