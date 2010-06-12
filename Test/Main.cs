using System;
using Metaheuristics;

namespace Test
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
		    int timeLimit = 3000;
		    
			string input = "../../../Problems/QAP/Instances/had20.in";
			string outputGA2OptBest = "../../../Problems/QAP/Instances/GA2OptBest.out";
			string outputGA2OptFirst = "../../../Problems/QAP/Instances/GA2OptFirst.out";
			string outputGA = "../../../Problems/QAP/Instances/GA.out";
			string outputGRASP2OptBest = "../../../Problems/QAP/Instances/GRASP2OptBest.out";
			string outputGRASP2OptFirst = "../../../Problems/QAP/Instances/GRASP2OptFirst.out";
			string outputPSO2OptBest = "../../../Problems/QAP/Instances/PSO2OptBest.out";
			string outputPSO2OptFirst = "../../../Problems/QAP/Instances/PSO2OptFirst.out";			
			string outputSA = "../../../Problems/QAP/Instances/SA.out";
			string outputUMDA2OptBest = "../../../Problems/QAP/Instances/UMDA2OptBest.out";
			string outputUMDA2OptFirst = "../../../Problems/QAP/Instances/UMDA2OptFirst.out";
			string outputUMDA = "../../../Problems/QAP/Instances/UMDA.out";
			
			(new GA2OptBest4QAP()).Start(input, outputGA2OptBest, timeLimit);
			(new GA2OptFirst4QAP()).Start(input, outputGA2OptFirst, timeLimit);
			(new GA4QAP()).Start(input, outputGA, timeLimit);
			(new GRASP2OptBest4QAP()).Start(input, outputGRASP2OptBest, timeLimit);
			(new GRASP2OptFirst4QAP()).Start(input, outputGRASP2OptFirst, timeLimit);
			(new PSO2OptBestQAP()).Start(input, outputPSO2OptBest, timeLimit);
			(new PSO2OptFirst4QAP()).Start(input, outputPSO2OptFirst, timeLimit);			
			(new SA4QAP()).Start(input, outputSA, timeLimit);
			(new UMDA2OptBest4QAP()).Start(input, outputUMDA2OptBest, timeLimit);
			(new UMDA2OptFirst4QAP()).Start(input, outputUMDA2OptFirst, timeLimit);
			(new UMDA4QAP()).Start(input, outputUMDA, timeLimit);
		}
	}
}
