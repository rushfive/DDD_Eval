using Campaigns.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaigns.Domain
{
	public static class Events
	{
		public class CampaignCreated
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
		}

		public class CampaignDescriptionUpdated
		{
			public Guid Id { get; set; }
			public string Description { get; set; }
		}

		public class CampaignEnrollmentSuspended
		{
			public Guid Id { get; set; }
		}

		public class CampaignTaskAdded
		{
			public string Name { get; set; }
			public string Description { get; set; }
			public TaskType Type { get; set; }
		}
	}
}
