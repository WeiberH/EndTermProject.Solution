using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EndTermProject.Models.ViewModels
{
	public class OrderItemVm
	{
		public int Id { get; set; }

		public int OrderId { get; set; }

		public int ProductId { get; set; }

		public int Qty { get; set; }

		public string ProductName { get; set; }

		public int ProductPrice { get; set; }
        public string Brand { get; set; }
		[DisplayFormat(DataFormatString = "{0:N0}")]
		public int SubTotal => ProductPrice * Qty;
		public string ProductImage { get; set; }
	}
}