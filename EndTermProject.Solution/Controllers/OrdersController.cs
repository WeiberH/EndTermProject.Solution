using EndTermProject.Models.ViewModels;
using EndTermProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EndTermProject.Controllers
{
    public class OrdersController : Controller
    {
		//GET: Orders
		[Authorize]
		public ActionResult Index()
        {
            string account = User.Identity.Name;


            var repo = new OrderRepository();
            var memberId = repo.GetMemberId(account);
            List<OrderVm> vm = repo.GetOrder(memberId);

            return View(vm);
        }

        [Authorize]
		public ActionResult OrderItem(int orderId)
		{
            string account = User.Identity.Name;

            var repo = new OrderRepository();
			List<OrderVm> vm = repo.GetOrderItems(orderId);

			return View(vm);
		}
    }
}