using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EndTermProjectBack.Models.ViewModels
{
	public class OrderStatusVm
	{
        [Display(Name ="訂單編號")]
        public int Id { get; set; }
		[Display(Name = "收件人")]
		public string Receiver { get; set; }
		[Display(Name = "收件人電話")]
		public string Telephone { get; set; }
		[Display(Name = "收件人電子郵件")]
		public string Email { get; set; }
		[Display(Name = "收件人地址")]
		public string Address { get; set; }
		[Display(Name = "下單時間")]
		public DateTime OrderTime { get; set; }
		[Display(Name = "付款方式")]
		public string Paytype { get; set; }
		[Display(Name = "貨物狀態")]
		public string Status { get; set; }
    }
}