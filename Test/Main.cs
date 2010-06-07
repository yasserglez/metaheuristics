using System;
using Metaheuristics;

namespace Test
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			string fileInput = "../../../Problems/TSP/Instances/pr76.in";
			string fileOutput = "../../../Problems/TSP/Instances/pr76.out";
			GA4TSP.Start(fileInput, fileOutput, 5000);
		}
	}
}