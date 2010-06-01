using System;
using Metaheuristics;

namespace Test
{
	public class MainClass
	{
		public static void Main(string[] args)
		{
			string fileInput, fileOutput;
			
			if (args.Length == 2) {
				// Files from command line arguments.
				fileInput = args[0];
				fileOutput = args[1];
			}
			else {
				// Hardcoded files. Util for debugging?
				fileInput = "";
				fileOutput = "";
			}
		}
	}
}
