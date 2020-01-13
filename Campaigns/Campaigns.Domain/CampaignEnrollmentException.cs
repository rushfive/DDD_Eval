using System;
using System.Collections.Generic;
using System.Text;

namespace Campaigns.Domain
{
	public class CampaignEnrollmentException : Exception
	{
		public Guid ParticipantId { get; }
		public Guid CampaignId{ get; }
		public CampaignEnrollmentException(Guid participantId, Guid campaignId)
		{
			ParticipantId = participantId;
			CampaignId = campaignId;
		}
	}
}
