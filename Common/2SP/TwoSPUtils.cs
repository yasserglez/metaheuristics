using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public static class TwoSPUtils
	{		
		public static int[] RandomSolution(TwoSPInstance instance)
		{
			int[] solution = new int[instance.NumberItems];
			List<int> items = new List<int>();
			
			for (int item = 0; item < instance.NumberItems; item++) {
				items.Add(item);
			}
			for (int i = 0; i < instance.NumberItems; i++) {
				int itemIndex = Statistics.RandomDiscreteUniform(0, items.Count - 1);
				int item = items[itemIndex];
				items.RemoveAt(itemIndex);
				solution[i] = item;
			}
			
			return solution;
		}
		
		public static int[] GetNeighbor(TwoSPInstance instance, int[] solution)
		{
			int[] neighbor = new int[instance.NumberItems];
			int a = Statistics.RandomDiscreteUniform(0, solution.Length - 1);
			int b = a;

			while (b == a) {
				b = Statistics.RandomDiscreteUniform(0, solution.Length - 1);
			}
			for (int i = 0; i < solution.Length; i++) {
				if (i == a) {
					neighbor[i] = solution[b];
				}
				else if (i == b) {
					neighbor[i] = solution[a];
				}
				else {
					neighbor[i] = solution[i];
				}
			}
			
			return neighbor;
		}		
		
		public static bool IsFeasible(TwoSPInstance instance, int[,] coordinates)
		{
			bool[] allocatedItems = new bool[instance.NumberItems];
			
			for (int item = 0; item < allocatedItems.Length; item++) {
				allocatedItems[item] = true;
			}
			for (int item = 0; item < coordinates.GetLength(0); item++) {
				allocatedItems[item] = false;
				if (!IsFeasible(instance, coordinates, allocatedItems, item)) {
					return false;
				}
				allocatedItems[item] = true;
			}
			return true;
		}
		
		public static int Fitness(TwoSPInstance instance, int[,] coordinates)
		{
			int totalHeight = 0;
			
			for (int item = 0; item < coordinates.GetLength(0); item++) {
				int itemHeight = coordinates[item,1] + instance.ItemsHeight[item];
				if (itemHeight > totalHeight) {
					totalHeight = itemHeight;
				}
			}
			
			return totalHeight;
		}
		
		private static bool IsFeasible(TwoSPInstance instance, int[,] coordinates, bool[] allocatedItem, int item)
		{
			int x = 0, y = 1;
			int itemXStart = coordinates[item,x];
			int itemXEnd = coordinates[item,x] + instance.ItemsWidth[item];
			int itemYStart = coordinates[item,y];
			int itemYEnd = coordinates[item,y] + instance.ItemsHeight[item];
			
			// Checking if the item is located inside the strip.
			if (itemXStart < 0 || itemXEnd > instance.StripWidth) {
				return false;
			}
			
			// Check if the item collapses with other item.
			for (int otherItem = 0; otherItem < coordinates.GetLength(0); otherItem++) {
				if (allocatedItem[otherItem]) {
					int otherItemXStart = coordinates[otherItem,x];
					int otherItemXEnd = coordinates[otherItem,x] + instance.ItemsWidth[otherItem];
					int otherItemYStart = coordinates[otherItem,y];
					int otherItemYEnd = coordinates[otherItem,y] + instance.ItemsHeight[otherItem];
					
					if (((otherItemXStart >= itemXStart && otherItemXStart < itemXEnd) ||
					     (otherItemXEnd > itemXStart && otherItemXEnd <= itemXEnd)) &&
					    ((otherItemYStart >= itemYStart && otherItemYStart < itemYEnd) ||
					     (otherItemYEnd > itemYStart && otherItemYEnd <= itemYEnd))) {
						return false;
					}
				}
			}
			
			return true;
		}		

		public static void Repair(TwoSPInstance instance, int[] individual)
		{			
			int itemsAllocatedCount = 0;
			bool[] itemsAllocated = new bool[instance.NumberItems];
			bool[] itemsRepeated = new bool[instance.NumberItems];
				
			// Get information to decide if the individual is valid.
			for (int item = 0; item < instance.NumberItems; item++) {
				if (!itemsAllocated[individual[item]]) {
					itemsAllocatedCount += 1;
					itemsAllocated[individual[item]] = true;
				}
				else {
					itemsRepeated[item] = true;
				}
			}

			// If the individual is invalid, make it valid.
			if (itemsAllocatedCount != instance.NumberItems) {
				for (int item = 0; item < itemsRepeated.Length; item++) {
					if (itemsRepeated[item]) {
						int count = Statistics.RandomDiscreteUniform(1, instance.NumberItems - itemsAllocatedCount);
						for (int i = 0; i < itemsAllocated.Length; i++) {
							if (!itemsAllocated[i]) {
								count -= 1;
								if (count == 0) {
									individual[item] = i;
									itemsRepeated[item] = false;
									itemsAllocated[i] = true;
									itemsAllocatedCount += 1;
									break;
								}
							}
						}
					}
				}
			}
		}		
		
		// Normal Pattern Shifting (NPS) placement heuristic.
		public static int[,] NPSCoordinates(TwoSPInstance instance, int[] ordering)
		{
			int x = 0, y = 1;
			int xpos, ypos;
			int currentItem;
			int[,] coordinates = new int[instance.NumberItems,2]; 
			List<int[]> topLeftSeeds = new List<int[]>(instance.NumberItems);
			List<int[]> bottomRightSeeds = new List<int[]>(instance.NumberItems);
			bool[] allocatedItems = new bool[instance.NumberItems];
			double finalQuality, currentQuality;
			int[] finalPosition = new int[2];
			
			// Set the coordinates of the first item.
			currentItem = ordering[0];
			coordinates[currentItem,x] = 0;
			coordinates[currentItem,y] = 0;
			allocatedItems[currentItem] = true;
			topLeftSeeds.Add(new int[] {0, instance.ItemsHeight[currentItem]});
			bottomRightSeeds.Add(new int[] {instance.ItemsWidth[currentItem], 0});
			
			// Set the coordinates of the rest of the items.
			for (int i = 1; i < instance.NumberItems; i++) {
				currentItem = ordering[i];
				finalQuality = double.MinValue;
				
				foreach (int[] position in topLeftSeeds) {
					// Move left the item as much as possible.

					// Check if we can put the item next to the left border of the strip.
					coordinates[currentItem,x] = 0;
					coordinates[currentItem,y] = position[y];
					if (NPSFeasible(instance, coordinates, allocatedItems, currentItem)) {
						// If this is valid, it's the best position.
						finalPosition[x] = coordinates[currentItem,x];
						finalPosition[y] = coordinates[currentItem,y];
						finalQuality = NPSQuality(instance, coordinates, allocatedItems, currentItem);
					}
					else {
						// Try to put the item next to other items.
						xpos = int.MaxValue;
						for (int otherItem = 0; otherItem < instance.NumberItems; otherItem++) {
							coordinates[currentItem,x] = coordinates[otherItem,x] + instance.ItemsWidth[otherItem];
							if (allocatedItems[otherItem] && coordinates[currentItem,x] < xpos) {
								if (NPSFeasible(instance, coordinates, allocatedItems, currentItem)) {
									currentQuality = NPSQuality(instance, coordinates, allocatedItems, currentItem);
									if (currentQuality > finalQuality || (currentQuality == finalQuality && 
									                                      coordinates[currentItem,x] <= finalPosition[x] && 
									                                      coordinates[currentItem,y] <= finalPosition[y])) {
										xpos = coordinates[currentItem,x];
										finalPosition[x] = coordinates[currentItem,x];
										finalPosition[y] = coordinates[currentItem,y];
										finalQuality = currentQuality;
									}
								}
							}
						}
					}
				}
				
				foreach (int[] position in bottomRightSeeds) {
					// Move down the item as much as possible.

					// Check if we can put the item at the bottom of the strip.
					coordinates[currentItem,x] = position[x];
					coordinates[currentItem,y] = 0;
					if (NPSFeasible(instance, coordinates, allocatedItems, currentItem)) {
						// If this is valid, it's the best position.
						finalPosition[x] = coordinates[currentItem,x];
						finalPosition[y] = coordinates[currentItem,y];
						finalQuality = NPSQuality(instance, coordinates, allocatedItems, currentItem);
					}
					else {
						// Try to put the item on top of other item.
						ypos = int.MaxValue;
						for (int otherItem = 0; otherItem < instance.NumberItems; otherItem++) {
							coordinates[currentItem,y] = coordinates[otherItem,y] + instance.ItemsHeight[otherItem];
							if (allocatedItems[otherItem] && coordinates[currentItem,y] < ypos) {
								if (NPSFeasible(instance, coordinates, allocatedItems, currentItem)) {
									currentQuality = NPSQuality(instance, coordinates, allocatedItems, currentItem);
									if (currentQuality > finalQuality || (currentQuality == finalQuality && 
									                                      coordinates[currentItem,x] <= finalPosition[x] && 
									                                      coordinates[currentItem,y] <= finalPosition[y])) {
										ypos = coordinates[currentItem,y];
										finalPosition[x] = coordinates[currentItem,x];
										finalPosition[y] = coordinates[currentItem,y];
										finalQuality = currentQuality;
									}
								}
							}
						}
					}
				}

				// Set the position of the current item.
				allocatedItems[currentItem] = true;
				coordinates[currentItem,x] = finalPosition[x];
				coordinates[currentItem,y] = finalPosition[y];
				
				// Update the lists with the seed positions.
				topLeftSeeds.Add(new int[] {coordinates[currentItem,x], coordinates[currentItem,y] + instance.ItemsHeight[currentItem]});
				bottomRightSeeds.Add(new int[] {coordinates[currentItem,x] + instance.ItemsWidth[currentItem], coordinates[currentItem,y]});
			}
			
			return coordinates;
		}		
		
		private static double NPSQuality(TwoSPInstance instance, int[,] coordinates, bool[] allocatedItem, int item)
		{
			int y = 1;
			double numerator = 0, denominator;
			double currentItemHeight, maxHeight = 0;

			for (int currentItem = 0; currentItem < instance.NumberItems; currentItem++) {
				if (allocatedItem[currentItem] || currentItem == item) {
					currentItemHeight = coordinates[currentItem,y] + instance.ItemsHeight[currentItem];		
					numerator += instance.ItemsWidth[currentItem] * currentItemHeight;
					if (currentItemHeight > maxHeight) {
						maxHeight = currentItemHeight;
					}
				}
			}
			denominator = instance.StripWidth * maxHeight;
			
			return numerator / denominator;
		}		
		
		private static bool NPSFeasible(TwoSPInstance instance, int[,] coordinates, bool[] allocatedItems, int item)
		{		
			int x = 0, y = 1;
			int itemXStart = coordinates[item,x];
			int itemXEnd = coordinates[item,x] + instance.ItemsWidth[item];
			int itemYStart = coordinates[item,y];
			int itemYEnd = coordinates[item,y] + instance.ItemsHeight[item];
			bool leftAdjacent = false;
			bool bottomAdjacent = false;	
			
			// The left-hand edge and the bottom edges should be both adjacent to other
			// items or to the edges of the strip.
			if (itemXStart == 0) leftAdjacent = true;
			if (itemYStart == 0) bottomAdjacent = true;			
			
			// Checking if the item is located inside the strip.
			if (itemXStart < 0 || itemXEnd > instance.StripWidth) {
				return false;
			}
			
			// Check if the item collapses with other item.
			for (int otherItem = 0; otherItem < coordinates.GetLength(0); otherItem++) {
				if (allocatedItems[otherItem]) {
					int otherItemXStart = coordinates[otherItem,x];
					int otherItemXEnd = coordinates[otherItem,x] + instance.ItemsWidth[otherItem];
					int otherItemYStart = coordinates[otherItem,y];
					int otherItemYEnd = coordinates[otherItem,y] + instance.ItemsHeight[otherItem];
					
					if (((otherItemXStart >= itemXStart && otherItemXStart < itemXEnd) ||
					     (otherItemXEnd > itemXStart && otherItemXEnd <= itemXEnd)) &&
					    ((otherItemYStart >= itemYStart && otherItemYStart < itemYEnd) ||
					     (otherItemYEnd > itemYStart && otherItemYEnd <= itemYEnd))) {
						return false;
					}
					
					// The left-hand edge and the bottom edges should be both adjacent to other
					// items or to the edges of the strip.						
					if (((otherItemXStart >= itemXStart && otherItemXStart < itemXEnd) ||
					     (otherItemXEnd > itemXStart && otherItemXEnd <= itemXEnd)) &&
					    (itemYStart == otherItemYEnd)) {
						bottomAdjacent = true;
					}
					
					if (((otherItemYStart >= itemYStart && otherItemYStart < itemYEnd) ||
					     (otherItemYEnd > itemYStart && otherItemYEnd <= itemYEnd)) &&
					    (itemXStart == otherItemXEnd)) {
						leftAdjacent = true;
					}
				}
			}
			
			return (leftAdjacent && bottomAdjacent);
		}
		
		
		public static void NPSLocalSearch2OptFirst(TwoSPInstance instance, int[] ordering)
		{
			int tmp;
			double currentFitness, bestFitness;

			bestFitness = Fitness(instance, NPSCoordinates(instance, ordering));			
			for (int j = 1; j < ordering.Length; j++) {
				for (int i = 0; i < j; i++) {
					// Swap the items.
					tmp = ordering[j];
					ordering[j] = ordering[i];
					ordering[i] = tmp;
					
					// Evaluate the fitness of this new solution.
					currentFitness = Fitness(instance, NPSCoordinates(instance, ordering));
					if (currentFitness < bestFitness) {
						return;
					}
					
					// Undo the swap.
					tmp = ordering[j];
					ordering[j] = ordering[i];
					ordering[i] = tmp;
				}
			}
		}
		
		public static void NPSLocalSearch2OptBest(TwoSPInstance instance, int[] ordering)
		{
			int tmp;
			int firstSwapItem = 0, secondSwapItem = 0;
			double currentFitness, bestFitness;
			
			bestFitness = Fitness(instance, NPSCoordinates(instance, ordering));
			for (int j = 1; j < ordering.Length; j++) {
				for (int i = 0; i < j; i++) {
					// Swap the items.
					tmp = ordering[j];
					ordering[j] = ordering[i];
					ordering[i] = tmp;
					
					// Evaluate the fitness of this new solution.
					currentFitness = Fitness(instance, NPSCoordinates(instance, ordering));
					if (currentFitness < bestFitness) {
						firstSwapItem = j;
						secondSwapItem = i;
						bestFitness = currentFitness;
					}
					
					// Undo the swap.
					tmp = ordering[j];
					ordering[j] = ordering[i];
					ordering[i] = tmp;
				}
			}
			
			// Use the best assignment.
			if (firstSwapItem != secondSwapItem) {
				tmp = ordering[firstSwapItem];
				ordering[firstSwapItem] = ordering[secondSwapItem];
				ordering[secondSwapItem] = tmp;
			}
		}
		
		// Bottom-Left (BL) placement heuristic.
		public static int[,] BLCoordinates(TwoSPInstance instance, int[] ordering)
		{
			int[,] coordinates = new int[instance.NumberItems,2];
			List<int> placedItems = new List<int>(instance.NumberItems);
			
			for (int i = 0; i < instance.NumberItems; i++) {
				BLPlaceItem(instance, coordinates, placedItems, ordering[i]);
				placedItems.Add(ordering[i]);
			}
			
			return coordinates;
		}
		
		private static void BLPlaceItem(TwoSPInstance instance, int[,] coordinates, List<int> placedItems, int item)
		{
			int x = 0, y = 1;
			int[] currentPosition = new int[2];
			int[] oldPosition = new int[2];
			bool moveLeft = false, moveDown = true;
			int itemXStart, itemXEnd, itemYStart, itemYEnd;
			int otherItemXStart, otherItemXEnd, otherItemYStart, otherItemYEnd;
			
			// Setting the starting position.
			currentPosition[x] = instance.StripWidth - instance.ItemsWidth[item];
			currentPosition[y] = int.MaxValue - instance.ItemsHeight[item];
			
			while (moveLeft || moveDown) {
				if (moveDown) {
					itemXStart = currentPosition[x];
					itemXEnd = itemXStart + instance.ItemsWidth[item];
					itemYStart = currentPosition[y];
					itemYEnd = itemYStart + instance.ItemsHeight[item];
					
					oldPosition[y] = currentPosition[y];

					currentPosition[y] = 0;
					foreach (int otherItem in placedItems) {
						otherItemXStart = coordinates[otherItem,x];
						otherItemXEnd = otherItemXStart + instance.ItemsWidth[otherItem];
						otherItemYStart = coordinates[otherItem,y];
						otherItemYEnd = otherItemYStart + instance.ItemsHeight[otherItem];
						
						if ((otherItemXStart >= itemXStart && otherItemXStart < itemXEnd) ||
						    (otherItemXEnd > itemXStart && otherItemXEnd <= itemXEnd) ||
						    (otherItemXStart < itemXStart && otherItemXEnd > itemXEnd)) {
							if (otherItemYEnd > currentPosition[y] && otherItemYEnd <= oldPosition[y]) {
								currentPosition[y] = otherItemYEnd;
							}
						}
					}
					
					moveDown = false;
					moveLeft = currentPosition[y] != oldPosition[y];
				}
				
				if (moveLeft) {
					itemXStart = currentPosition[x];
					itemXEnd = itemXStart + instance.ItemsWidth[item];
					itemYStart = currentPosition[y];
					itemYEnd = itemYStart + instance.ItemsHeight[item];
					
					oldPosition[x] = currentPosition[x];

					currentPosition[x] = 0;
					foreach (int otherItem in placedItems) {
						otherItemXStart = coordinates[otherItem,x];
						otherItemXEnd = otherItemXStart + instance.ItemsWidth[otherItem];
						otherItemYStart = coordinates[otherItem,y];
						otherItemYEnd = otherItemYStart + instance.ItemsHeight[otherItem];
							
						if ((otherItemYStart >= itemYStart && otherItemYStart < itemYEnd) ||
						    (otherItemYEnd > itemYStart && otherItemYEnd <= itemYEnd) ||
						    (otherItemYStart < itemYStart && otherItemYEnd > itemYEnd)) {
							if (otherItemXEnd > currentPosition[x] && otherItemXEnd <= oldPosition[x]) {
								currentPosition[x] = otherItemXEnd;
							}
						}
					}
					
					moveLeft = false;
					moveDown = currentPosition[x] != oldPosition[x];
				}
			}
			
			coordinates[item,x] = currentPosition[x];
			coordinates[item,y] = currentPosition[y];
		}
		
		public static void BLLocalSearch2OptFirst(TwoSPInstance instance, int[] ordering)
		{
			int tmp;
			double currentFitness, bestFitness;

			bestFitness = Fitness(instance, BLCoordinates(instance, ordering));			
			for (int j = 1; j < ordering.Length; j++) {
				for (int i = 0; i < j; i++) {
					// Swap the items.
					tmp = ordering[j];
					ordering[j] = ordering[i];
					ordering[i] = tmp;
					
					// Evaluate the fitness of this new solution.
					currentFitness = Fitness(instance, BLCoordinates(instance, ordering));
					if (currentFitness < bestFitness) {
						return;
					}
					
					// Undo the swap.
					tmp = ordering[j];
					ordering[j] = ordering[i];
					ordering[i] = tmp;
				}
			}
		}
		
		public static void BLLocalSearch2OptBest(TwoSPInstance instance, int[] ordering)
		{
			int tmp;
			int firstSwapItem = 0, secondSwapItem = 0;
			double currentFitness, bestFitness;
			
			bestFitness = Fitness(instance, BLCoordinates(instance, ordering));
			for (int j = 1; j < ordering.Length; j++) {
				for (int i = 0; i < j; i++) {
					// Swap the items.
					tmp = ordering[j];
					ordering[j] = ordering[i];
					ordering[i] = tmp;
					
					// Evaluate the fitness of this new solution.
					currentFitness = Fitness(instance, BLCoordinates(instance, ordering));
					if (currentFitness < bestFitness) {
						firstSwapItem = j;
						secondSwapItem = i;
						bestFitness = currentFitness;
					}
					
					// Undo the swap.
					tmp = ordering[j];
					ordering[j] = ordering[i];
					ordering[i] = tmp;
				}
			}
			
			// Use the best assignment.
			if (firstSwapItem != secondSwapItem) {
				tmp = ordering[firstSwapItem];
				ordering[firstSwapItem] = ordering[secondSwapItem];
				ordering[secondSwapItem] = tmp;
			}
		}		
	}
}
