using Campaigns.Domain;
using Campaigns.Domain.Entities;
using Campaigns.Domain.ValueObjects;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WebApp.Contracts.Campaigns;

namespace WebApp.Api
{
	// the app service is responsible for translating primitive types
	// to value objects.
	// the incoming messages/requests from the edge are always primitive
	// due to serialization, but the domain interacts with the richer
	// value objects

	public class CampaignsApplicationService : IApplicationService
	{
		private readonly ICampaignsRepository _repository;

		public CampaignsApplicationService(ICampaignsRepository repository)
		{
			_repository = repository;
		}


		public Task Handle(object command)
		{
			return command switch
			{
				V1.Create cmd => 
					HandleCreate(cmd),
				V1.UpdateDescription cmd => 
					HandleUpdate(cmd.Id, 
						c => c.SetDescription(new CampaignDescription(cmd.Description))),
				//V1.SuspendNewEnrollments cmd => 
				//	HandleUpdate(cmd.Id, 
				//		c => c.SuspendNewEnrollments()),
				_ => Task.CompletedTask
			};
		}

		private async Task HandleCreate(V1.Create cmd)
		{
			if (await _repository.Exists(cmd.Id))
			{
				throw new InvalidOperationException($"Campaign with id '{cmd.Id}' already exists.");
			}

			var campaign = new Campaign(
				new CampaignId(cmd.Id),
				new CampaignName(cmd.Name));

			await _repository.Save(campaign);
		}

		private async Task HandleUpdate(Guid campaignId, Action<Campaign> operation)
		{
			var campaign = await _repository.Load(campaignId);
			if (campaign == null)
			{
				throw new InvalidOperationException($"Campaign with id '{campaignId}' cannot be found");
			}

			//campaign.SuspendNewEnrollments();
			await _repository.Save(campaign);
		}
	}
}
