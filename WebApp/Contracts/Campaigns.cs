using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Contracts
{
	// contracts are DTOs/POCOs
	// these particular ones represent commands
	// and this HTTP API is part of the outer "edge layer" in the onion architecture
	// the app can have multiple ways of interacting w/ the outside world
	// so if we add messaging as another adapter to our app layer in the future,
	// it would use these same contracts
	public static class Campaigns
	{
		public static class V1
		{
			public class Create
			{
				public Guid Id { get; set; }
				public string Name { get; set; }
			}

			public class UpdateDescription
			{
				public Guid Id { get; set; }
				public string Description { get; set; }
			}

			public class SuspendNewEnrollments
			{
				public Guid Id { get; set; }
			}
		}
	}
}
