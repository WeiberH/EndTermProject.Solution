using EndTermProjectBack.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;
using System.Linq;
using System.Web;

namespace EndTermProjectBack.Models.ViewModels
{
	public class ProductViewModel
	{
		[DisplayName("品名")]
		[Required(ErrorMessage = "品名 為必填")]
		public string Name { get; set; }

		[DisplayName("品牌")]
		[Required(ErrorMessage = "品牌 為必填")]
		public int BrandId { get; set; }

		[DisplayName("容量")]
		[Required(ErrorMessage = "容量 為必填")]
		public int CapacityId { get; set; }

		[DisplayName("品類")]
		[Required(ErrorMessage = "品類 為必填")]
		public int SecondaryCategoryId { get; set; }

		[DisplayName("香調")]
		[Required(ErrorMessage = "香調 為必填")]
		public int NoteId { get; set; }

		[DisplayName("價格")]
		[Required(ErrorMessage = "價格 為必填")]
		[Range(1, int.MaxValue, ErrorMessage = "價格必須大於0且不能有小數")]
		public int Price { get; set; }

		[DisplayName("商品介紹")]
		public string Description { get; set; }

		[DisplayName("用法用途")]
		public string Method { get; set; }

		[DisplayName("主要成分")]
		public string Ingredient { get; set; }

		public string Image { get; set; }

		[DisplayName("庫存")]
		public int Stock { get; set; }

		[DisplayName("是否啟用")]
		public bool Enabled { get; set; }

		// 用於圖片上傳的屬性
		public IEnumerable<HttpPostedFileBase> UploadedImages { get; set; }

		public IEnumerable<HttpPostedFileBase> ProductImages { get; set; }

		public Brand Brand { get; set; }

		public Capacity Capacity { get; set; }

		public int Id { get; internal set; }
	}
}