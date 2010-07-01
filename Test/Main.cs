using System;
using Metaheuristics;

namespace Test
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
		    int time = 10000;
			string input = "../../../Problems/SPP/Instances/inst2.in";
			string output = "../../../Problems/SPP/Instances/inst2.out";
			(new GA4SPP()).Start(input, output + ".GA4SPP", time);
			(new SA4SPP()).Start(input, output + ".SA4SPP", time);
		}
	}
}
