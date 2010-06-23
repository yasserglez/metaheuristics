using System;

namespace Metaheuristics
{
	public interface ITunableMetaheuristic : IMetaheuristic
	{
		void UpdateParameters(double[] parameters);
	}
}
