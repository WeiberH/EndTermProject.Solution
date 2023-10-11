using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndTermProjectBack.Models.Entities
{
	public class SummaryStock
	{
		public string BrandName { get; set; }
		public string SecondaryCategoryName { get; set; }
		public int ProductCount { get; set; }
		public int TotalStock { get; set; }
		public int TotalStockValue { get; set; }
	}
}