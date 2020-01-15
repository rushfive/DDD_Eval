using Campaigns.Domain.ValueObjects;
using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaigns.Domain.Entities
{
	// should be aggregate root, containing entities like steps and tasks
	public class Workflow : Entity<WorkflowId>
	{
		public Workflow() : base(null)
		{

		}
		protected override void When(object @event)
		{
			throw new NotImplementedException();
		}
	}
}
