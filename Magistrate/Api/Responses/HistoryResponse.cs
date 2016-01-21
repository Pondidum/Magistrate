using System;

namespace Magistrate.Api.Responses
{
	public class HistoryResponse
	{
		public string Action { get; set; }
		public MagistrateUser By { get; set; }
		public DateTime At { get; set; }
		public string Description { get; set; }
	}
}
