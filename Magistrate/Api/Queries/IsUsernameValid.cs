using Magistrate.Domain;
using Magistrate.Domain.Services;

namespace Magistrate.Api.Queries
{
	public class IsUsernameValid
	{
		private readonly SystemFacade _system;

		public IsUsernameValid(SystemFacade system)
		{
			_system = system;
		}

		public object Execute(string key)
		{
			return new
			{
				Value = _system.CanCreateUser(new UserKey(key))
			};
		}
	}
}
