using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework
{
	public abstract class Entity<TId> : IInternalEventHandler
		where TId : Value<TId>
	{
		private readonly Action<object> _rootApplier;

		public TId Id { get; protected set; }

		// applier is the AggregateRoot's Apply method
		// the entity will call this after handling its own events, to ensure
		// the root's validations are run (ie consistency is maintained)
		// and the event is added to the changes list
		protected Entity(Action<object> rootApplier) => _rootApplier = rootApplier;

		protected abstract void When(object @event);

		protected void Apply(object @event)
		{
			When(@event);
			_rootApplier(@event);
		}

		void IInternalEventHandler.Handle(object @event) => When(@event);
	}
}
