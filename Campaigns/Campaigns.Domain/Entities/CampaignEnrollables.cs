using Campaigns.Domain.Enumerations;
using Campaigns.Domain.Invariants;
using Campaigns.Domain.ValueObjects;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Campaigns.Domain.Entities
{
	public class CampaignEnrollables : AggregateRoot<CampaignId>
	{
		public Dictionary<WorkflowId, PublishableEntity<Workflow, WorkflowId>> Workflows { get; }
		public Dictionary<CampaignTaskId, PublishableEntity<CampaignTask, CampaignTaskId>> CampaignTasks { get; }
		public CampaignEnrollmentConfiguration EnrollmentConfiguration { get; private set; }

		public CampaignEnrollables(CampaignId id)
		{
			Id = id;
			Workflows = new Dictionary<WorkflowId, PublishableEntity<Workflow, WorkflowId>>();
			CampaignTasks = new Dictionary<CampaignTaskId, PublishableEntity<CampaignTask, CampaignTaskId>>();
			EnrollmentConfiguration = CampaignEnrollmentConfiguration.Default;
		}

		public void AddCampaignTask(CampaignTaskBasicInfo basicInfo) =>
			Apply(new Events.CampaignTaskAdded
			{
				CampaignTaskId = CampaignTaskId.GenerateNew(),
				Name = basicInfo.Name,
				Description = basicInfo.Description,
				Type = basicInfo.Type
			});

		public void UpdateCampaignTaskBasicInfo(CampaignTaskId campaignTaskId, CampaignTaskBasicInfo newInfo)
		{
			var task = FindDraftCampaignTask(campaignTaskId);
			if (task == null)
				throw new InvalidOperationException($"Campaign task '{campaignTaskId}' doesn't exist for campaign '{Id}'.");

			task.UpdateBasicInfo(newInfo);
		}


		public void SuspendNewEnrollments()
		{
			Apply(new Events.CampaignEnrollmentSuspended
			{
				Id = Id
			});
		}

		private CampaignTask FindDraftCampaignTask(CampaignTaskId id)
			=> CampaignTasks.TryGetValue(id, out PublishableEntity<CampaignTask, CampaignTaskId> task)
				? task.Draft
				: null;

		protected override void When(object @event)
		{
			CampaignTask campaignTask;
			switch (@event)
			{
				case Events.CampaignTaskAdded e:
					campaignTask = new CampaignTask(Apply);
					ApplyToEntity(campaignTask, e);
					CampaignTasks[campaignTask.Id] = new PublishableEntity<CampaignTask, CampaignTaskId>(campaignTask);
					break;
				case Events.CampaignTaskBasicInfoUpdated e:
					campaignTask = FindDraftCampaignTask(new CampaignTaskId(e.CampaignTaskId));
					ApplyToEntity(campaignTask, e);
					break;
				case Events.CampaignEnrollmentSuspended _:
					EnrollmentConfiguration = EnrollmentConfiguration.SuspendEnrollments();
					break;
			}
		}

		protected override void EnsureValidState()
		{
			bool valid =
				Id != null
				&& Workflows != null
				&& CampaignTasks != null
				&& EnrollmentConfiguration != null
				&& AllCampaignTasks.All(t => t.IsValid());

			if (!valid)
			{
				throw new InvalidEntityStateException(this,
					$"Campaign enrollables for '{Id}' failed to pass post-checks.");
			}
		}

		private List<CampaignTask> AllCampaignTasks
			=> CampaignTasks
				.Select(ct => ct.Value)
				.Aggregate(
					new List<CampaignTask>(),
					(result, publishable) =>
					{
						if (publishable.Draft != null) result.Add(publishable.Draft);
						if (publishable.Published != null) result.Add(publishable.Published);
						return result;
					})
					.ToList();
	}
}
