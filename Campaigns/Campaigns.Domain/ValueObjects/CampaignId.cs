using Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Campaigns.Domain.ValueObjects
{
	public class CampaignId : Value<CampaignId>
	{
		private readonly Guid Value;

		internal CampaignId(Guid value)
		{
			if (value == default)
				throw new ArgumentException("Campaign id must be specified.", nameof(value));
			Value = value;
		}

		public static implicit operator Guid(CampaignId self) => self?.Value ?? default;
	}
}
