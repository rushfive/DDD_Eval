using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaigns.Domain.ValueObjects
{
	public class StepId : Value<StepId>
	{
		public Guid Value { get; }

		public StepId(Guid value)
		{
			if (value == default)
				throw new ArgumentException("Step id must be specified.", nameof(value));
			Value = value;
		}

		public static implicit operator Guid(StepId self) => self?.Value ?? default;
	}
}
