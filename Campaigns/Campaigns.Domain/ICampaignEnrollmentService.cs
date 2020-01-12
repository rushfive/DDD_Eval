using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Campaigns.Domain
{
	public interface ICampaignEnrollmentService
	{
		Task<bool> CanEnroll(Guid participantId, Guid campaignId);
	}
}
