using System;

namespace Magistrate.Domain
{
	public interface IKeyed
	{
		string Key { get; }
	}

	public interface IIdentity
	{
		Guid ID { get; }
	}
}
