using System;
using System.Collections;
using System.Collections.Generic;

namespace Metaheuristics
{
	public static class Statistics
	{
		private static Random random = new Random();
		
		public static double RandomUniform()
		{
			return random.NextDouble();
		}
		
		public static double RandomUniform(double a, double b)
		{
			return a + (b - a) * random.NextDouble();
		}

        public static int RandomDiscreteUniform(int a, int b)
        {
            return (int) Math.Floor(RandomUniform(a, b + 1));
        }
		
		public static int RandomPoisson(double lambda)
		{
			int k = 0;
			double p = 1.0;
			double L = Math.Exp(-lambda);
			
			do {
				k++;
				p *= random.NextDouble();
			} while (p >= L);
			
			return k - 1;
		}
		
		public static double RandomExponential(double alpha)
		{
			return -Math.Log(random.NextDouble()) / alpha;
		}
		
		public static double Mean(ICollection<double> sample)
		{
			double mean = 0;
			
			foreach (double item in sample) {
				mean += item;
			}
			mean /= sample.Count;
			
			return mean;			
		}
		
		public static double Variance(ICollection<double> sample)
		{
			return Variance(sample, Mean(sample));
		}
			
		public static double Variance(ICollection<double> sample, double mean)	
		{
			double variance = 0;
	
			foreach (double item in sample) {
				variance += Math.Pow(item - mean, 2);
			}
			variance /= (sample.Count - 1);
			
			return variance;			
		}
		
		public static double StandardDeviation(ICollection<double> sample)
		{
			return StandardDeviation(sample, Mean(sample));
		}
		
		public static double StandardDeviation(ICollection<double> sample, double mean)
		{
			return Math.Sqrt(Variance(sample, mean));
		}		
	}
}
