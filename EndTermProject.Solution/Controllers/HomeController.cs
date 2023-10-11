using EndTermProject.Solution.Models.ViewModels;
using EndTermProject.Solution.Repositories;
using EndTermProject.Solution.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Web.UI;
using PagedList;
using EndTermProjectBack.Repositories;

namespace EndTermProject.Solution.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var bannerCarousel = new HomeRepository().GetBannerCarousel();
			return View(bannerCarousel);
		}

		public ActionResult ReturnPolicy()
		{

			return View();
		}
		public ActionResult Shipping()
		{
			return View();
		}
		public ActionResult PaymentMethod()
		{
			return View();
		}
		public ActionResult AboutUs()
		{
			return View();
		}

		public ActionResult TermsOfService()
		{
			return View();
		}
		public ActionResult JoinUs()
		{
			return View();
		}
		public ActionResult RegisterFAQ()
		{
			return View();
		}
		public ActionResult News(string season )
		{
			var newsList = new NewsRepository().GetAll(season);

			return View(newsList);
		}
		public ActionResult NewsContent(int id)
		{
			var vm = new NewsRepository().GetContent(id);

			return View(vm);
		}
	}
}