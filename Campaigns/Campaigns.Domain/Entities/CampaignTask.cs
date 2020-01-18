using Campaigns.Domain.Enumerations;
using Campaigns.Domain.ValueObjects;
using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaigns.Domain.Entities
{
	public class CampaignTask : Entity<CampaignTaskId>
	{
		public string Name { get; private set; }
		public string Description { get; private set; }
		public TaskType Type { get; private set; }

		public CampaignTask(Action<object> rootApplier) : base(rootApplier) { }

		// This 'When' method should never fail
		protected override void When(object @event)
		{
			switch (@event)
			{
				case Events.CampaignTaskAdded e:
					Id = e.CampaignTaskId;
					Name = e.Name;
					Description = e.Description;
					Type = e.Type;
					break;
			}
		}
	}
}
