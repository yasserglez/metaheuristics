using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Metaheuristics
{	
	public class QAPInstance
	{
		public int NumberFacilities { get; protected set; }
		
		public double[,] Distances { get; protected set; }
		
		public double[,] Flows { get; protected set; }
		
		public QAPInstance(string file)
		{
			Regex regex = new Regex(@"\s+");
			
			using (StreamReader reader = File.OpenText(file)) {
				string line = "";
				
				// Getting the dimension.
				line = reader.ReadLine();
				while (line.Trim() == "") {
					line = reader.ReadLine();
				}
				NumberFacilities = int.Parse(line.Trim());
				
				// Getting the distance matrix.			
				Distances = new double[NumberFacilities,NumberFacilities];
				for (int i = 0; i < NumberFacilities; i++) {
					line = reader.ReadLine();
					while (line.Trim() == "") {
						line = reader.ReadLine();
					}			
					string[] parts = regex.Split(line.Trim());
					for (int j = 0; j < NumberFacilities; j++) {
						Distances[i,j] = double.Parse(parts[j]);
					}
				}
				
				// Getting the flow matrix.
				Flows = new double[NumberFacilities,NumberFacilities];
				for (int i = 0; i < NumberFacilities; i++) {
					line = reader.ReadLine();
					while (line.Trim() == "") {
						line = reader.ReadLine();
					}
					string[] parts = regex.Split(line.Trim());
					for (int j = 0; j < NumberFacilities; j++) {
						Flows[i,j] = double.Parse(parts[j]);
					}
				}
			}
		}			
	}
}
