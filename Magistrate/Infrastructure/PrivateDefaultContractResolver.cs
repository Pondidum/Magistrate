using System;
using System.Collections.Generic;
using Magistrate.Domain.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Magistrate.Infrastructure
{
	public class PrivateDefaultContractResolver : DefaultContractResolver
	{
		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			if (typeof(UserLoggedEvent).IsAssignableFrom(type))
				return base.CreateProperties(type, MemberSerialization.Fields);

			return base.CreateProperties(type, memberSerialization);
		}
	}
}
