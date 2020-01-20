using Campaigns.Domain.Entities;
using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaigns.Domain.ValueObjects
{
	public class CampaignEnrollmentConfiguration : Value<CampaignEnrollmentConfiguration>
	{
		public bool Suspended { get; internal set; }
		public bool AutoEnroll { get; internal set; }
		//public Criteria EntranceCriteria { get; internal set; }

		internal CampaignEnrollmentConfiguration(
			bool enrollmentSuspended,
			bool autoEnrollNewParticipants)
			//Criteria entranceCriteria)
		{
			Suspended = enrollmentSuspended;
			AutoEnroll = autoEnrollNewParticipants;
			//EntranceCriteria = entranceCriteria;
		}

		public static CampaignEnrollmentConfiguration Default => new CampaignEnrollmentConfiguration(false, false);

		public CampaignEnrollmentConfiguration SuspendEnrollments()
		{
			return new CampaignEnrollmentConfiguration(
				enrollmentSuspended: true,
				autoEnrollNewParticipants: AutoEnroll);
				//entranceCriteria: EntranceCriteria);
		}

		public CampaignEnrollmentConfiguration ResumeEnrollments()
		{
			return new CampaignEnrollmentConfiguration(
				enrollmentSuspended: false,
				autoEnrollNewParticipants: AutoEnroll);
				//entranceCriteria: EntranceCriteria);
		}

		public CampaignEnrollmentConfiguration StartAutoEnrollingNewParticipants()
		{
			return new CampaignEnrollmentConfiguration(
				enrollmentSuspended: Suspended,
				autoEnrollNewParticipants: true);
			//entranceCriteria: EntranceCriteria);
		}

		public CampaignEnrollmentConfiguration StopAutoEnrollingNewParticipants()
		{
			return new CampaignEnrollmentConfiguration(
				enrollmentSuspended: Suspended,
				autoEnrollNewParticipants: false);
			//entranceCriteria: EntranceCriteria);
		}

		public CampaignEnrollmentConfiguration UpdateEntranceCriteria(Criteria entranceCriteria)
		{
			return new CampaignEnrollmentConfiguration(
				enrollmentSuspended: Suspended,
				autoEnrollNewParticipants: AutoEnroll);
			//entranceCriteria: entranceCriteria);
		}

		public override string ToString()
		{
			return $"Enrollments: {(Suspended ? "Suspended" : "Active")}, {(AutoEnroll ? "Auto-Enrolled" : "Manually-Enrolled")}";
		}

		internal CampaignEnrollmentConfiguration() { }
	}
}
