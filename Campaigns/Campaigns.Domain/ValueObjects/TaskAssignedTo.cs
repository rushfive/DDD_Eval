using Campaigns.Domain.Enumerations;
using Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Campaigns.Domain.ValueObjects
{
	public class TaskAssignedTo : Value<TaskAssignedTo>
	{
		public Guid? AssignedToId { get; internal set; }
		public TaskAssignmentType Type { get; internal set; }

		public static TaskAssignedTo Participants() =>
			new TaskAssignedTo(null, TaskAssignmentType.Participant);

		public static TaskAssignedTo User(Guid userId) =>
			new TaskAssignedTo(userId, TaskAssignmentType.User);

		public static TaskAssignedTo UserGroup(Guid userGroupId) =>
			new TaskAssignedTo(userGroupId, TaskAssignmentType.UserGroup);

		private TaskAssignedTo(Guid? assignedToId, TaskAssignmentType type)
		{
			switch (type)
			{
				case TaskAssignmentType.Participant:
					// this is a general reference (not pointing at a specific participant)
					break;
				case TaskAssignmentType.User:
				case TaskAssignmentType.UserGroup:
					if (!assignedToId.HasValue || assignedToId == default)
						throw new ArgumentException($"{type} id must be specified.");
					break;
				default:
					throw new ArgumentException($"Task assignment type '{type}' is invalid.");
			}

			AssignedToId = assignedToId;
			Type = type;
		}

		internal TaskAssignedTo() { }
	}
}
