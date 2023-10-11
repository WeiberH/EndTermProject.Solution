using EndTermProjectBack.Models.EFModels;
using EndTermProjectBack.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EndTermProjectBack.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult IndexApi()
		{

			return View();
		}

	}
}