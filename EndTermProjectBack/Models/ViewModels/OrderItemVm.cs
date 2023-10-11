using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EndTermProjectBack.Models.VMModels
{
	public class OrderItemVm
	{
		public int Id { get; set; }
		[Display(Name = "訂單編號")]
		public int OrderId { get; set; }

		[Display(Name ="數量")]
		public int Qty { get; set; }

		[StringLength(50)]
		[Display(Name ="產品名稱")]
		public string ProductName { get; set; }
		[Display(Name = "單價")]
		public int ProductPrice { get; set; }
		[Display(Name = "小計")]
		public int Subtotal => Qty * ProductPrice;
        //[Display(Name ="香調")]
        //      public string Note { get; set; }
        //[Display(Name ="分類")]
        //      public string SecondaryCategory { get; set; }


    }
}