using System;
using System.IO;

namespace Metaheuristics
{
	public class TSPInstance
	{
		public int Dimension { get; protected set; }

		public double[,] Costs { get; protected set; }
		
		public TSPInstance(string file)
		{
			double[] xCoords = null, yCoords = null;
			
			using (StreamReader reader = File.OpenText(file)) {
				string line = "";
				
				// Getting the dimension.
				Dimension = -1;
				while (Dimension == -1) {
					line = reader.ReadLine();
					if (line.StartsWith("DIMENSION")) {
						Dimension = int.Parse(line.Substring(11));
						xCoords = new double[Dimension];
						yCoords = new double[Dimension];
						Costs = new double[Dimension,Dimension];
					}
				}
				
				// Getting the coordinates of the cities.
				while(!line.StartsWith("NODE_COORD_SECTION")) {
					line = reader.ReadLine();
				}
				for (int k = 0; k < Dimension; k++) {
					line = reader.ReadLine();
					string[] parts = line.Split(' ');
					int i = int.Parse(parts[0]) - 1;
					xCoords[i] = int.Parse(parts[1]);
					yCoords[i] = int.Parse(parts[2]);
				}
			}
			
			// Building the matrix of distances.
			for (int i = 0; i < Dimension; i++) {
				for (int j = 0; j < Dimension; j++) {
					Costs[i,j] = Math.Sqrt(Math.Pow(xCoords[i] - xCoords[j], 2) + 
					                        Math.Pow(yCoords[i] - yCoords[j], 2));
				}
			}
		}
	}
	
	public class QAPInstance
	{
		public int Dimension { get; protected set; }
		
		public double[,] Distances { get; protected set; }
		
		public double[,] Flows { get; protected set; }
		
		public QAPInstance(string file)
		{
			using (StreamReader reader = File.OpenText(file)) {
				string line = "";
				
				// Getting the dimension.
				line = reader.ReadLine();
				while (line.Trim() == "") {
					line = reader.ReadLine();
				}
				Dimension = int.Parse(line);
				
				// Getting the distance matrix.			
				Distances = new double[Dimension,Dimension];
				for (int i = 0; i < Dimension; i++) {
					line = reader.ReadLine();
					while (line.Trim() == "") {
						line = reader.ReadLine();
					}			
					string[] parts = line.Split(' ');
					for (int j = 0; j < Dimension; j++) {
						Distances[i,j] = double.Parse(parts[j]);
					}
				}
				
				// Getting the flow matrix.
				Flows = new double[Dimension,Dimension];
				for (int i = 0; i < Dimension; i++) {
					line = reader.ReadLine();
					while (line.Trim() == "") {
						line = reader.ReadLine();
					}
					string[] parts = line.Split(' ');
					for (int j = 0; j < Dimension; j++) {
						Flows[i,j] = double.Parse(parts[j]);
					}
				}
			}
		}			
	}
	
	public class TwoSPInstance
	{
		public TwoSPInstance(string file)
		{
		}
	}	
	
	public class SPPInstance
	{
		public SPPInstance(string file)
		{
		}
	}
}
