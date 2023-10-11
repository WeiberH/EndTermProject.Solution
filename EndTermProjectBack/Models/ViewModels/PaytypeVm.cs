using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EndTermProjectBack.Models.ViewModels
{
	public class PaytypeVm
	{
		[Display(Name = "編號")]
		public int Id { get; set; }

		[Display(Name ="付款方式")]
		[Required]
		[StringLength(10)]
		public string Name { get; set; }
		[Display(Name = "是否啟用")]
		public bool Enabled { get; set; }
	}
}