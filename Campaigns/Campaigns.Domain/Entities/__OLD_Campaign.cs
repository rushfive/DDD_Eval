//using Campaigns.Domain.ValueObjects;
//using Framework;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace Campaigns.Domain.Entities
//{

//	// consider: splitting into 2 entities
//	// 1 - contains id name desc enrollment config
//	// 2 - "CampaignEnrollables"
//	//		- contains workflows and campaign tasks
//	public class Campaign : AggregateRoot<CampaignId>
//	{
//		public CampaignId Id { get; private set; }
//		public CampaignName Name { get; private set; }
//		public CampaignDescription Description { get; private set; }
//		//public CampaignEnrollmentConfiguration EnrollmentConfiguration { get; private set; } = CampaignEnrollmentConfiguration.Default;

//		//private readonly ICampaignsRepository _repository;

//		public Campaign(CampaignId id, CampaignName name)
//		{
//			Apply(new Events.CampaignCreated
//			{
//				CampaignId = id,
//				Name = name
//			});
//		}


//		public void SetDescription(CampaignDescription description)
//		{
//			Apply(new Events.CampaignDescriptionUpdated
//			{
//				Id = Id,
//				Description = description
//			});
//		}

		

//		protected override void When(object @event)
//		{
//			switch (@event)
//			{
//				case Events.CampaignCreated e:
//					Id = new CampaignId(e.CampaignId);
//					Name = new CampaignName(e.Name);
//					break;
//				case Events.CampaignDescriptionUpdated e:
//					Description = new CampaignDescription(e.Description);
//					break;
//			}
//		}

//		//public async Task Add()
//		//{
//		//	// invariant for adding: cannot have the same id or name as another existing campaign
//		//	if (await _repository.Exists(_name))
//		//	{
//		//		throw new InvalidEntityStateException(this, $"Campaign with name '{_name}' already exists.", GetSerializableDebugRepresentation());
//		//	}

//		//	EnsureValidState();
//		//}

//		protected override void EnsureValidState()
//		{
//			bool valid = Id != null && Name != null;

//			if (!valid)
//			{
//				throw new InvalidEntityStateException(this, "Post checks failed");
//			}
//		}
//	}
//}
