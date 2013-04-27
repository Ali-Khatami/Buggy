using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Buggy.Models
{
	/// <summary>
	/// The whole point of the site. A bug is generally something that is wrong with software, but could also be
	/// something that needs to be added to the software.
	/// </summary>
	public class Bug
	{
		/// <summary>
		/// Unique ID for each bug.
		/// </summary>
		public int ID { get; set; }
		/// <summary>
		/// A Name/Title for the bug.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// A detailed descripion explaining.
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// The date the bug was created and inserted into the database.
		/// </summary>
		public DateTime InsertDate { get; set; }
		/// <summary>
		/// The last time the bug was updated.
		/// </summary>
		public DateTime LastUpdatedDate
		{
			get
			{
				DateTime dtLastUpdate = InsertDate;
				
				// grab the last updates time or just use the insert date
				if(Updates != null && Updates.Count > 0)
				{
					dtLastUpdate = Updates[Updates.Count - 1].InsertDate;
				}

				return dtLastUpdate;
			}
		}
		/// <summary>
		/// The user who entered the bug.
		/// </summary>
		public User Creator { get; set; }
		/// <summary>
		/// The user who is assigned to resolve the bug.
		/// </summary>
		public User Resolver { get; set; }
		/// <summary>
		/// The user who is assigned to test the resolution to the bug.
		/// </summary>
		public User Tester { get; set; }
		/// <summary>
		/// The history of updates on the bug. Generally the conversation regarding details/fixes.
		/// </summary>
		public List<Update> Updates { get; set; }
		/// <summary>
		/// The priorty level of the bug.
		/// </summary>
		public PriorityLevel Priority { get; set; }
		/// <summary>
		/// Helps identify if someone is working on the bug, the bug is closed, just created, etc.
		/// </summary>
		public State CurrentState { get; set; }
		/// <summary>
		/// A generic description of the bug type. ie. enhancement, bug, non issue, etc.
		/// </summary>
		public BugType Type { get; set; }

		/// <summary>
		/// A simple update on the bug.
		/// </summary>
		public class Update
		{
			public User Updater { get; set; }
			public string Description { get; set; }
			public DateTime InsertDate = DateTime.Now;
		}

		public enum PriorityLevel
		{
			Critical = 1,
			High = 2,
			Medium = 3,
			Low = 4,
			Meh = 5
		}

		public enum BugType
		{
			Bug,
			New_Requirement,
			Enhancement,
			Non_Issue
		}

		public enum State
		{
			Created,
			In_Progress,
			Resolved,
			Closed,
			On_Hold
		}
	}
}