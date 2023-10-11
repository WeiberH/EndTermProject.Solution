
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EndTermProject.Models.ViewModels
{
	public class OrderVm
	{
		[Display(Name = "訂單編號")]
		public int Id { get; set; }
		public int MemberId { get; set; }
		[Display(Name = "訂單成立時間")]
		public DateTime OrderTime { get; set; }
		[Display(Name = "付款方式")]
		public string Paytype { get; set; }
		[Display(Name ="貨物狀態")]
		public string Status { get; set; }
		public string City { get; set; }
		public string District { get; set; }
		public string Address { get; set; }
		[Display(Name = "收件地址")]
		public string FullAddress => City + District + Address;
		[Display(Name ="收件人")]
		public string Receiver { get; set; }
		[Display(Name ="收件人電話")]
		public string TelePhone { get; set; }
		[Display(Name ="收件人電子郵件")]
		public string Email { get; set; }
        public List<OrderItemVm> OrderItems { get; set; }
		[Display(Name ="總計")]
		[DisplayFormat(DataFormatString = "{0:N0}")]
		public int Total => OrderItems.Sum(oi => oi.SubTotal)+60;

	}
}