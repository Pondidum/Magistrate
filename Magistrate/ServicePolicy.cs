using System;
using StructureMap;
using StructureMap.Pipeline;

namespace Magistrate
{
	public class ServicePolicy : IInstancePolicy
	{
		public void Apply(Type pluginType, Instance instance)
		{
			if (instance.ReturnedType.Name.EndsWith("Service"))
				instance.SetLifecycleTo<SingletonLifecycle>();
		}
	}
}
