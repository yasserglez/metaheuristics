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
	
	public class TwoSPInstance
	{
		public int NumberItems { get; protected set; }
		
		public int StripWidth { get; protected set; }

		public int[] ItemsWidth { get; protected set; }
		
		public int[] ItemsHeight { get; protected set; }
		
		public TwoSPInstance(string file)
		{
			Regex regex = new Regex(@"\s+");
			
			using (StreamReader reader = File.OpenText(file)) {
				string line = "";
				
				// Getting the dimension.
				line = reader.ReadLine();
				while (line.Trim() == "") {
					line = reader.ReadLine();
				}
				NumberItems = int.Parse(regex.Split(line.Trim())[0]);
				
				// Getting the width of the strip.
				line = reader.ReadLine();
				while (line.Trim() == "") {
					line = reader.ReadLine();
				}
				StripWidth = int.Parse(regex.Split(line.Trim())[0]);
				
				// Getting height and width of each item.
				ItemsHeight = new int[NumberItems];
				ItemsWidth = new int[NumberItems];
				for (int i = 0; i < NumberItems; i++) {
					line = reader.ReadLine();
					while (line.Trim() == "") {
						line = reader.ReadLine();
					}			
					string[] parts = regex.Split(line.Trim());
					ItemsHeight[i] = int.Parse(parts[0]);
					ItemsWidth[i] = int.Parse(parts[1]);
				}
			}
		}
	}	
	
	public class SPPInstance
	{
		public SPPInstance(string file)
		{
		}
	}
}
