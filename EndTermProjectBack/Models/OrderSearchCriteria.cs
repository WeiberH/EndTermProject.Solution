using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndTermProjectBack.Models
{
	public class OrderSearchCriteria
	{
		public string Receiver { get; set; }
		public string Telephone { get; set; }
		public string Email { get; set; }
		public int? StatusId { get; set; }
		public int? PaytypeId { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
	}
}