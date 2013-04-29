using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Buggy.Interfaces
{
	public interface ISortable
	{
		string SortField { get; set; }
		SortDirection SortDirection { get; set; }
	}
	
	public enum SortDirection
	{
		Ascending,
		Descending
	}
}