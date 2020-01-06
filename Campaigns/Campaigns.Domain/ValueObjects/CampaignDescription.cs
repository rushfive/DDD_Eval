using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaigns.Domain.ValueObjects
{
	public class CampaignDescription : Value<CampaignDescription>
	{
		public static CampaignDescription FromString(string description) => new CampaignDescription(description);

		private readonly string _value;

		internal CampaignDescription(string value)
		{
			CheckValidity(value);
			_value = value;
		}

		public static implicit operator string(CampaignDescription self) => self?._value ?? default;

		private static void CheckValidity(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new ArgumentException("Campaign description must be specified.", nameof(value));
			if (value.Length > 256)
				throw new ArgumentOutOfRangeException("Campaign description cannot be longer than 256 characters.", nameof(value));
		}
	}
}
