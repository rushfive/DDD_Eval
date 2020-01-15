using Campaigns.Domain.ValueObjects;
using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaigns.Domain.Entities
{
	public class StepTask : Entity<StepTaskId>
	{
		public StepTask() : base(null)
		{

		}


		protected override void When(object @event)
		{
			throw new NotImplementedException();
		}
	}
}
