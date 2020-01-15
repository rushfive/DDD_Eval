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
		public string Name { get; }
		public string Description { get; }
		public TaskType Type { get; }

		public CampaignTask(CampaignTaskId id, string name, 
			string description, TaskType taskType) : base(null)
		{
			Id = id;
			Name = name;
			Description = description;
			Type = taskType;
		}

		protected override void When(object @event)
		{
			throw new NotImplementedException();
		}
	}
}
