using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public class NPS42SP : IMetaheuristic
	{
		public void Start(string fileInput, string fileOutput, int timeLimit)
		{
			TwoSPInstance instance = new TwoSPInstance(fileInput);
			List<Tuple<int,int>> sorting = new List<Tuple<int,int>>();
			for (int i = 0; i < instance.NumberItems; i++) {
				int area = instance.ItemsHeight[i] * instance.ItemsWidth[i];
				sorting.Add(new Tuple<int,int>(-area, i));
			}
			sorting.Sort();
			int[] ordering = new int[instance.NumberItems];
			for (int i = 0; i < instance.NumberItems; i++) {
				ordering[i] = sorting[i].Val2;
			}
			int[,] coordinates = TwoSPUtils.NPSCoordinates(instance, ordering);
			TwoSPSolution solution = new TwoSPSolution(instance, coordinates);
			solution.Write(fileOutput);
		}

		public string Name {
			get {
				return "NPS heuristic for 2SP";
			}
		}
		
		public MetaheuristicType Type {
			get {
				return MetaheuristicType.SH;
			}
		}
		
		public ProblemType Problem {
			get {
				return ProblemType.TwoSP;
			}
		}
		
		public string[] Team {
			get {
				return About.Team;
			}
		}
	}
}
