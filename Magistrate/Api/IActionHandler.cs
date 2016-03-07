namespace Magistrate.Api
{
	public interface IActionHandler
	{
		string ActionName { get; }
		void Handle(HandlerActions e);
	}
}
