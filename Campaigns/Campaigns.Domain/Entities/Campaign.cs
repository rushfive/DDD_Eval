using Campaigns.Domain.ValueObjects;
using Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Campaigns.Domain.Entities
{
	public class Campaign : Entity
	{
		public CampaignId Id { get; private set; }
		public CampaignName Name { get; private set; }
		public CampaignDescription Description { get; private set; }
		public CampaignEnrollmentState EnrollmentState { get; private set; }

		//private readonly ICampaignsRepository _repository;

		public Campaign(CampaignId id, CampaignName name)
		{
			Apply(new Events.CampaignCreated
			{
				Id = id,
				Name = name
			});
		}


		public void SetDescription(CampaignDescription description)
		{
			Apply(new Events.CampaignDescriptionUpdated
			{
				Id = Id,
				Description = description
			});
		}

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
				case Events.CampaignCreated e:
					Id = new CampaignId(e.Id);
					Name = new CampaignName(e.Name);
					EnrollmentState = CampaignEnrollmentState.Default;
					break;
				case Events.CampaignDescriptionUpdated e:
					Description = new CampaignDescription(e.Description);
					break;
				case Events.CampaignEnrollmentSuspended e:
					EnrollmentState = EnrollmentState.SuspendEnrollments();
					break;
			}
		}

		//public async Task Add()
		//{
		//	// invariant for adding: cannot have the same id or name as another existing campaign
		//	if (await _repository.Exists(_name))
		//	{
		//		throw new InvalidEntityStateException(this, $"Campaign with name '{_name}' already exists.", GetSerializableDebugRepresentation());
		//	}

		//	EnsureValidState();
		//}

		protected override void EnsureValidState()
		{
			bool valid = Id != null && Name != null;

			if (!valid)
			{
				throw new InvalidEntityStateException(this, "Post checks failed", GetSerializableDebugRepresentation());
			}
		}

		protected override object GetSerializableDebugRepresentation()
		{
			throw new NotImplementedException();
		}
	}
}
