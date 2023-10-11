using EndTermProjectBack.Models.EFModels;
using EndTermProjectBack.Models.Infra;
using EndTermProjectBack.Models.ViewModels;
using EndTermProjectBack.Repositories;
using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;

namespace EndTermProjectBack.Controllers
{
	public class CarouselController : Controller
	{
		// GET: Carousel
		public ActionResult TextCarousel()
		{
			var repo = new CarouselRepository().GetTextCarousel();

			return View(repo);
		}
		public ActionResult EditTextCarousel(int id)
		{
			var repo = new CarouselRepository().EditTextCarousel(id);

			return View(repo);
		}

		[HttpPost]
		public ActionResult EditTextCarousel(TextCarousel vm)
		{
			var repo = new CarouselRepository();
			repo.UpdateTextCarousel(vm);

			return RedirectToAction("TextCarousel");
		}

		public ActionResult CreateTextCarousel()
		{
			return View();
		}

		[HttpPost]
		public ActionResult CreateTextCarousel(TextCarousel vm)
		{
			var repo = new CarouselRepository();
			repo.CreateTextCarousel(vm);

			return View(vm);
		}

		public ActionResult BannerCarousel()
		{
			var repo = new CarouselRepository().GetBannerCarousel();

			return View(repo);
		}

		public ActionResult EditBannerCarousel(int id)
		{
			var repo = new CarouselRepository().EditBannerCarousel(id);

			return View(repo);
		}
		[HttpPost]
		public ActionResult EditBannerCarousel(BannerCarousel vm, HttpPostedFileBase ImageFile)
		{

			try
			{
				if (ImageFile != null && ImageFile.ContentLength > 0)
				{
					string fileName = Path.GetFileName(ImageFile.FileName);


					string imagePath = Path.Combine(Server.MapPath("~/Images/Carousel/"), fileName);


					ImageFile.SaveAs(imagePath);


					string dest = System.Configuration
						.ConfigurationManager
						.AppSettings["Carousel"];


					string destFullPath = Path.Combine(dest, fileName);


					System.IO.File.Copy(imagePath, destFullPath, true);

					vm.Image = fileName;
				}

				var repo = new CarouselRepository();
				repo.UpdateBannerCarousel(vm);

				return RedirectToAction("BannerCarousel");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, "出現錯誤：" + ex.Message);
				return View(vm);
			}
		}

		public ActionResult CreateBannerCarousel()
		{
			return View();
		}

		[HttpPost]
		public ActionResult CreateBannerCarousel(BannerCarousel vm, HttpPostedFileBase imageFile)
		{
			string fileName = "";
			string path = Server.MapPath("~/Images/Carousel/");

			try
			{
				if (imageFile != null && imageFile.ContentLength > 0)
				{
					fileName = Path.GetFileName(imageFile.FileName);

					string destFullPath = Path.Combine(path, fileName);
					if (System.IO.File.Exists(destFullPath))
					{
						System.IO.File.Delete(destFullPath);
					}

					// 保存新文件
					imageFile.SaveAs(destFullPath);

					// 複製一個到前台
					string sourceFullPath = Path.Combine(path, fileName);

					string dest = System.Configuration
						.ConfigurationManager
						.AppSettings["Carousel"];

					destFullPath = Path.Combine(dest, fileName);

					System.IO.File.Copy(sourceFullPath, destFullPath, true);
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("imageFile", ex.Message);
				return View(vm);
			}

			vm.Image = fileName;

			var repo = new CarouselRepository();
			repo.CreateBannerCarousel(vm);

			return RedirectToAction("BannerCarousel");

		}
	}
}