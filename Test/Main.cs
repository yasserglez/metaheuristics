using System;
using Metaheuristics;

namespace Test
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
		    int timeLimit = 1000;
		    
			string input = "../../../Problems/QAP/Instances/had20.in";
			string outputGA = "../../../Problems/QAP/Instances/had20_GA.out";
			string outputGRASP2OptBest = "../../../Problems/QAP/Instances/had20_GRASP2B.out";
			string outputGRASP2OptFirst = "../../../Problems/QAP/Instances/had20_GRASP2F.out";
			string outputUMDA = "../../../Problems/QAP/Instances/had20_UMDA.out";
			string outputUMDA2OptBest = "../../../Problems/QAP/Instances/had20_UMDA2B.out";
			string outputUMDA2OptFirst = "../../../Problems/QAP/Instances/had20_UMDA2F.out";
			
			GA4QAP.Start(input, outputGA, timeLimit);
			GRASP2OptBest4QAP.Start(input, outputGRASP2OptBest, timeLimit);
			GRASP2OptFirst4QAP.Start(input, outputGRASP2OptFirst, timeLimit);
			UMDA4QAP.Start(input, outputUMDA, timeLimit);
			UMDA2OptBest4QAP.Start(input, outputUMDA2OptBest, timeLimit);
			UMDA2OptFirst4QAP.Start(input, outputUMDA2OptFirst, timeLimit);
		}
	}
}
