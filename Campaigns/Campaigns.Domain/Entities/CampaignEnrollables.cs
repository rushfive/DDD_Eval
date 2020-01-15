using Campaigns.Domain.Enumerations;
using Campaigns.Domain.ValueObjects;
using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaigns.Domain.Entities
{
	public class CampaignEnrollables : AggregateRoot<CampaignId>
	{
		public Dictionary<WorkflowId, PublishableEntity<Workflow, WorkflowId>> Workflows { get; }
		public Dictionary<CampaignTaskId, PublishableEntity<CampaignTask, CampaignTaskId>> Tasks { get; }
		public CampaignEnrollmentConfiguration EnrollmentConfiguration { get; private set; } 

		public CampaignEnrollables(CampaignId id)
		{
			Id = id;
			Workflows = new Dictionary<WorkflowId, PublishableEntity<Workflow, WorkflowId>>();
			Tasks = new Dictionary<CampaignTaskId, PublishableEntity<CampaignTask, CampaignTaskId>>();
			EnrollmentConfiguration = CampaignEnrollmentConfiguration.Default;
		}

		public void AddCampaignTask(string name, string description, TaskType taskType) =>
			Apply(new Events.CampaignTaskAdded
			{
				Name = name,
				Description = description,
				Type = taskType
			});

		public void SuspendNewEnrollments()
		{
			Apply(new Events.CampaignEnrollmentSuspended
			{
				Id = Id
			});
		}

		protected override void When(object @event)
		{
			switch (@event)
			{
				case Events.CampaignTaskAdded e:
					var id = CampaignTaskId.GenerateNew();
					var task = new CampaignTask(id, e.Name, e.Description, e.Type);
					Tasks[id] = new PublishableEntity<CampaignTask, CampaignTaskId>(task);
					break;
				case Events.CampaignEnrollmentSuspended _:
					EnrollmentConfiguration = EnrollmentConfiguration.SuspendEnrollments();
					break;
			}
		}

		protected override void EnsureValidState()
		{
			throw new NotImplementedException();
		}
	}
}
