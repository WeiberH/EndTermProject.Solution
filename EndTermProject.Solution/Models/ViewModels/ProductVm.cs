using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace EndTermProject.Models.ViewModels
{
	public class ProductVm
	{
		public int Id { get; set; }

		[Display(Name = "商品")]
		public string Name { get; set; }

		[Display(Name = "價格")]
		[DisplayFormat(DataFormatString = "{0:#,#}")]
		public int Price { get; set; }
		public string Image { get; set; }


	}
}