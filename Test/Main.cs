using System;
using Metaheuristics;

namespace Test
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			string fileInput = "../../../Problems/SPP/Instances/inst1.in";
			string fileOutput = "../../../Problems/SPP/Instances/inst1.out";
			GA2OptFirst4SPP.Start(fileInput, fileOutput, 5000);
		}
	}
}