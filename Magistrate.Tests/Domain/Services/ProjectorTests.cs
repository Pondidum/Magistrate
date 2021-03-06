﻿using Magistrate.Domain.Events;
using Magistrate.Domain.Events.UserEvents;
using Magistrate.Domain.Services;
using Magistrate.Infrastructure;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Domain.Services
{
	public class ProjectorTests
	{
		[Fact]
		public void When_projecting_a_non_registered_event()
		{
			var projector = new Projector();

			Should.NotThrow(() => projector.Apply(new UserDeletedEvent(new Operator())));
		}

		[Fact]
		public void When_projecting_an_event()
		{
			var count = 0;
			var projector = new Projector();
			projector.Register<UserDeletedEvent>(e => count++);

			projector.Apply(new UserDeletedEvent(new Operator()));

			count.ShouldBe(1);
		}

		[Fact]
		public void When_projecting_an_event_with_multiple_handlers()
		{
			var first = 0;
			var second = 0;

			var projector = new Projector();
			projector.Register<UserDeletedEvent>(e => first++);
			projector.Register<UserDeletedEvent>(e => second++);

			projector.Apply(new UserDeletedEvent(new Operator()));

			first.ShouldBe(1);
			second.ShouldBe(1);
		}

		[Fact]
		public void When_projecting_an_inherited_event()
		{
			var direct = 0;
			var inherited = 0;

			var projector = new Projector();
			projector.Register<UserDeletedEvent>(e => direct++);
			projector.Register<UserLoggedEvent>(e => inherited++);

			projector.Apply(new UserDeletedEvent(new Operator()));

			direct.ShouldBe(1);
			inherited.ShouldBe(1);
		}
	}
}
