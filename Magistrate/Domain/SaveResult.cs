using System.Collections.Generic;
using System.Linq;

namespace Magistrate.Domain
{
	public class SaveResult
	{
		public bool Success { get; set; }
		public IEnumerable<string> Messages { get; set; }

		public SaveResult()
		{
			Messages = Enumerable.Empty<string>();
		}

		public static SaveResult Pass()
		{
			return new SaveResult { Success = true };
		}

		public static SaveResult Fail(IEnumerable<string> messages)
		{
			return new SaveResult { Success = false, Messages = messages };
		}
	}
}
