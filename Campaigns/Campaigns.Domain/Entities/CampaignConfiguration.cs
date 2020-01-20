using Campaigns.Domain.ValueObjects;
using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaigns.Domain.Entities
{
	public class CampaignConfiguration : Value<CampaignConfiguration>
	{
		public CampaignBasicInfo BasicInfo { get; private set; }
		public CampaignEnrollmentConfiguration Enrollment { get; private set; }



	}

	//public class CampaignConfigurationId : Value<CampaignConfigurationId>
	//{
	//	public Guid Value { get; }

	//	public CampaignConfigurationId(Guid value)
	//	{
	//		if (value == default)
	//			throw new ArgumentException("Campaign configuration id must be specified.", nameof(value));
	//		Value = value;
	//	}

	//	public static implicit operator Guid(CampaignConfigurationId self) => self?.Value ?? default;

	//	public static implicit operator CampaignConfigurationId(Guid id) => new CampaignConfigurationId(id);
	//}
}
