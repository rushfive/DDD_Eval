using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Campaigns.Domain
{
	public interface ICampaignEnrollmentCriteriaEvaluator
	{
		Task<bool> CanEnroll(Guid participantId, Guid campaignId);
	}
}
