using Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Campaigns.Domain.ValueObjects
{
	public class CampaignId : Value<CampaignId>
	{
		public Guid Value { get; }

		public CampaignId(Guid value)
		{
			if (value == default)
				throw new ArgumentException("Campaign id must be specified.", nameof(value));
			Value = value;
		}

		public static implicit operator Guid(CampaignId self) => self?.Value ?? default;

		public static implicit operator CampaignId(Guid id) => new CampaignId(id);
	}
}
