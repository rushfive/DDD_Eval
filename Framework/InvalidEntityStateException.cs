using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{
	public class InvalidEntityStateException : Exception
	{
		public string SerializedEntity { get; }

		public InvalidEntityStateException(object entity, string message, object serializableRepresentation)
			: base($"Entity {entity.GetType().Name} state change rejected, {message}")
		{
			if (serializableRepresentation != null)
			{
				SerializedEntity = JsonConvert.SerializeObject(serializableRepresentation, Formatting.Indented);
			}
		}
	}
}
