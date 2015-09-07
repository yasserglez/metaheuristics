using System;
using Metaheuristics;

namespace Test
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			IMetaheuristic[] algorithms = new IMetaheuristic[] {
				new ACO2OptBest4QAP(),
				new ACO2OptBest4SPP(),
				new ACO2OptBest4TSP(),
				new ACO2OptFirst4QAP(),
				new ACO2OptFirst4SPP(),
				new ACO2OptFirst4TSP(),
				new ACO4QAP(),
				new ACO4SPP(),
				new ACO4TSP(),
				new ACOBL2OptBest42SP(),
				new ACOBL2OptFirst42SP(),
				new ACOBL42SP(),
				new ACONPS42SP(),
				new BL2OptBest42SP(),
				new BL2OptFirst42SP(),
				new GA2OptBest4QAP(),
				new GA2OptBest4SPP(),
				new GA2OptBest4TSP(),
				new GA2OptFirst4QAP(),
				new GA2OptFirst4SPP(),
				new GA2OptFirst4TSP(),
				new GA4QAP(),
				new GA4SPP(),
				new GA4TSP(),
				new GABL2OptBest42SP(),
				new GABL2OptFirst42SP(),
				new GABL42SP(),
				new GANPS42SP(),
				new GRASP2OptBest4QAP(),
				new GRASP2OptBest4SPP(),
				new GRASP2OptBest4TSP(),
				new GRASP2OptFirst4QAP(),
				new GRASP2OptFirst4SPP(),
				new GRASP2OptFirst4TSP(),
				new HMSAwGRASP2OptBest4QAP(),
				new HMSAwGRASP2OptBest4SPP(),
				new HMSAwGRASP2OptBest4TSP(),
				new HMSAwGRASP2OptFirst4QAP(),
				new HMSAwGRASP2OptFirst4SPP(),
				new HMSAwGRASP2OptFirst4TSP(),
				new HMTSwGRASP2OptBest4QAP(),
				new HMTSwGRASP2OptBest4SPP(),
				new HMTSwGRASP2OptBest4TSP(),
				new HMTSwGRASP2OptFirst4QAP(),
				new HMTSwGRASP2OptFirst4SPP(),
				new HMTSwGRASP2OptFirst4TSP(),
				new NPS42SP(),
				new PSO2OptBest4QAP(),
				new PSO2OptBest4TSP(),
				new PSO2OptFirst4QAP(),
				new PSO2OptFirst4TSP(),
				new PSO4QAP(),
				new PSO4TSP(),
				new PSOBL2OptBest42SP(),
				new PSOBL2OptFirst42SP(),
				new PSOBL42SP(),
				new PSONPS42SP(),
				new SA4QAP(),
				new SA4SPP(),
				new SA4TSP(),
				new SABL42SP(),
				new SANPS42SP(),
				new SS2OptBest4QAP(),
				new SS2OptBest4SPP(),
				new SS2OptBest4TSP(),
				new SS2OptFirst4QAP(),
				new SS2OptFirst4SPP(),
				new SS2OptFirst4TSP(),
				new SS4QAP(),
				new SS4SPP(),
				new SS4TSP(),
				new SSBL2OptBest42SP(),
				new SSBL2OptFirst42SP(),
				new SSBL42SP(),
				new SSNPS42SP(),
				new TS4QAP(),
				new TS4SPP(),
				new TS4TSP(),
				new TSBL42SP(),
				new TSNPS42SP(),
				new TwoOptBest4QAP(),
				new TwoOptBest4SPP(),
				new TwoOptBest4TSP(),
				new TwoOptFirst4QAP(),
				new TwoOptFirst4SPP(),
				new TwoOptFirst4TSP(),
				new UMDA2OptBest4QAP(),
				new UMDA2OptBest4SPP(),
				new UMDA2OptBest4TSP(),
				new UMDA2OptFirst4QAP(),
				new UMDA2OptFirst4SPP(),
				new UMDA2OptFirst4TSP(),
				new UMDA4QAP(),
				new UMDA4SPP(),
				new UMDA4TSP(),
				new UMDABL2OptBest42SP(),
				new UMDABL2OptFirst42SP(),
				new UMDABL42SP(),
				new UMDANPS42SP(),
			};

			foreach (IMetaheuristic algorithm in algorithms) {
				string fileInput = "", fileOutput = "";
				switch (algorithm.Problem) {
				case ProblemType.QAP:
					fileInput = "../../../Problems/QAP/Instances/sko56.in";
					fileOutput = "../../../Problems/QAP/Instances/sko56.out";
					break;
				case ProblemType.SPP:
					fileInput = "../../../Problems/SPP/Instances/inst1.in";
					fileOutput = "../../../Problems/SPP/Instances/inst1.out";
					break;
				case ProblemType.TSP:
					fileInput = "../../../Problems/TSP/Instances/bier127.in";
					fileOutput = "../../../Problems/TSP/Instances/bier127.out";
					break;
				case ProblemType.TwoSP:
					fileInput = "../../../Problems/2SP/Instances/inst3.in";
					fileOutput = "../../../Problems/2SP/Instances/inst3.out";
					break;
				default:
					break;
				}

				Console.Write("Running " + algorithm.Name + ": ");
				algorithm.Start(fileInput, fileOutput, 5000);
				Console.WriteLine("done.");
			}
		}
	}
}
