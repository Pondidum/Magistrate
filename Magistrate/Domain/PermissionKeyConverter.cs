using System;
using Newtonsoft.Json;

namespace Magistrate.Domain
{
	public class PermissionKeyConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteValue(value.ToString());
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return new PermissionKey(Convert.ToString(reader.Value));
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(PermissionKey);
		}

		public override bool CanRead => true;
		public override bool CanWrite => true;
	}
}