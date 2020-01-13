using Campaigns.Domain.ValueObjects;
using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaigns.Domain.Entities
{
	public class CampaignEnrollables : Entity
	{
		public CampaignId Id { get; private set; }
		public List<Workflow> Workflows { get; }
		public List<CampaignTask> Tasks { get; }

		protected override void EnsureValidState()
		{
			throw new NotImplementedException();
		}

		protected override void When(object @event)
		{
			throw new NotImplementedException();
		}
	}
}
