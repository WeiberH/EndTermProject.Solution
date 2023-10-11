using EndTermProject.Solution.Models.EFModels;
using EndTermProject.Solution.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EndTermProject.Solution.Controllers
{
	public class FAQsController : Controller
	{
		//GET: FAQs
		public ActionResult FAQFrontIndex(int? categoryId)
		{
			var db = new AppDbContext();

			// 獲取所有次分類的清單
			var categories = db.SecondaryCategories.ToList();

			// 如果沒有傳遞特定的分類ID並且分類列表不為空，將第一個categoryId默認為第一個
			if (!categoryId.HasValue && categories.Any())
			{
				categoryId = categories.First().Id;
			}

			// 根據提供的categoryId獲取相應的FAQs
			var selectedFAQs = GetFAQs(categoryId.Value);

			// 創建FAQPageVm模型並填充其屬性
			var viewModel = new FAQsVm.FAQPageVm
			{
				// 設置已選擇的FAQs
				FAQs = selectedFAQs,

				// 轉換所有分類為FAQCategoryVm列表
				Categories = categories.Select(c => new FAQsVm.FAQCategoryVm
				{
					Id = c.Id,
					Name = c.Name,
				}).ToList()
			};

			// 將當前的categoryId存儲在ViewBag中，供視圖使用
			ViewBag.CurrentCategoryId = categoryId;

			return View(viewModel);
		}

		[HttpGet]
		private List<FAQsVm> GetFAQs(int categoryId)
		{
			using (var db = new AppDbContext())
			{
				var faqs = db.FAQs
					.Where(f => f.SecondaryCategoriesId == categoryId)
					.Select(f => new FAQsVm
					{
						Id = f.Id,
						SecondaryCategoriesId = f.SecondaryCategoriesId,
						Question = f.Question,
						Answer = f.Answer,
					})
					.ToList();

				return faqs;
			}
		}

	}
}