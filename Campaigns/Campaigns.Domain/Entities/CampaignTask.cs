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
		public CampaignTaskBasicInfo BasicInfo { get; private set; }

		// Type specific configurations
		public ActivityTaskConfiguration ActivityTypeConfiguration { get; private set; }

		public CampaignTask(Action<object> rootApplier) : base(rootApplier) { }

		public void UpdateBasicInfo(CampaignTaskBasicInfo newInfo)
			=> Apply(new Events.CampaignTaskBasicInfoUpdated
			{
				CampaignTaskId = Id,
				Name = newInfo.Name,
				Description = newInfo.Description,
				Type = newInfo.Type
			});

		// This 'When' method should never fail
		protected override void When(object @event)
		{
			switch (@event)
			{
				case Events.CampaignTaskAdded e:
					Id = new CampaignTaskId(e.CampaignTaskId);
					BasicInfo = new CampaignTaskBasicInfo(e.Name, e.Description, e.Type);
					break;
				case Events.CampaignTaskBasicInfoUpdated e:
					BasicInfo = new CampaignTaskBasicInfo(e.Name, e.Description, e.Type);
					break;
			}
		}
	}

	public class ActivityTaskConfiguration : Value<ActivityTaskConfiguration>
	{
		public Guid? ActivityId { get; internal set; }
	}

	public class CampaignTaskBasicInfo : Value<CampaignTaskBasicInfo>
	{
		public string Name { get; internal set; }
		public string Description { get; internal set; }
		public TaskType Type { get; internal set; }

		public CampaignTaskBasicInfo(
			string name, 
			string description, 
			TaskType type)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException(nameof(name),
				"Campaign task name must be specified.");

			Name = name;
			Description = description;
			Type = type;
		}

		internal CampaignTaskBasicInfo() { }
	}
}
