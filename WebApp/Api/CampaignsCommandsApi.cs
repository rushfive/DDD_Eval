using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Api
{
	[Route("/campaign")]
	public class CampaignsCommandsApi : Controller
	{
		private readonly CampaignsApplicationService _applicationService;

		public CampaignsCommandsApi(CampaignsApplicationService applicationService)
		{
			_applicationService = applicationService;
		}

		[HttpPost]
		public async Task<IActionResult> Post(Contracts.Campaigns.V1.Create request)
		{
			await _applicationService.Handle(request);
			return Ok();
		}

		[HttpPut("description")]
		public async Task<IActionResult> Put(Contracts.Campaigns.V1.UpdateDescription request)
		{
			await _applicationService.Handle(request);
			return Ok();
		}

		[HttpPut("suspendEnrollments")]
		public async Task<IActionResult> Put(Contracts.Campaigns.V1.SuspendNewEnrollments request)
		{
			await _applicationService.Handle(request);
			return Ok();
		}




	}
}
