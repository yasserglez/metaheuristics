using System;
using Metaheuristics;

namespace Test
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
		    int time = 3000;
			string input = "../../../Problems/QAP/Instances/had20.in";
			string output = "../../../Problems/QAP/Instances/had20.out";
			(new TwoOptBest4QAP()).Start(input, output, time);
		}
	}
}
