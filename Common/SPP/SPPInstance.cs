using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Metaheuristics
{
	public class SPPInstance
	{
		public int NumberItems { get; protected set; }
		
		public double[] ItemsWeight { get; protected set; }
		
		public int NumberSubsets { get; protected set; }
		
		public double[] SubsetsWeight { get; protected set; }
		
		public SPPInstance(string file)
		{
			using (StreamReader reader = File.OpenText(file)) {
				string line = "";
				
				// Getting the dimension.
				line = reader.ReadLine();
				while (line.Trim() == "") {
					line = reader.ReadLine();
				}
				NumberItems = int.Parse(line.Trim());				
				
				// Getting the weights of the items.
				ItemsWeight = new double[NumberItems];
				for (int i = 0; i < NumberItems; i++) {
					line = reader.ReadLine();
					while (line.Trim() == "") {
						line = reader.ReadLine();
					}			
					ItemsWeight[i] = int.Parse(line);
				}
				
				// Getting the number of subsets.
				line = reader.ReadLine();
				while (line.Trim() == "") {
					line = reader.ReadLine();
				}
				NumberSubsets = int.Parse(line.Trim());				
				
				// Getting the weights of the subsets.
				SubsetsWeight = new double[NumberSubsets];
				for (int i = 0; i < NumberSubsets; i++) {
					line = reader.ReadLine();
					while (line.Trim() == "") {
						line = reader.ReadLine();
					}	
					SubsetsWeight[i] = int.Parse(line);
				}				
			}
		}
	}
}
