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
	public class Campaign : AggregateRoot<CampaignId>
	{
		public CampaignConfiguration Configuration { get; private set; }
		//public CampaignBasicInfo BasicInfo { get; private set; }
		//public Dictionary<WorkflowId, PublishableEntity<Workflow, WorkflowId>> Workflows { get; }
		//public Dictionary<CampaignTaskId, PublishableEntity<CampaignTask, CampaignTaskId>> CampaignTasks { get; }
		//public CampaignEnrollmentConfiguration EnrollmentConfiguration { get; private set; }
		public List<CampaignTask> DraftCampaignTasks { get; }
		public List<CampaignTask> PublishedCampaignTasks { get; }
		

		//public CampaignEnrollables(CampaignId id)
		//{
		//	Id = id;
		//	Workflows = new Dictionary<WorkflowId, PublishableEntity<Workflow, WorkflowId>>();
		//	CampaignTasks = new Dictionary<CampaignTaskId, PublishableEntity<CampaignTask, CampaignTaskId>>();
		//	EnrollmentConfiguration = CampaignEnrollmentConfiguration.Default;
		//}

		public Campaign(CampaignId id, CampaignBasicInfo basicInfo)
		{
			Id = id;
			DraftCampaignTasks = new List<CampaignTask>();
			PublishedCampaignTasks = new List<CampaignTask>();
			//EnrollmentConfiguration = new CampaignEnrollmentConfiguration();

			Apply(new Events.CampaignCreated
			{
				CampaignId = id,
				Name = basicInfo.Name,
				Description	 = basicInfo.Description
			});


			//Id = id;
			//Workflows = new Dictionary<WorkflowId, PublishableEntity<Workflow, WorkflowId>>();
			//CampaignTasks = new Dictionary<CampaignTaskId, PublishableEntity<CampaignTask, CampaignTaskId>>();
			//EnrollmentConfiguration = CampaignEnrollmentConfiguration.Default;
		}

		public void UpdateBasicInfo(CampaignBasicInfo newInfo)
			=> Apply(new Events.CampaignBasicInfoUpdated
			{
				CampaignId = Id,
				Name = newInfo.Name,
				Description = newInfo.Description
			});




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


		//public void SuspendNewEnrollments()
		//{
		//	Apply(new Events.CampaignEnrollmentSuspended
		//	{
		//		Id = Id
		//	});
		//}

		private CampaignTask FindDraftCampaignTask(CampaignTaskId id)
			=> DraftCampaignTasks.SingleOrDefault(ct => ct.Id == id);
			//=> CampaignTasks.TryGetValue(id, out PublishableEntity<CampaignTask, CampaignTaskId> task)
			//	? task.Draft
			//	: null;

		protected override void When(object @event)
		{
			CampaignTask campaignTask;
			switch (@event)
			{
				case Events.CampaignCreated e:
					Id = new CampaignId(e.CampaignId);
					BasicInfo = new CampaignBasicInfo(e.Name, e.Description);
					break;
				case Events.CampaignBasicInfoUpdated e:
					BasicInfo = new CampaignBasicInfo(e.Name, e.Description);
					break;
				case Events.CampaignTaskAdded e:
					campaignTask = new CampaignTask(Apply);
					ApplyToEntity(campaignTask, e);
					DraftCampaignTasks.Add(campaignTask);
					//CampaignTasks[campaignTask.Id] = new PublishableEntity<CampaignTask, CampaignTaskId>(campaignTask);
					break;
				case Events.CampaignTaskBasicInfoUpdated e:
					campaignTask = FindDraftCampaignTask(new CampaignTaskId(e.CampaignTaskId));
					ApplyToEntity(campaignTask, e);
					break;
				//case Events.CampaignEnrollmentSuspended _:
				//	EnrollmentConfiguration = EnrollmentConfiguration.SuspendEnrollments();
				//	break;
			}
		}

		protected override void EnsureValidState()
		{
			bool valid =
				Id != null
				&& BasicInfo != null
				&& DraftCampaignTasks != null
				&& PublishedCampaignTasks != null
				&& EnrollmentConfiguration != null
				&& AllCampaignTasks.All(t => t.IsValid());

			if (!valid)
			{
				throw new InvalidEntityStateException(this,
					$"Post-checks failed for campaign '{Id}'.");
			}
		}

		private List<CampaignTask> AllCampaignTasks
			=> DraftCampaignTasks.Concat(PublishedCampaignTasks).ToList();
			//=> CampaignTasks
			//	.Select(ct => ct.Value)
			//	.Aggregate(
			//		new List<CampaignTask>(),
			//		(result, publishable) =>
			//		{
			//			if (publishable.Draft != null) result.Add(publishable.Draft);
			//			if (publishable.Published != null) result.Add(publishable.Published);
			//			return result;
			//		})
			//		.ToList();
	}

	public class CampaignBasicInfo : Value<CampaignBasicInfo>
	{
		public string Name { get; internal set; }
		public string Description { get; internal set; }

		public CampaignBasicInfo(string name, string description)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException(nameof(name),
				"Campaign name must be specified.");

			Name = name;
			Description = description;
		}

		internal CampaignBasicInfo() { }
	}
}
