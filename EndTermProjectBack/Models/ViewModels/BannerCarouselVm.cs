using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EndTermProjectBack.Models.ViewModels
{
	public class BannerCarouselVm
	{
		public int Id { get; set; }

		[Display(Name ="標題")]
		public string Headline { get; set; }

		[Display(Name = "內容")]
		public string Description { get; set; }

		[Display(Name = "建立時間")]
		public DateTime CreatTime { get; set; }

		[Display(Name = "圖片")]
		public string Image { get; set; }

		[Display(Name = "啟用狀態")]
		public bool Enabled { get; set; }
	}
}