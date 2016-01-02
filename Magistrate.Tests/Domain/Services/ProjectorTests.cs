using Magistrate.Domain.Events;
using Magistrate.Domain.Events.UserEvents;
using Magistrate.Domain.Services;
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

			Should.NotThrow(() => projector.Project(new UserDeactivatedEvent()));
		}

		[Fact]
		public void When_projecting_an_event()
		{
			var count = 0;
			var projector = new Projector();
			projector.Add<UserDeactivatedEvent>(e => count++);

			projector.Project(new UserDeactivatedEvent());

			count.ShouldBe(1);
		}

		[Fact]
		public void When_projecting_an_event_with_multiple_handlers()
		{
			var first = 0;
			var second = 0;

			var projector = new Projector();
			projector.Add<UserDeactivatedEvent>(e => first++);
			projector.Add<UserDeactivatedEvent>(e => second++);

			projector.Project(new UserDeactivatedEvent());

			first.ShouldBe(1);
			second.ShouldBe(1);
		}

		[Fact]
		public void When_projectiong_an_inherited_event()
		{
			var direct = 0;
			var inherited = 0;

			var projector = new Projector();
			projector.Add<UserDeactivatedEvent>(e => direct++);
			projector.Add<UserLoggedEvent>(e => inherited++);

			projector.Project(new UserDeactivatedEvent());

			direct.ShouldBe(1);
			inherited.ShouldBe(1);
		}
	}
}