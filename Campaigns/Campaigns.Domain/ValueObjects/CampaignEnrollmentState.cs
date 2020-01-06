using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaigns.Domain.ValueObjects
{
	public class CampaignEnrollmentState : Value<CampaignEnrollmentState>
	{
		public bool EnrollmentSuspended { get; }
		public bool AutoEnrollNewParticipants { get; }

		internal CampaignEnrollmentState(
			bool enrollmentSuspended,
			bool autoEnrollNewParticipants)
		{
			EnrollmentSuspended = enrollmentSuspended;
			AutoEnrollNewParticipants = autoEnrollNewParticipants;
		}

		public static CampaignEnrollmentState Default => new CampaignEnrollmentState(false, false);

		public CampaignEnrollmentState SuspendEnrollments()
		{
			return new CampaignEnrollmentState(
				enrollmentSuspended: true,
				autoEnrollNewParticipants: AutoEnrollNewParticipants);
		}

		public CampaignEnrollmentState ResumeEnrollments()
		{
			return new CampaignEnrollmentState(
				enrollmentSuspended: false,
				autoEnrollNewParticipants: AutoEnrollNewParticipants);
		}

		public CampaignEnrollmentState StartAutoEnrollingNewParticipants()
		{
			return new CampaignEnrollmentState(
				enrollmentSuspended: EnrollmentSuspended,
				autoEnrollNewParticipants: true);
		}

		public CampaignEnrollmentState StopAutoEnrollingNewParticipants()
		{
			return new CampaignEnrollmentState(
				enrollmentSuspended: EnrollmentSuspended,
				autoEnrollNewParticipants: false);
		}

		public override string ToString()
		{
			return $"Enrollments: {(EnrollmentSuspended ? "Suspended" : "Active")}, {(AutoEnrollNewParticipants ? "Auto-Enrolled" : "Manually-Enrolled")}";
		}
	}
}
