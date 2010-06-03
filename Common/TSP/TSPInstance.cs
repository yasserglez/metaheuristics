using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Metaheuristics
{
	public class TSPInstance
	{
		public int NumberCities { get; protected set; }

		public double[,] Costs { get; protected set; }
		
		public TSPInstance(string file)
		{
			Regex regex = new Regex(@"\s+");
			double[] xCoords = null, yCoords = null;
			
			using (StreamReader reader = File.OpenText(file)) {
				string line = "";
				
				// Getting the dimension.
				NumberCities = -1;
				while (NumberCities == -1) {
					line = reader.ReadLine();
					if (line.StartsWith("DIMENSION")) {
						NumberCities = int.Parse(line.Substring(11));
						xCoords = new double[NumberCities];
						yCoords = new double[NumberCities];
						Costs = new double[NumberCities,NumberCities];
					}
				}
				
				// Getting the coordinates of the cities.
				while(!line.StartsWith("NODE_COORD_SECTION")) {
					line = reader.ReadLine();
				}
				for (int k = 0; k < NumberCities; k++) {
					line = reader.ReadLine();
					string[] parts = regex.Split(line.Trim());
					int i = int.Parse(parts[0]) - 1;
					xCoords[i] = int.Parse(parts[1]);
					yCoords[i] = int.Parse(parts[2]);
				}
			}
			
			// Building the matrix of distances.
			for (int i = 0; i < NumberCities; i++) {
				for (int j = 0; j < NumberCities; j++) {
					Costs[i,j] = Math.Sqrt(Math.Pow(xCoords[i] - xCoords[j], 2) + 
					                        Math.Pow(yCoords[i] - yCoords[j], 2));
				}
			}
		}
	}
}
