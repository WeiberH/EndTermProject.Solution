
using EndTermProject.Models.ViewModels;
using EndTermProject.Repositories;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace EndTermProject.Controllers
{
	public class CartController : Controller
	{
		private CartRepository repo = new CartRepository();
		// GET: Cart
		[Authorize]
		public ActionResult CartInfo()
		{
			string account = User.Identity.Name;
			
			CartVm cart = repo.GetOrCreateCart(account);
			return View(cart);
		}
		[Authorize]
		public ActionResult AddItem(int productId, int qty)
		{
			if (!User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Login", "Members");
			}
			string buyer = User.Identity.Name;

			repo.AddToCart(buyer, productId, qty);

			return new EmptyResult();

		}
		[Authorize]
		public ActionResult UpdateItem(int productId, int qty)
		{
			var buyer = User.Identity.Name;


			qty = qty < 0 ? 0 : qty;

			repo.UpdateItemQty(buyer, productId, qty);

			return new EmptyResult();
		}

		[Authorize]
		public ActionResult Checkout()
		{
			var buyer = User.Identity.Name;

			CartVm cart = repo.GetOrCreateCart(buyer);

			var cartrepo = new CheckoutRepository();

			List<PayTypeVm> payTypes = cartrepo.GetPayType();
			ViewBag.Paytype = payTypes;

			List<CityVm> cities = cartrepo.GetCities();
			ViewBag.Cities = cities;


			if (cart.AllowCheckout == false)
			{
				return ViewBag.ErrorMessage = "購物車沒有任何商品，無法結帳";
			}

			return View();
		}
		[Authorize]
		[HttpPost]
		public ActionResult Checkout(CheckoutVm vm)
		{
			if (!ModelState.IsValid)
			{
				return View(vm);
			}

			var account = User.Identity.Name;
			
			CartVm cart = repo.GetOrCreateCart(account);

			if (cart.AllowCheckout == false)
			{
				ModelState.AddModelError("", "購物車是空的, 無法進行結帳");
				return View(vm);
			}

			var checkoutrepo = new CheckoutRepository();
			checkoutrepo.CheckoutProcess(account, cart, vm);
			return RedirectToAction("Index", "Orders");

		}


		public JsonResult GetDistricts(int cityId)
		{
			var repo = new CheckoutRepository();
			// 根据城市ID从数据库或其他数据源获取鄉鎮数据
			List<DistrictVm> districts = repo.GetDistrictsByCityId(cityId);

			// 返回鄉鎮数据
			return Json(districts, JsonRequestBehavior.AllowGet);
		}
		

	}
}