using Campaigns.Domain.Entities;
using Campaigns.Domain.ValueObjects;
using Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Campaigns.Tests.ValueObjects
{
	public class CampaignTests
	{
		private readonly Campaign _campaign;

		public CampaignTests()
		{

		}

		[Fact]
		public void Create_campaign_without_id_specified_throws()
		{
			Assert.Throws<ArgumentException>(
				() => new Campaign(null, new CampaignName("name")));
		}

		[Fact]
		public void Create_campaign_without_name_specified_throws()
		{
			Assert.Throws<ArgumentException>(
				() => new Campaign(new CampaignId(Guid.NewGuid()), null));
		}
	}
}
