using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaigns.Domain.ValueObjects
{
	public class StepTaskId : Value<StepTaskId>
	{
		public Guid Value { get; }

		public StepTaskId(Guid value)
		{
			if (value == default)
				throw new ArgumentException("Step Task id must be specified.", nameof(value));
			Value = value;
		}

		public static implicit operator Guid(StepTaskId self) => self?.Value ?? default;
	}
}
