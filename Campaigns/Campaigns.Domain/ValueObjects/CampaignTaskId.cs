using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaigns.Domain.ValueObjects
{
	public class CampaignTaskId : Value<CampaignTaskId>
	{
		public Guid Value { get; }

		public CampaignTaskId(Guid value)
		{
			if (value == default)
				throw new ArgumentException("Campaign Task id must be specified.", nameof(value));
			Value = value;
		}

		public static CampaignTaskId GenerateNew() => new CampaignTaskId(Guid.NewGuid());

		public static implicit operator Guid(CampaignTaskId self) => self?.Value ?? default;
	}
}
