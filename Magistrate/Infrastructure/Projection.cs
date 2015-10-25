using System;
using System.Collections.Generic;

namespace Magistrate.Infrastructure
{
	public class Projection
	{
		private readonly Dictionary<Type, List<Action<object>>> _aggregateProjections;

		public Projection()
		{
			_aggregateProjections = new Dictionary<Type, List<Action<object>>>();
		}

		public void Register<TAggregate>(Action<TAggregate> projection)
		{
			if (_aggregateProjections.ContainsKey(typeof(TAggregate)) == false)
				_aggregateProjections[typeof(TAggregate)] = new List<Action<object>>();

			_aggregateProjections[typeof(TAggregate)].Add(aggregate => projection((TAggregate)aggregate));
		}

		public void Run<TAggregate>(TAggregate aggregate)
		{
			List<Action<object>> projections;

			if (_aggregateProjections.TryGetValue(typeof (TAggregate), out projections) == false)
				return;

			projections.ForEach(projection => projection(aggregate));
		}
	}
}