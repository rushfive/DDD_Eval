using Campaigns.Domain;
using Campaigns.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Documents;

namespace WebApp.Api
{
	public class CampaignsRepository : ICampaignsRepository
	{
		private readonly IMongoDatabase _database;

		public CampaignsRepository(IMongoDatabase database)
		{
			_database = database;
		}

		public async Task<bool> Exists(Guid id)
		{
			var count = await Collection()
				.CountDocumentsAsync(d => d.Id == id)
				.ConfigureAwait(false);
			return count > 0;
		}

		public async Task<Campaign> Load(Guid id)
		{
			var filter = Builders<CampaignDocument>.Filter.Eq(c => c.Id, id);
			IFindFluent<CampaignDocument, CampaignDocument> fluentFind = Collection().Find(filter);
			
			CampaignDocument campaign = await fluentFind.SingleOrDefaultAsync();
			if (campaign == null)
			{
				return null;
			}

			return CampaignDocument.ToEntity(campaign);
		}

		public Task Save(Campaign campaign)
		{
			var document = CampaignDocument.ToDocument(campaign);
			return Collection().InsertOneAsync(document);
		}

		private IMongoCollection<CampaignDocument> Collection()
		{
			return _database.GetCollection<CampaignDocument>("Campaigns");
		}
	}
}
