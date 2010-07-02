using System;
using Metaheuristics;

namespace Test
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
		   int time = 600000;
			string input = "../../../Problems/SPP/Instances/inst1.in";
			string output = "../../../Problems/SPP/Instances/inst1.out";
			(new GA4SPP()).Start(input, output + ".GA4SPP", time);
			Console.WriteLine ("A");
			(new SA4SPP()).Start(input, output + ".SA4SPP", time);
			Console.WriteLine ("B");
			(new UMDA4SPP()).Start(input, output + ".UMDA4SPP", time);
		}
	}
}
