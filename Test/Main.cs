using System;
using Metaheuristics;

namespace Test
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
		    int time = 3000;
			string input = "../../../Problems/2SP/Instances/inst1.in";
			string output = "../../../Problems/2SP/Instances/inst1.out";
			(new BL2OptFirst42SP()).Start(input, output, time);
		}
	}
}
