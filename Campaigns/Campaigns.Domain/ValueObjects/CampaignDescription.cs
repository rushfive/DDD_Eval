using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaigns.Domain.ValueObjects
{
	public class CampaignDescription : Value<CampaignDescription>
	{
		public static CampaignDescription FromString(string description) => new CampaignDescription(description);

		public string Value { get; }

		public CampaignDescription(string value)
		{
			CheckValidity(value);
			Value = value;
		}

		public static implicit operator string(CampaignDescription self) => self?.Value ?? default;

		private static void CheckValidity(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new ArgumentException("Campaign description must be specified.", nameof(value));
			if (value.Length > 256)
				throw new ArgumentOutOfRangeException("Campaign description cannot be longer than 256 characters.", nameof(value));
		}
	}
}
