using Campaigns.Domain.Entities;
using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaigns.Domain.ValueObjects
{
	public class CampaignEnrollmentConfiguration : Value<CampaignEnrollmentConfiguration>
	{
		public bool EnrollmentSuspended { get; internal set; }
		public bool AutoEnrollNewParticipants { get; internal set; }
		public Criteria EntranceCriteria { get; internal set; }

		internal CampaignEnrollmentConfiguration(
			bool enrollmentSuspended,
			bool autoEnrollNewParticipants,
			Criteria entranceCriteria)
		{
			EnrollmentSuspended = enrollmentSuspended;
			AutoEnrollNewParticipants = autoEnrollNewParticipants;
			EntranceCriteria = entranceCriteria;
		}

		public static CampaignEnrollmentConfiguration Default => new CampaignEnrollmentConfiguration(false, false, null);

		public CampaignEnrollmentConfiguration SuspendEnrollments()
		{
			return new CampaignEnrollmentConfiguration(
				enrollmentSuspended: true,
				autoEnrollNewParticipants: AutoEnrollNewParticipants,
				entranceCriteria: EntranceCriteria);
		}

		public CampaignEnrollmentConfiguration ResumeEnrollments()
		{
			return new CampaignEnrollmentConfiguration(
				enrollmentSuspended: false,
				autoEnrollNewParticipants: AutoEnrollNewParticipants,
				entranceCriteria: EntranceCriteria);
		}

		public CampaignEnrollmentConfiguration StartAutoEnrollingNewParticipants()
		{
			return new CampaignEnrollmentConfiguration(
				enrollmentSuspended: EnrollmentSuspended,
				autoEnrollNewParticipants: true,
				entranceCriteria: EntranceCriteria);
		}

		public CampaignEnrollmentConfiguration StopAutoEnrollingNewParticipants()
		{
			return new CampaignEnrollmentConfiguration(
				enrollmentSuspended: EnrollmentSuspended,
				autoEnrollNewParticipants: false,
				entranceCriteria: EntranceCriteria);
		}

		public CampaignEnrollmentConfiguration UpdateEntranceCriteria(Criteria entranceCriteria)
		{
			return new CampaignEnrollmentConfiguration(
				enrollmentSuspended: EnrollmentSuspended,
				autoEnrollNewParticipants: AutoEnrollNewParticipants,
				entranceCriteria: entranceCriteria);
		}

		public override string ToString()
		{
			return $"Enrollments: {(EnrollmentSuspended ? "Suspended" : "Active")}, {(AutoEnrollNewParticipants ? "Auto-Enrolled" : "Manually-Enrolled")}";
		}

		internal CampaignEnrollmentConfiguration() { }
	}
}
