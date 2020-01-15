using System;
using System.Collections.Generic;
using System.Text;

namespace Framework
{
	// Both the RootAggregate and Entity classes will implement this
	// they are both entities themselves, so will need to be able to handle
	// events themselves (thatre specific to themself)
	// also, the root aggregate might be interested in events happening from the 
	// entities it encapsulates, so we can use this to bubble up events to the root
	public interface IInternalEventHandler
	{
		void Handle(object @event);
	}
}
