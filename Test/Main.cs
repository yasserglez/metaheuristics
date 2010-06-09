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
			string outputGA2OptBest = "../../../Problems/QAP/Instances/had20_GA2OptBest.out";
			string outputGA2OptFirst = "../../../Problems/QAP/Instances/had20_GA2OptFirst.out";
			string outputGA = "../../../Problems/QAP/Instances/had20_GA.out";
			string outputGRASP2OptBest = "../../../Problems/QAP/Instances/had20_GRASP2OptBest.out";
			string outputGRASP2OptFirst = "../../../Problems/QAP/Instances/had20_GRASP2OptFirst.out";
			string outputSA = "../../../Problems/QAP/Instances/had20_SA.out";
			string outputUMDA2OptBest = "../../../Problems/QAP/Instances/had20_UMDA2OptBest.out";
			string outputUMDA2OptFirst = "../../../Problems/QAP/Instances/had20_UMDA2OptFirst.out";
			string outputUMDA = "../../../Problems/QAP/Instances/had20_UMDA.out";
			
			GA2OptBest4QAP.Start(input, outputGA2OptBest, timeLimit);
			GA2OptFirst4QAP.Start(input, outputGA2OptFirst, timeLimit);
			GA4QAP.Start(input, outputGA, timeLimit);
			GRASP2OptBest4QAP.Start(input, outputGRASP2OptBest, timeLimit);
			GRASP2OptFirst4QAP.Start(input, outputGRASP2OptFirst, timeLimit);
			SA4QAP.Start(input, outputSA, timeLimit);
			UMDA2OptBest4QAP.Start(input, outputUMDA2OptBest, timeLimit);
			UMDA2OptFirst4QAP.Start(input, outputUMDA2OptFirst, timeLimit);
			UMDA4QAP.Start(input, outputUMDA, timeLimit);
		}
	}
}
