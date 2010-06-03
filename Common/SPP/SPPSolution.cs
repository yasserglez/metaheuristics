using System;
using System.IO;

namespace Metaheuristics
{	
	public class SPPSolution
	{
		public SPPInstance Instance { get; protected set; }
		
		public SPPSolution(SPPInstance instance)
		{
			Instance = instance;
		}
		
		public void Write(string file)
		{
		}
	}
}
