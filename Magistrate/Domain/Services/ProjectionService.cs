using System;
using Ledger;
using Magistrate.Infrastructure;

namespace Magistrate.Domain.Services
{
	public class ProjectionService
	{
		private readonly Projector _projections;

		public ProjectionService()
		{
			_projections = new Projector();
		}

		protected void Register<TEvent>(Action<TEvent> projection)
			where TEvent : DomainEvent<Guid>
		{
			_projections.Register(projection);
		}

		public void Project(DomainEvent<Guid> @event)
		{
			_projections.Apply(@event);
		}
	}
}
