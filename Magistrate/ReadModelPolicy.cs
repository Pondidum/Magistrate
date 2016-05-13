using System;
using StructureMap;
using StructureMap.Pipeline;

namespace Magistrate
{
	public class ReadModelPolicy : IInstancePolicy
	{
		public void Apply(Type pluginType, Instance instance)
		{
			if (instance.ReturnedType.Name.EndsWith("ReadModel"))
				instance.SetLifecycleTo<SingletonLifecycle>();
		}
	}
}
