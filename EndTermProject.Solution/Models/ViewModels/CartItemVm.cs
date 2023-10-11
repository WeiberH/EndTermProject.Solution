
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndTermProject.Models.ViewModels
{
	public class CartItemVm
	{
		public int Id { get; set; }

		public int CartId { get; set; }

		public int ProductId { get; set; }
		public int Qty { get; set; }

		public ProductVm Product { get; set; }
		public int SubTotal => Product.Price * Qty;
	}
}