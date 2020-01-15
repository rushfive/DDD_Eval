using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaigns.Domain.ValueObjects
{
	public class WorkflowId : Value<WorkflowId>
	{
		public Guid Value { get; }

		public WorkflowId(Guid value)
		{
			if (value == default)
				throw new ArgumentException("Workflow id must be specified.", nameof(value));
			Value = value;
		}

		public static implicit operator Guid(WorkflowId self) => self?.Value ?? default;
	}
}
