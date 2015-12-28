using System;

namespace Magistrate.Domain
{
	public interface IKeyed<TKey>
	{
		TKey Key { get; }
	}

	public interface IIdentity
	{
		Guid ID { get; }
	}
}
