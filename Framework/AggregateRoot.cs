using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework
{
	public abstract class AggregateRoot<TId>
		where TId : Value<TId>
	{
		public TId Id { get; protected set; }
		
		protected abstract void When(object @event);
		
		private readonly List<object> _changes;
		
		protected AggregateRoot() => _changes = new List<object>();
		
		protected void Apply(object @event)
		{
			When(@event);
			EnsureValidState();
			_changes.Add(@event);
		}

		public IEnumerable<object> GetChanges() => _changes.AsEnumerable();

		public void ClearChanges() => _changes.Clear();

		// this method checks the validity of the entire entity's state
		// - called for every operation, to ensure the entity's invariants are protected
		// - only needs to exist in Aggregate roots b/c the root is what maintains the
		//   correctness of the whole aggregates (and not just itself)
		protected abstract void EnsureValidState();
	}
}
