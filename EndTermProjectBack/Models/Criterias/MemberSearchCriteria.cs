using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndTermProjectBack.Models.Criterias
{
	public class MemberSearchCriteria
	{
		public string Account { get; set; }
		public string Telephone { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Gender{ get; set; }
		public string Enabled { get; set; }
		public string IsConfirmed { get; set; }
	}
}