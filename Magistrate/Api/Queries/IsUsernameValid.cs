using Magistrate.Domain;
using Magistrate.Domain.Services;

namespace Magistrate.Api.Queries
{
	public class IsUsernameValid
	{
		private readonly UserService _userService;

		public IsUsernameValid(UserService userService)
		{
			_userService = userService;
		}

		public object Execute(string key)
		{
			return new
			{
				Value = _userService.CanCreateUser(new UserKey(key))
			};
		}
	}
}
