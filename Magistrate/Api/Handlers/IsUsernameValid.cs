using Magistrate.Domain;
using Magistrate.Domain.Services;
using Newtonsoft.Json;

namespace Magistrate.Api.Handlers
{
	public class IsUsernameValidHandler: IActionHandler
	{
		private readonly UserService _userService;

		public IsUsernameValidHandler(UserService userService)
		{
			_userService = userService;
		}

		public string ActionName { get { return "IS_USER_VALID"; } }

		public void Handle(HandlerActions e)
		{
			var dto = JsonConvert.DeserializeObject<Dto>(e.Json);
			var valid = _userService.CanCreateUser(dto.Key);

			e.Reply(new
			{
				Type = ActionName,
				IsValid = valid
			});
		}

		private class Dto
		{
			public UserKey Key { get; set; }
		}
	}
}
