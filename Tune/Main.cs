using System;

using Metaheuristics;

namespace Tune
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			ITunableMetaheuristic[] algorithms = new ITunableMetaheuristic[] {		
				#region Yasser
//				new SS2OptBest4QAP(),
//				new SS2OptBest4SPP(),
//				new ACOBL2OptFirst42SP(),
//				new SS2OptBest4TSP(),
//				new SSBL2OptBest42SP(),
				
//				new ACOBL2OptBest42SP(), /* OK */
//				new ACONPS42SP(), /* OK */
//				new BL2OptBest42SP(), /* OK */
//				new BL2OptFirst42SP(), /* OK */
//				new NPS42SP(), /* OK */
//				new SSBL2OptFirst42SP(), /* OK */
//				new SSBL42SP(), /* OK */
//				new UMDABL2OptBest42SP(), /* OK */
//				new UMDABL2OptFirst42SP(), /* OK */
//				new UMDABL42SP(), /* OK */
//				new UMDANPS42SP(), /* OK */
//				new UMDA4QAP(), /* OK */
//				new SANPS42SP(), /* OK */
//				new ACOBL42SP(), /* OK */
//				new SABL42SP(), /* OK */
//				new SSNPS42SP(), /* OK */
				
//				new ACO2OptFirst4QAP(), /* OK */
//				new ACO4QAP(), /* OK */
//				new SS4QAP(), /* OK */
//				new UMDA2OptFirst4QAP(), /* OK */
//				new SS2OptFirst4QAP(), /* OK */
//				new SA4QAP(), /* OK */
//				new UMDA2OptBest4QAP(), /* OK */
//				new ACO2OptBest4QAP(), /* OK */
			
//				new TwoOptBest4SPP(), /* OK */
//				new TwoOptFirst4SPP(), /* OK */
//				new ACO2OptBest4SPP(), /* OK */
//				new UMDA4SPP(), /* OK */
//				new SS2OptFirst4SPP(), /* OK */
//				new ACO2OptFirst4SPP(), /* OK */
//				new UMDA2OptFirst4SPP(), /* OK */
//				new ACO4SPP(), /* OK */
//				new SS4SPP(), /* OK */
//				new SA4SPP(), /* OK */
//				new UMDA2OptBest4SPP(), /* OK */
				
//				new UMDA4TSP(), /* OK */
//				new ACO2OptFirst4TSP(), /* OK */
//				new ACO4TSP(), /* OK */
//				new SS2OptFirst4TSP(), /* OK */
//				new SS4TSP(), /* OK */
//				new UMDA2OptBest4TSP(), /* OK */
//				new UMDA2OptFirst4TSP(), /* OK */
//				new SA4TSP(), /* OK */
//				new ACO2OptBest4TSP(), /* OK */
				#endregion
				
				#region Ariel
//				new GABL2OptBest42SP(), /* OK */
//				new GABL2OptFirst42SP(), /* OK */
//				new GABL42SP(), /* OK */ 
//				new GANPS42SP(), /* OK */
//				new PSOBL42SP(), /* OK */
//				new PSONPS42SP(), /* OK */
//				new PSOBL2OptBest42SP(), /* OK */
//				new PSOBL2OptFirst42SP(), /* OK */            
//				new TSNPS42SP(), /* OK */      
//				new TSBL42SP(), /* OK */
				
//				new TwoOptBest4TSP(),            
//				new TwoOptFirst4TSP(),            
//				new HMSAwGRASP2OptBest4TSP(),            
//				new HMSAwGRASP2OptFirst4TSP(),            
//				new GA2OptBest4TSP(), /* OK */
//				new GA2OptFirst4TSP(), /* OK */ 
//				new GA4TSP(), /* OK */
//				new GRASP2OptBest4TSP(), /* OK */            
//				new GRASP2OptFirst4TSP(), /* OK */
//				new HMTSwGRASP2OptBest4TSP(),  /* OK */          
//				new HMTSwGRASP2OptFirst4TSP(), /* OK */           
//				new PSO2OptBest4TSP(), /* OK */            
//				new PSO2OptFirst4TSP(), /* OK */            
//				new PSO4TSP(), /* OK */           
//				new TS4TSP(), /* OK */   
				
//				new TwoOptBest4QAP(),
//				new TwoOptFirst4QAP(),
//				new HMSAwGRASP2OptBest4QAP(),            
//				new HMSAwGRASP2OptFirst4QAP(),            
				new GA2OptBest4QAP(),
//				new GA2OptFirst4QAP(), /* OK */   
//				new GA4QAP(), /* OK */   
//				new GRASP2OptBest4QAP(), /* OK */   
//				new GRASP2OptFirst4QAP(), /* OK */   
//				new HMTSwGRASP2OptBest4QAP(), /* OK */   
//				new HMTSwGRASP2OptFirst4QAP(), /* OK */   
//				new PSO2OptBest4QAP(), /* OK */
//				new PSO2OptFirst4QAP(), /* OK */ 
//				new PSO4QAP(), /* OK */ 
//				new TS4QAP(), /* OK */ 
				
//				new GA2OptBest4SPP(),         
//				new GRASP2OptBest4SPP(),
//				new GRASP2OptFirst4SPP(),
//				new HMTSwGRASP2OptFirst4SPP(),            
//				new HMTSwGRASP2OptBest4SPP(),       
//				new HMSAwGRASP2OptBest4SPP(),            
//				new HMSAwGRASP2OptFirst4SPP(),            
//				new GA2OptFirst4SPP(),  /* OK */
//				new GA4SPP(), /* OK */           
//				new TS4SPP(), /* OK */   
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
