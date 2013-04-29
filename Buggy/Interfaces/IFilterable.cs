using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Buggy.Interfaces
{
	public interface IFilterable
	{
		string FilterField { get; set; }
		string FilterValue { get; set; }
	}
}