using EndTermProject.Solution.Models.EFModels;
using EndTermProject.Solution.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EndTermProject.Solution.Controllers
{
	public class ProductFiltersController : Controller
	{
		// GET: ProductbyBrand
		public ActionResult BrandIndex()
		{
			var db = new AppDbContext();

			// 從數據庫獲取所有的商品列表
			List<Product> products = db.Products.ToList();

			// 從數據庫獲取所有的篩選器名稱
			List<string> secondarycategories = db.SecondaryCategories.Select(b => b.Name).Distinct().ToList();
			List<string> notes = db.Notes.Select(n => n.Name).Distinct().ToList();
			List<string> capacities = db.Capacities.Select(c => c.Name).Distinct().ToList();

			// 初始化ViewModel用於將數據傳遞給視圖
			var model = new ProductFiltersVm
			{
				Products = products,
				SecondaryCategories = secondarycategories,
				Capacities = capacities,
				Notes = notes,

				// 初始篩選器列表為空，待用戶在前端選擇
				SelectedSecondaryCategoryIds = new List<string>(),
				SelectedCapacityIds = new List<string>(),
				SelectedNoteIds = new List<string>()
			};

			return View(model);
		}

		[HttpGet]
		public ActionResult FilterProductsByBrand(ProductFiltersVm vm, int? brandId)
		{
			var db = new AppDbContext();

			// 初始化要過濾的商品集合為數據庫中的所有商品
			IQueryable<Product> filteredProducts = db.Products;

			// 如果點選了選單上的品牌ID
			if (brandId.HasValue)
			{
				// 查找該ID的品牌
				var brand = db.Brands.FirstOrDefault(b => b.Id == brandId.Value);
				if (brand != null)
				{
					// 將該品牌的名稱賦值給ViewModel
					vm.SelectedBrandName = brand.Name;
					vm.BrandImage = brand.Image;
					vm.BrandDescription = brand.Description;
				}

				// 設置已選擇的品牌ID為提供的ID
				vm.SelectedBrandId = brandId.Value;

				// 根據提供的品牌ID過濾商品集合
				filteredProducts = filteredProducts.Where(p => p.BrandId == brandId.Value);
			}

			// 各種篩選器過濾商品集合
			if (vm.SelectedSecondaryCategoryIds != null && vm.SelectedSecondaryCategoryIds.Any())
			{
				filteredProducts = filteredProducts.Where(s => vm.SelectedSecondaryCategoryIds.Contains(s.SecondaryCategory.Name));
			}

			if (vm.SelectedCapacityIds != null && vm.SelectedCapacityIds.Any())
			{
				filteredProducts = filteredProducts.Where(c => vm.SelectedCapacityIds.Contains(c.Capacity.Name));
			}

			if (vm.SelectedNoteIds != null && vm.SelectedNoteIds.Any())
			{
				filteredProducts = filteredProducts.Where(n => vm.SelectedNoteIds.Contains(n.Note.Name));
			}
			if (vm.LowPrice.HasValue)
			{
				filteredProducts = filteredProducts.Where(p => p.Price >= vm.LowPrice.Value);
			}

			if (vm.HighPrice.HasValue)
			{
				filteredProducts = filteredProducts.Where(p => p.Price <= vm.HighPrice.Value);
			}

			// 價格排序
			if (!string.IsNullOrEmpty(vm.PriceSort))
			{
				if (vm.PriceSort == "asc")
				{
					filteredProducts = filteredProducts.OrderBy(p => p.Price);
				}
				else if (vm.PriceSort == "desc")
				{
					filteredProducts = filteredProducts.OrderByDescending(p => p.Price);
				}
			}

			// 將過濾後的商品集合賦值給ViewModel
			vm.Products = filteredProducts.ToList();

			// 篩選器的值
			vm.SecondaryCategories = db.SecondaryCategories.Select(s => s.Name).Distinct().ToList();
			vm.Brands = db.Brands.Select(b => b.Name).Distinct().ToList();
			vm.Notes = db.Notes.Select(n => n.Name).Distinct().ToList();
			vm.Capacities = db.Capacities.Select(c => c.Name).Distinct().ToList();

			return View("BrandIndex", vm);
		}

		//GET: ProductbySecondaryCategory
		public ActionResult SecondaryCategoryIndex()
		{
			var db = new AppDbContext();

			// 從數據庫獲取所有的商品列表
			List<Product> products = db.Products.ToList();

			// 從數據庫獲取所有的篩選器名稱
			List<string> brands = db.Brands.Select(b => b.Name).Distinct().ToList();
			List<string> notes = db.Notes.Select(n => n.Name).Distinct().ToList();
			List<string> capacities = db.Capacities.Select(c => c.Name).Distinct().ToList();

			// 初始化ViewModel用於將數據傳遞給視圖
			var model = new ProductFiltersVm
			{
				Products = products,
				Brands = brands,
				Capacities = capacities,
				Notes = notes,

				// 初始篩選器列表為空，待用戶在前端選擇
				SelectedBrandIds = new List<string>(),
				SelectedCapacityIds = new List<string>(),
				SelectedNoteIds = new List<string>()
			};

			return View(model);
		}

		[HttpGet]
		public ActionResult FilterProductsBySecondaryCategory(ProductFiltersVm vm, int? SecondaryCategoryId = null)
		{
			var db = new AppDbContext();

			// 初始化要過濾的商品集合為數據庫中的所有商品
			IQueryable<Product> filteredProducts = db.Products;

			// 如果點選了選單上的次要分類ID
			if (SecondaryCategoryId.HasValue)
			{
				// 查找該ID的次要分類
				var SecondaryCategory = db.SecondaryCategories.FirstOrDefault(s => s.Id == SecondaryCategoryId.Value);
				if (SecondaryCategory != null)
				{
					// 將該次要分類的名稱賦值給ViewModel
					vm.SelectedSecondaryCategoryName = SecondaryCategory.Name;
				}

				// 設置已選擇的次要分類ID為提供的ID
				vm.SelectedSecondaryCategoryId = SecondaryCategoryId.Value;

				// 根據提供的次要分類ID過濾商品集合
				filteredProducts = filteredProducts.Where(s => s.SecondaryCategoryId == SecondaryCategoryId.Value);
			}

			// 各種篩選器過濾商品集合
			if (vm.SelectedBrandIds != null && vm.SelectedBrandIds.Any())
			{
				filteredProducts = filteredProducts.Where(b => vm.SelectedBrandIds.Contains(b.Brand.Name));
			}

			if (vm.SelectedCapacityIds != null && vm.SelectedCapacityIds.Any())
			{
				filteredProducts = filteredProducts.Where(c => vm.SelectedCapacityIds.Contains(c.Capacity.Name));
			}

			if (vm.SelectedNoteIds != null && vm.SelectedNoteIds.Any())
			{
				filteredProducts = filteredProducts.Where(n => vm.SelectedNoteIds.Contains(n.Note.Name));
			}
			if (vm.LowPrice.HasValue)
			{
				filteredProducts = filteredProducts.Where(p => p.Price >= vm.LowPrice.Value);
			}

			if (vm.HighPrice.HasValue)
			{
				filteredProducts = filteredProducts.Where(p => p.Price <= vm.HighPrice.Value);
			}

			// 價格排序
			if (!string.IsNullOrEmpty(vm.PriceSort))
			{
				if (vm.PriceSort == "asc")
				{
					filteredProducts = filteredProducts.OrderBy(p => p.Price);
				}
				else if (vm.PriceSort == "desc")
				{
					filteredProducts = filteredProducts.OrderByDescending(p => p.Price);
				}
			}

			// 將過濾後的商品集合賦值給ViewModel
			vm.Products = filteredProducts.ToList();

			// 篩選器的值
			vm.Brands = db.Brands.Select(b => b.Name).Distinct().ToList();
			vm.Notes = db.Notes.Select(n => n.Name).Distinct().ToList();
			vm.Capacities = db.Capacities.Select(c => c.Name).Distinct().ToList();

			return View("SecondaryCategoryIndex", vm);
		}
	}
}