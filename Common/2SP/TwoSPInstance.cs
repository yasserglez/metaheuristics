using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Metaheuristics
{		
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
}
