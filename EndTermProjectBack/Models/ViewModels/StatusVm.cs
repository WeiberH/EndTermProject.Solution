using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EndTermProjectBack.Models.ViewModels
{
	public class StatusVm
	{
		[Display(Name = "編號")]
		public int Id { get; set; }

		[Display(Name ="貨物狀態")]
		[Required]
		[StringLength(10)]
		public string Name { get; set; }
	}
}