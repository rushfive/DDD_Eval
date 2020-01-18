using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaigns.Domain.Entities
{
	// todo: should be aggregate root
	public class ParticipantCampaign : Entity<ParticipantCampaignId>
	{
		public ParticipantCampaign(Action<object> rootApplier) : base(rootApplier) { }

		protected override void When(object @event)
		{
			throw new NotImplementedException();
		}
	}

	public class ParticipantCampaignId : Value<ParticipantCampaignId>
	{

	}
}
