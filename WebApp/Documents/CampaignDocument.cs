using Campaigns.Domain.Entities;
using Campaigns.Domain.ValueObjects;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Documents
{
	public class CampaignDocument
	{
		[BsonId]
		public Guid Id { get; set; }
		public string Name{ get; set; }
		public string Description{ get; set; }
		public CampaignEnrollmentStateDocument EnrollmentState { get; set; }

		public static Campaign ToEntity(CampaignDocument document)
		{
			var campaign = new Campaign(
				new CampaignId(document.Id),
				new CampaignName(document.Name));

			if (!string.IsNullOrWhiteSpace(document.Description))
				campaign.SetDescription(new CampaignDescription(document.Description));

			if (document.EnrollmentState.EnrollmentSuspended)
				campaign.EnrollmentState.SuspendEnrollments();

			if (document.EnrollmentState.AutoEnrollNewParticipants)
				campaign.EnrollmentState.StartAutoEnrollingNewParticipants();

			return campaign;
		}

		public static CampaignDocument ToDocument(Campaign entity)
		{
			return new CampaignDocument
			{
				Id = entity.Id,
				Name = entity.Name,
				Description = entity.Description,
				EnrollmentState = CampaignEnrollmentStateDocument.ToDocument(entity.EnrollmentState)
			};
		}
	}

	public class CampaignEnrollmentStateDocument
	{
		public bool EnrollmentSuspended { get; set; }
		public bool AutoEnrollNewParticipants { get; set; }

		public static CampaignEnrollmentState ToEntity(CampaignEnrollmentStateDocument document)
		{
			var state = CampaignEnrollmentState.Default;

			if (document.EnrollmentSuspended)
				state.SuspendEnrollments();

			if (document.AutoEnrollNewParticipants)
				state.StartAutoEnrollingNewParticipants();

			return state;
		}

		public static CampaignEnrollmentStateDocument ToDocument(CampaignEnrollmentState entity)
		{
			return new CampaignEnrollmentStateDocument
			{
				EnrollmentSuspended = entity.EnrollmentSuspended,
				AutoEnrollNewParticipants = entity.AutoEnrollNewParticipants
			};
		}
	}

	//public class CampaignSerializer : SerializerBase<Campaign>, IBsonDocumentSerializer
	//{
	//	public bool TryGetMemberSerializationInfo(string memberName, out BsonSerializationInfo serializationInfo)
	//	{
	//		switch (memberName)
	//		{
	//			case "Id":
	//				serializationInfo = new BsonSerializationInfo("_id", )
	//				return true;
	//		}

	//		throw new NotImplementedException();
	//	}
	//}
}
