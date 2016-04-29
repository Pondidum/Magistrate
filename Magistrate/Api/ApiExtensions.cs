using System.Diagnostics;
using Microsoft.Owin;

namespace Magistrate.Api
{
	public static class ApiExtensions
	{
		[DebuggerStepThrough]
		public static Operator GetOperator(this IOwinContext context)
		{
			return context.Get<Operator>("operator");
		}
	}
}
