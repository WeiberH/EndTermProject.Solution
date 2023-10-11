using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EndTermProjectBack.Models.VMModels
{
	public class OrderVm
	{
		[Display(Name ="編號")]
        public int Id { get; set; }
		[Display(Name ="會員編號")]
        public int MemberId { get; set; }
		[Display(Name ="會員")]
        public string Member { get; set; }
		[Display(Name = "下單時間")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime OrderTime { get; set; }
		[Display(Name = "付款方式")]
		public string Paytype { get; set; }
        public int PaytypeId { get; set; }
        [Display(Name = "貨物狀態")]
		public string Status { get; set; }
        public int StatusId { get; set; }

        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }

        [StringLength(200)]
		[Display(Name = "運送地址")]
		public string FullAddress => City + District + Address;

		[StringLength(50)]
		[Display(Name = "收件人")]
		public string Receiver { get; set; }

		[StringLength(50)]
		[Display(Name = "收件人電話")]
		public string TelePhone { get; set; }

		[StringLength(50)]
		[Display(Name = "收件人電子郵件")]
		public string Email { get; set; }
        public List<OrderItemVm> OrderItems { get; set; }
        public int Total => OrderItems.Sum(oi => oi.Subtotal) +60;

		internal OrderVm Where(Func<object, bool> value)
		{
			throw new NotImplementedException();
		}
	}
}