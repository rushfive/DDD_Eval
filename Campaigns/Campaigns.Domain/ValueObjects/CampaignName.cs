using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaigns.Domain.ValueObjects
{
	public class CampaignName : Value<CampaignName>
	{
		public string Value { get; }

		public CampaignName(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new ArgumentException("Campaign name must be specified.", nameof(value));
			Value = value;
		}

		public static implicit operator string(CampaignName self) => self?.Value ?? default;
	}
}
