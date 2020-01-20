using Campaigns.Domain.Entities;
using Campaigns.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Campaigns.Domain
{
	public interface ICampaignsRepository
	{
		Task<bool> Exists(Guid id);
		//Task<bool> Exists(string name);
		//Task<Campaign> Load(Guid id);
		//Task Save(Campaign campaign);
	}
}
