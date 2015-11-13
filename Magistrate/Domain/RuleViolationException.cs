using System;
using System.Collections.Generic;
using System.Text;
using Ledger.Infrastructure;

namespace Magistrate.Domain
{
	public class RuleViolationException : Exception
	{
		public IEnumerable<string> Violations { get; }

		public RuleViolationException(IKeyed aggregate, List<string> violationMessages)
			: base(BuildMessage(aggregate, violationMessages))
		{
			Violations = violationMessages;
		}

		private static string BuildMessage(IKeyed aggregate, IEnumerable<string> messages)
		{
			var sb = new StringBuilder();

			sb.AppendLine($"Could not add the {aggregate.GetType().Name} '{aggregate.Key}' as:");
			sb.AppendLine();

			messages.ForEach(m => sb.AppendLine(m));

			return sb.ToString();
		}
	}
}
