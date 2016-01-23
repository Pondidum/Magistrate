using Magistrate.Domain.ReadModels;
using Shouldly;
using Xunit;

namespace Magistrate.Tests.Domain.ReadModels
{
	public class HistoryEntryTests
	{
		[Theory]
		[InlineData("TestEvent", "Test")]
		[InlineData("UserNameChangedEvent","User Name Changed")]
		[InlineData("SequenceIDEvent", "Sequence ID")]
		[InlineData("HTMLUpdated", "HTML Updated")]
		public void When_parsing_names(string eventName, string expected)
		{
			HistoryEntry.ActionFromEvent(eventName).ShouldBe(expected);
		} 
	}
}
