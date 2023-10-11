
using EndTermProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;
using System.Web.Mvc;
using System.Data.Entity;
using System.Collections;
using EndTermProject.Solution.Models.EFModels;

namespace EndTermProject.Repositories
{
	public class CheckoutRepository
	{
		public void CheckoutProcess(string account, CartVm cartvm, CheckoutVm checkoutVm)
		{
			CreateOrder(account, cartvm, checkoutVm);

			EmptyCart(account);
		}
		public void CreateOrder(string account, CartVm cartvm, CheckoutVm checkoutVm)
		{
			var db = new AppDbContext();

			var member = db.Members.FirstOrDefault(m => m.Account == account);

			var order = new Order
			{
				MemberId = member.Id,
				OrderTime = DateTime.Now,
				PaytypeId = checkoutVm.PaytypeId,
				StatusId = 1,
				DistrictsId = checkoutVm.DistrictId,
				Address = checkoutVm.Address,
				Receiver = checkoutVm.Receiver,
				TelePhone = checkoutVm.Cellphone,
				Email = checkoutVm.Email,
			};
			
			foreach(var item in cartvm.CartItems)
			{
				var orderItem = new OrderItem
				{
					OrderId = order.Id,
					ProductId = item.ProductId,
					Qty = item.Qty,
					ProductName = item.Product.Name,
					ProductPrice = item.Product.Price,
				};
				order.OrderItems.Add(orderItem);
			}
			db.Orders.Add(order);
			db.SaveChanges();
		}

		public void EmptyCart(string account)
		{
			var db = new AppDbContext();
			var cart = db.Carts.FirstOrDefault(m => m.MemberAccount == account);
			if (cart == null) return;

			db.Carts.Remove(cart);
			db.SaveChanges();
		}

		public List<DistrictVm> GetDistrictsByCityId(int cityId)
		{
			var db = new AppDbContext();
				// 查询数据库，根据城市ID获取鄉鎮数据
				var districts = db.Districts
					.Where(d => d.CityId == cityId)
					.Select(d => new DistrictVm
					{
						Id = d.Id,
						District = d.District1
					})
					.ToList();

				return districts;
			
		}

		public List<PayTypeVm> GetPayType()
		{
			var db = new AppDbContext();
			var paytype = db.Paytypes.Where(x =>x.Enabled == true).OrderBy(x => x.Id).Select(p => new PayTypeVm
			{
				Id = p.Id,
				PayType = p.Paytype1
			}).ToList();
			return paytype;
		}

		public List<CityVm> GetCities()
		{
			var db = new AppDbContext();

			var cities = db.Cities.AsNoTracking()
									.Include(c => c.Districts)
									.Select(c => new CityVm
									{
										Id = c.Id,
										Name = c.Name,
										Districts = c.Districts.Select(d => new DistrictVm
										{
											Id = d.Id,
											CityId = d.CityId,
											District = d.District1
										}).ToList()
									}).ToList();
			return cities;
		}

		
	}
}