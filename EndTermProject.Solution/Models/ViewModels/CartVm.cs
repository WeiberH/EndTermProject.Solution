
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndTermProject.Models.ViewModels
{
	public class CartVm
	{
		public int Id { get; set; }
		public string MemberAccount { get; set; }
		public List<CartItemVm> CartItems { get; set; }
		public int Total => CartItems.Sum(x => x.SubTotal);
        public bool AllowCheckout => CartItems.Any();
	}
}