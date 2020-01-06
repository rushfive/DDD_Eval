using Campaigns.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Campaigns.Tests.ValueObjects
{
	public class CampaignIdTests
	{
		[Fact]
		public void CampaignId_objects_with_the_same_id_should_be_equal()
		{
			var id = Guid.NewGuid();
			var firstCampaignId = new CampaignId(id);
			var secondCampaignId = new CampaignId(id);
			Assert.Equal(firstCampaignId, secondCampaignId);
		}
	}
}
