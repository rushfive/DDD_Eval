using Campaigns.Domain;
using Campaigns.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WebApp.Contracts.CampaignEnrollment;

namespace WebApp.Api
{
	public class CampaignEnrollmentApplicationService
	{
		private readonly ICampaignEnrollmentCriteriaEvaluator _enrollmentCriteriaEvaluator;

		public CampaignEnrollmentApplicationService(
			ICampaignEnrollmentCriteriaEvaluator enrollmentCriteriaEvaluator)
		{
			_enrollmentCriteriaEvaluator = enrollmentCriteriaEvaluator;
		}

		public async Task Handle(V1.Enroll command)
		{
			if (!await _enrollmentCriteriaEvaluator.CanEnroll(command.ParticipantId, command.CampaignId))
			{
				throw new CampaignEnrollmentException(command.ParticipantId, command.CampaignId);
			}

			//var participantCampaign = new ParticipantCampaign();

			// TODO: add via ptp campaign repo

			// TODO: enroll via messaging??
		}
	}
}
