using EndTermProject.Solution.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EndTermProject.Solution.Models.ViewModels
{
	public class FAQsVm
	{
		public int Id { get; set; }

		public int SecondaryCategoriesId { get; set; }

		[Required]
		[StringLength(3000)]
		public string Question { get; set; }

		[Required]
		[StringLength(3000)]
		public string Answer { get; set; }

		public SecondaryCategory SecondaryCategory { get; set; }

		public class FAQCategoryVm
		{
			public int Id { get; set; }
			public string Name { get; set; }
		}

		public class FAQPageVm
		{
			public List<FAQsVm> FAQs { get; set; }
			public List<FAQCategoryVm> Categories { get; set; }
		}
	}
}