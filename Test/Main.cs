using System;
using Metaheuristics;

namespace Test
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
		   	int time = 2000;
			string input = "../../../Problems/2SP/Instances/inst3.in";
			string output = "../../../Problems/2SP/Instances/inst3.out";
			
			//(new TSNPS42SP()).Start(input, output+"TSNPS42SP", time);
			(new TSBL42SP()).Start(input, output+"TSBL42SP", time);
		}
	}
}

