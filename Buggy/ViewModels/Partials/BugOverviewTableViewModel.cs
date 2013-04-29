using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Buggy.Interfaces;
using Buggy.Models;

namespace Buggy.ViewModels.Partials
{
	public class BugOverviewTableViewModel
	{
		private Bug.FilterableFields _FilterField;
		private string _FilterValue;

		private string _SortField;
		private SortDirection _SortDirection;

		private SiteDB db = new SiteDB();

		public List<Bug> Bugs
		{
			get
			{
				List<Bug> lBugs = new List<Bug>();

				if (!string.IsNullOrEmpty(this.FilterValue))
				{
					lBugs = this._DoFilter(lBugs);
				}

				if (!string.IsNullOrEmpty(this.SortField))
				{
					lBugs = this._DoSort(lBugs);
				}

				return lBugs;
			}
		}

		public Bug.FilterableFields FilterField
		{
			get
			{
				return this._FilterField;
			}
			set
			{
				this._FilterField = value;
			}
		}

		public string FilterValue
		{
			get
			{
				return this._FilterValue;
			}
			set
			{
				this._FilterValue = value;
			}
		}

		public string SortField
		{
			get
			{
				return this._SortField ?? "LastUpdatedDate";
			}
			set
			{
				this._SortField = value;
			}
		}

		public SortDirection SortDirection
		{
			get
			{
				return this._SortDirection;
			}
			set
			{
				this._SortDirection = value;
			}
		}

		private List<Bug> _DoFilter(List<Bug> lBugs)
		{
			List<Bug> lFilteredBugs = new List<Bug>();

			switch (this.FilterField)
			{
				case Bug.FilterableFields.Name:
					lFilteredBugs = db.Bugs.Where(bug => bug.Name.Contains(this.FilterValue)).ToList();
					break;
				case Bug.FilterableFields.Description:
					lFilteredBugs = db.Bugs.Where(bug => bug.Description.Contains(this.FilterValue)).ToList();
					break;
				case Bug.FilterableFields.Resolver:
					int iResolverID = int.Parse(this.FilterValue);
					lFilteredBugs = db.Bugs.Where(bug => bug.ResolverID == iResolverID).ToList();
					break;
				case Bug.FilterableFields.Creator:
					int iCreatorID = int.Parse(this.FilterValue);
					lFilteredBugs = db.Bugs.Where(bug => bug.CreatorID == iCreatorID).ToList();
					break;
				case Bug.FilterableFields.Tester:
					int iTesterID = int.Parse(this.FilterValue);
					lFilteredBugs = db.Bugs.Where(bug => bug.TesterID == iTesterID).ToList();
					break;
				case Bug.FilterableFields.Type:
					lFilteredBugs = db.Bugs.Where(bug => bug.Type == (Bug.BugType)Enum.Parse(typeof(Bug.BugType), this.FilterValue)).ToList();
					break;
				case Bug.FilterableFields.Priority:
					lFilteredBugs = db.Bugs.Where(bug => bug.Priority == (Bug.PriorityLevel)Enum.Parse(typeof(Bug.PriorityLevel), this.FilterValue)).ToList();
					break;
				case Bug.FilterableFields.State:
					lFilteredBugs = db.Bugs.Where(bug => bug.CurrentState == (Bug.State)Enum.Parse(typeof(Bug.State), this.FilterValue)).ToList();
					break;
			}

			return lFilteredBugs;
		}

		private List<Bug> _DoSort(List<Bug> lBugs)
		{
			List<Bug> lSortedBugs = new List<Bug>();

			if (this.SortDirection == Interfaces.SortDirection.Ascending)
			{
				lSortedBugs = lBugs.OrderBy(x => x.GetType().GetProperty(this.SortField).GetValue(x, null)).ToList();
			}
			else
			{
				lSortedBugs = lBugs.OrderByDescending(x => x.GetType().GetProperty(this.SortField).GetValue(x, null)).ToList();
			}

			return lSortedBugs;
		}
	}
}