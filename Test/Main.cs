using System;
using Metaheuristics;

namespace Test
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			string fileInput = "../../../Problems/QAP/Instances/had20.in";
			string fileOutputGA = "../../../Problems/QAP/Instances/had20_GA.out";
			string fileOutputGRASP2B = "../../../Problems/QAP/Instances/had20_GRASP2B.out";
			string fileOutputGRASP2F = "../../../Problems/QAP/Instances/had20_GRASP2F.out";
			string fileOutputUMDA = "../../../Problems/QAP/Instances/had20_UMDA.out";
			string fileOutputUMDA2B = "../../../Problems/QAP/Instances/had20_UMDA2B.out";
			string fileOutputUMDA2F = "../../../Problems/QAP/Instances/had20_UMDA2F.out";
			GA4QAP.Start(fileInput, fileOutputGA, 10000);
			GRASP2OptBest4QAP.Start(fileInput, fileOutputGRASP2B, 10000);
			GRASP2OptFirst4QAP.Start(fileInput, fileOutputGRASP2F, 10000);
			UMDA4QAP.Start(fileInput, fileOutputUMDA, 10000);
			UMDA2OptBest4QAP.Start(fileInput, fileOutputUMDA2B, 10000);
			UMDA2OptFirst4QAP.Start(fileInput, fileOutputUMDA2F, 10000);
		}
	}
}