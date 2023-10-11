using EndTermProject.Solution.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndTermProject.Solution.Models.ViewModels
{
	public class ProductFiltersVm
	{
		// Index頁顯示
		public Product Product { get; set; }
		public List<Product> Products { get; internal set; }
		public List<string> Brands { get; set; }
		public List<string> SecondaryCategories { get; set; }
		public List<string> Capacities { get; set; }
		public List<string> Notes { get; set; }

		// 首頁選單選擇值
		public int? BrandId { get; set; }
		public int? SecondaryCategoryId { get; set; }

		// 儲存已選擇的首頁選單值
		public int SelectedBrandId { get; internal set; }
		public int SelectedSecondaryCategoryId { get; internal set; }

		// 篩選器 & 價格排序
		public List<string> SelectedBrandIds { get; set; }
		public List<string> SelectedSecondaryCategoryIds { get; set; }
		public List<string> SelectedCapacityIds { get; set; }
		public List<string> SelectedNoteIds { get; set; }
		public decimal? LowPrice { get; set; }
		public decimal? HighPrice { get; set; }
		public string PriceSort { get; set; }

		// 麵包屑名稱
		public string SelectedBrandName { get; set; }
		public string SelectedSecondaryCategoryName { get; set; }

		// 品牌banner
		public string BrandImage { get; set; }
		public string BrandDescription { get; set; }
	}

}