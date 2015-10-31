namespace Magistrate.Domain.Rules
{
	public interface IRule<T>
	{
		bool IsSatisfiedBy(T target);
		string GetMessage(T target);
	}
}
