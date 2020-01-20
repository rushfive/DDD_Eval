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
		public string Name { get; set; }
		public string Description { get; set; }
		public CampaignEnrollmentConfigurationDocument EnrollmentConfiguration { get; set; }

		public static Campaign ToEntity(CampaignDocument document)
		{
			throw new NotImplementedException();
			//var campaign = new Campaign(
			//	new CampaignId(document.Id),
			//	new CampaignName(document.Name));

			//if (!string.IsNullOrWhiteSpace(document.Description))
			//	campaign.SetDescription(new CampaignDescription(document.Description));

			//if (document.EnrollmentConfiguration.EnrollmentSuspended)
			//	campaign.EnrollmentConfiguration.SuspendEnrollments();

			//if (document.EnrollmentConfiguration.AutoEnrollNewParticipants)
			//	campaign.EnrollmentConfiguration.StartAutoEnrollingNewParticipants();

			//return campaign;
		}

		public static CampaignDocument ToDocument(Campaign entity)
		{
			throw new NotImplementedException();
			//return new CampaignDocument
			//{
			//	Id = entity.Id,
			//	Name = entity.Name,
			//	Description = entity.Description,
			//	//EnrollmentConfiguration = CampaignEnrollmentConfigurationDocument.ToDocument(entity.EnrollmentConfiguration)
			//};
		}
	}

	public class CampaignEnrollmentConfigurationDocument
	{
		public bool EnrollmentSuspended { get; set; }
		public bool AutoEnrollNewParticipants { get; set; }

		public static CampaignEnrollmentConfiguration ToEntity(CampaignEnrollmentConfigurationDocument document)
		{
			var state = CampaignEnrollmentConfiguration.Default;

			if (document.EnrollmentSuspended)
				state.SuspendEnrollments();

			if (document.AutoEnrollNewParticipants)
				state.StartAutoEnrollingNewParticipants();

			return state;
		}

		public static CampaignEnrollmentConfigurationDocument ToDocument(CampaignEnrollmentConfiguration entity)
		{
			return new CampaignEnrollmentConfigurationDocument
			{
				EnrollmentSuspended = entity.Suspended,
				AutoEnrollNewParticipants = entity.AutoEnroll
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
