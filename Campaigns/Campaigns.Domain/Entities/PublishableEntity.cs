using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaigns.Domain.Entities
{
	public class PublishableEntity<TEntity, TId>
		where TEntity : Entity<TId>
		where TId : Value<TId>
	{
		public TEntity Draft { get; internal set; }
		public TEntity Published { get; internal set; }
		public int Order { get; internal set; }

		internal PublishableEntity(TEntity entityDraft)
		{
			if (entityDraft == null)
				throw new ArgumentNullException(nameof(entityDraft),
					"Draft version of entity must be provided.");

			Draft = entityDraft;
		}

		

	}
}
