using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Contracts
{
	public static  class CampaignEnrollment
	{
		public static class V1
		{
			public class Enroll
			{
				public Guid CampaignId { get; set; }
				public Guid ParticipantId { get; set; }
			}
		}
	}
}
