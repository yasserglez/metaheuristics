using System;

using Metaheuristics;

namespace Tune
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			ITunableMetaheuristic[] algorithms = new ITunableMetaheuristic[] {		
//				new ACOBL2OptBest42SP(),
//				new ACONPS42SP(),
//				new BL2OptBest42SP(),
//				new BL2OptFirst42SP(),
//				new NPS42SP(),
//				new SSBL2OptFirst42SP(),
//				new SSBL42SP(),
//				new UMDABL2OptBest42SP(),
//				new UMDABL2OptFirst42SP(),
//				new UMDABL42SP(),
//				new UMDANPS42SP(),
//				new UMDA4QAP(),
//				new SANPS42SP(),
//				new ACOBL42SP(),
//				new SABL42SP(),
//				new SSNPS42SP(),
//				new ACOBL2OptFirst42SP(),
//				new SSBL2OptBest42SP(),				
//				new ACO2OptFirst4QAP(),
//				new ACO4QAP(),
//				new SS4QAP(),
//				new UMDA2OptFirst4QAP(),
//				new SS2OptFirst4QAP(),
//				new SA4QAP(),
//				new UMDA2OptBest4QAP(),
//				new ACO2OptBest4QAP(),
//				new SS2OptBest4QAP(),
//				new TwoOptBest4SPP(),
//				new TwoOptFirst4SPP(),
//				new ACO2OptBest4SPP(),
//				new UMDA4SPP(),
//				new SS2OptFirst4SPP(),
//				new ACO2OptFirst4SPP(),
//				new UMDA2OptFirst4SPP(),
//				new ACO4SPP(),
//				new SS4SPP(),
//				new SA4SPP(),
//				new UMDA2OptBest4SPP(),
//				new SS2OptBest4SPP(),
//				new UMDA4TSP(),
//				new ACO2OptFirst4TSP(),
//				new ACO4TSP(),
//				new SS2OptFirst4TSP(),
//				new SS4TSP(),
//				new UMDA2OptBest4TSP(),
//				new UMDA2OptFirst4TSP(),
//				new SA4TSP(),
//				new ACO2OptBest4TSP(),
//				new SS2OptBest4TSP(),
//				new GABL2OptBest42SP(),
//				new GABL2OptFirst42SP(),
//				new GABL42SP(), 
//				new GANPS42SP(),
//				new PSOBL42SP(),
//				new PSONPS42SP(),
//				new PSOBL2OptBest42SP(),
//				new PSOBL2OptFirst42SP(),            
//				new TSNPS42SP(),      
//				new TSBL42SP(),
//				new TwoOptBest4TSP(),            
//				new TwoOptFirst4TSP(),            
//				new HMSAwGRASP2OptBest4TSP(),            
//				new HMSAwGRASP2OptFirst4TSP(),            
//				new GA2OptBest4TSP(),
//				new GA2OptFirst4TSP(), 
//				new GA4TSP(),
//				new GRASP2OptBest4TSP(),            
//				new GRASP2OptFirst4TSP(),
//				new HMTSwGRASP2OptBest4TSP(),           
//				new HMTSwGRASP2OptFirst4TSP(),           
//				new PSO2OptBest4TSP(),            
//				new PSO2OptFirst4TSP(),            
//				new PSO4TSP(),           
//				new TS4TSP(),   
//				new TwoOptBest4QAP(),
//				new TwoOptFirst4QAP(),
//				new HMSAwGRASP2OptBest4QAP(),            
//				new HMSAwGRASP2OptFirst4QAP(),            
//				new GA2OptBest4QAP(),
//				new GA2OptFirst4QAP(),
//				new GA4QAP(),
//				new GRASP2OptBest4QAP(),
//				new GRASP2OptFirst4QAP(),
//				new HMTSwGRASP2OptBest4QAP(),
//				new HMTSwGRASP2OptFirst4QAP(),
//				new PSO2OptBest4QAP(),
//				new PSO2OptFirst4QAP(),
//				new PSO4QAP(),
//				new TS4QAP(),
//				new GA2OptBest4SPP(),
//				new GRASP2OptBest4SPP(),
//				new GRASP2OptFirst4SPP(),
//				new HMTSwGRASP2OptFirst4SPP(),
//				new HMTSwGRASP2OptBest4SPP(),
//				new HMSAwGRASP2OptBest4SPP(),
//				new HMSAwGRASP2OptFirst4SPP(),
//				new GA2OptFirst4SPP(),
//				new GA4SPP(),
//				new TS4SPP(),
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
