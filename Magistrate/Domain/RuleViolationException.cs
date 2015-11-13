using System;
using System.Collections.Generic;

namespace Magistrate.Domain
{
	public class RuleViolationException : Exception
	{
		public RuleViolationException(IEnumerable<string> violationMessages)
		{

		}
	}
}
