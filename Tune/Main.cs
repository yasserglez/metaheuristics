using System;

using Metaheuristics;

namespace Tune
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			ITunableMetaheuristic[] algorithms = new ITunableMetaheuristic[] {
				new GA2OptFirst4SPP(),
				new GA4SPP(),
				new GA2OptFirst4TSP(),
				new GA4TSP(),
//				new GA2OptFirst4QAP(),
//				new GA4QAP(),
//				new GABL2OptFirst42SP(),
//				new GABL42SP()
//				new GA2OptBest4QAP(),
//				new GA2OptBest4TSP(),
//				new GA2OptBest4SPP(),
//				new GABL2OptBest42SP(),
//				new GANPS42SP(),
//				new PSO2OptFirst4QAP(),
//				new PSO4QAP(),
//				new PSO2OptFirst4TSP(),
//				new PSO4TSP(),
//				new PSOBL2OptFirst42SP(),
//				new PSOBL42SP(),
//				new PSO2OptBest4TSP(),
//				new PSO2OptBestQAP(),
//				new PSOBL2OptBest42SP(),
//				new PSONPS42SP(),				
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
				case MetaheuristicType.PSO:
					tuner = new PSOTuner(algorithm, instancesDir);
					break;
				default:
					break;
				}
				
				tuner.Start(algorithm.GetType().Name + ".txt");
			});
		}
	}
}
