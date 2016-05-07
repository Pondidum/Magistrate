namespace Magistrate.Domain.Events.RoleEvents
{
	public class RoleDescriptionChangedEvent : UserLoggedEvent
	{
		public RoleDescriptionChangedEvent(Operator @operator, string newDescription) : base(@operator)
		{
			NewDescription = newDescription;
		}

		public string NewDescription { get; }
	}
}
