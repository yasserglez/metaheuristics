using System;
using Metaheuristics;

namespace Test
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			string fileInput = "../../../Problems/QAP/Instances/nug15.in";
			string fileOutput = "../../../Problems/QAP/Instances/nug15.out";
			UMDA2OptBest4QAP.Start(fileInput, fileOutput, 10000);
		}
	}
}
