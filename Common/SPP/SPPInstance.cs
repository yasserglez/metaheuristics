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
			string[] parts;
			Regex regex = new Regex(@"\s+");
			
			using (StreamReader reader = File.OpenText(file)) {
				string line = "";
				
				// Getting the dimension.
				line = reader.ReadLine();
				while (line.Trim() == "") {
					line = reader.ReadLine();
				}
				parts = regex.Split(line.Trim());
				NumberItems = int.Parse(parts[0]);				
				
				// Getting the weights of the items.
				ItemsWeight = new double[NumberItems];
				for (int i = 0; i < NumberItems; i++) {
					line = reader.ReadLine();
					while (line.Trim() == "") {
						line = reader.ReadLine();
					}			
					parts = regex.Split(line.Trim());
					ItemsWeight[i] = int.Parse(parts[0]);
				}
				
				// Getting the number of subsets.
				line = reader.ReadLine();
				while (line.Trim() == "") {
					line = reader.ReadLine();
				}
				parts = regex.Split(line.Trim());
				NumberSubsets = int.Parse(parts[0]);				
				
				// Getting the weights of the subsets.
				SubsetsWeight = new double[NumberSubsets];
				for (int i = 0; i < NumberSubsets; i++) {
					line = reader.ReadLine();
					while (line.Trim() == "") {
						line = reader.ReadLine();
					}	
					parts = regex.Split(line.Trim());
					SubsetsWeight[i] = int.Parse(parts[0]);
				}				
			}
		}
	}
}
