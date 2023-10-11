using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

namespace EndTermProject.Solution.Models.Entities
{
	public class News
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
		public DateTime Time { get; set; }
	}
}