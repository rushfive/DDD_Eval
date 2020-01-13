using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{
	public class InvalidEntityStateException : Exception
	{
		public InvalidEntityStateException(object entity, string message)
			: base($"Entity {entity.GetType().Name} state change rejected, {message}")
		{
			
		}
	}
}
