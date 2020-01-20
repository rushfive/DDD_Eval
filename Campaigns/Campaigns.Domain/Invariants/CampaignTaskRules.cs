using Campaigns.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaigns.Domain.Invariants
{
	public static class CampaignTaskRules
	{
		public static bool IsValid(this CampaignTask task)
		{
			return task.Id != null
				&& task.BasicInfo !=  null
				&& HasValidTypeBasedConfiguration(task);
		}

		private static bool HasValidTypeBasedConfiguration(CampaignTask task)
		{
			switch (task.BasicInfo.Type)
			{
				case Enumerations.TaskType.Custom:
				case Enumerations.TaskType.Email:
					return true;
				case Enumerations.TaskType.Activity:
					return HasValidActivityConfiguration(task);
				case Enumerations.TaskType.Mail:
				case Enumerations.TaskType.Phone:
				case Enumerations.TaskType.Sms:
					return true;
				default:
					throw new ArgumentOutOfRangeException(
						nameof(task.BasicInfo.Type),
						$"'{task.BasicInfo.Type}' is an invalid task type.");
			}
		}

		private static bool HasValidActivityConfiguration(CampaignTask task)
			=> task.ActivityTypeConfiguration != null
				&& task.ActivityTypeConfiguration.ActivityId.HasValue
				&& task.ActivityTypeConfiguration.ActivityId != default;
	}
}
