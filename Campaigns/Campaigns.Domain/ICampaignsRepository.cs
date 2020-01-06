using Campaigns.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Campaigns.Domain
{
	public interface ICampaignsRepository
	{
		Task<bool> Exists(CampaignId id);
		Task<bool> Exists(CampaignName name);
	}
}
