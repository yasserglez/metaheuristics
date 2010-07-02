using System;

using Metaheuristics;

namespace Tune
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			ITunableMetaheuristic[] algorithms = new ITunableMetaheuristic[] {
				#region To Implement
//				new ACOBL42SP(),
//				new ACONPS42SP(),
//				new SSNPS42SP(),
//				new TSNPS42SP(),
				
//				new ACO4TSP(),		
//				new SS4TSP(),		
//				new TS4TSP(),		
				
//				new ACO4QAP(),
//				new SS4QAP(),
//				new TS4QAP(),
				
//				new ACO4SPP(),		
//				new SS4SPP(),		
//				new TS4SPP(),		
				#endregion
				
				#region To be Tuned
//				new GA2OptBest4TSP(), /*A*/
//				new GA4TSP(), /*A*/ /*Ya*/
//				new GA2OptFirst4TSP(), /*A*/ /*Ya*/
//				new PSO2OptBest4TSP(), /*A*/
//				new PSO2OptFirst4TSP(),	/*A*/ /*Ya*/
//				new PSO4TSP(), /*A*/ /*Ya*/
//				new GRASP2OptBest4TSP(), /*A*/ /*Ya*/
//				new GRASP2OptFirst4TSP(), /*A*/	/*Ya*/
				new TS4TSP(), /*A*/
//				new SA4TSP(),		
//				new UMDA2OptBest4TSP(),		
//				new UMDA2OptFirst4TSP(),		
//				new UMDA4TSP(),	
//				
//				new GA2OptBest4QAP(), /*A*/
//				new GA2OptFirst4QAP(), /*A*/ /*Ya*/
//				new GA4QAP(), /*A*/ /*Ya*/
//				new PSO2OptBest4QAP(), /*A*/ 
//				new PSO2OptFirst4QAP(), /*A*/ /*Ya*/
//				new PSO4QAP(), /*A*/ /*Ya*/
//				new GRASP2OptBest4QAP(), /*A*/ /*Ya*/
//				new GRASP2OptFirst4QAP(), /*A*/ /*Ya*/
				new TS4QAP(), /*A*/
//				new SA4QAP(),
//				new UMDA2OptBest4QAP(),
//				new UMDA2OptFirst4QAP(),
//				new UMDA4QAP(),		
//				
//				new GA2OptBest4SPP(), /*A*/
//				new GA2OptFirst4SPP(),	/*A*/	
//				new GA4SPP(), /*A*/
//				new GRASP2OptBest4SPP(), /*A*/
//				new GRASP2OptFirst4SPP(), /*A*/	
				new TS4SPP(), /*A*/
//				new SA4SPP(),
//				new UMDA2OptBest4SPP(),		
//				new UMDA2OptFirst4SPP(),		
//				new UMDA4SPP(),		
//				
//				new GABL2OptBest42SP(), /*A*/
//				new GABL2OptFirst42SP(), /*A*/
//				new GABL42SP(), /*A*/
//				new GANPS42SP(), /*A*/
//				new PSOBL2OptBest42SP(), /*A*/
//				new PSOBL2OptFirst42SP(), /*A*/
//				new PSOBL42SP(), /*A*/
//				new PSONPS42SP(), /*A*/
				new TSBL42SP(), /*A*/
				new TSNPS42SP(), /*A*/
//				new SANPS42SP(),
//				new SABL42SP(),
//				new UMDABL2OptBest42SP(),
//				new UMDABL2OptFirst42SP(),
//				new UMDABL42SP(),
//				new UMDANPS42SP(),
				#endregion
				
				#region Already Tuned
//				new BL2OptBest42SP(),
//				new BL2OptFirst42SP(),
//				new NPS2OptFirst42SP(),
				
//				new TwoOptBest4TSP(),		
//				new TwoOptFirst4TSP(),		
				
//				new TwoOptBest4SPP(),		
//				new TwoOptFirst4SPP(),		
				
//				new TwoOptBest4QAP(),
//				new TwoOptFirst4QAP(),
				#endregion
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
				case MetaheuristicType.SS:
					tuner = new SSTuner(algorithm, instancesDir);
					break;
				case MetaheuristicType.TS:
					tuner = new TSTuner(algorithm, instancesDir);
					break;
				case MetaheuristicType.HM:
					tuner = new HMTuner(algorithm, instancesDir);
					break;
				case MetaheuristicType.ACO:
					tuner = new ACOTuner(algorithm, instancesDir);
					break;
				default:
					break;
				}
				
				tuner.Start(algorithm.GetType().Name + ".txt");
			});
		}
	}
}
