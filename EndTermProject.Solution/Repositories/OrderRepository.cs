
using EndTermProject.Models.ViewModels;
using EndTermProject.Solution.Models.EFModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndTermProject.Repositories
{
	public class OrderRepository
	{
		public int GetMemberId(string account)
		{
			var db = new AppDbContext();
			var memberId = db.Members.FirstOrDefault(x => x.Account == account);
			if (memberId == null) return -1;

			return memberId.Id;
			
		}
		public List<OrderVm> GetOrder(int memberId)
		{
			var db = new AppDbContext();

			var order = db.Orders.Where(o => o.MemberId == memberId).ToList();

			var orderVmList = order.Select(o => new OrderVm
			{

				Id = o.Id,
				MemberId = memberId,
				OrderTime = o.OrderTime,
				Paytype = o.Paytype.Paytype1,
				Status = o.Status.Status1,
				City = o.District.City.Name,
				District = o.District.District1,
				Address = o.Address,
				Receiver = o.Receiver,
				TelePhone = o.TelePhone,
				Email = o.Email,
				OrderItems = o.OrderItems.Select(od => new OrderItemVm
				{
					Id = od.Id,
					OrderId = od.OrderId,
					ProductId = od.ProductId,
					Qty = od.Qty,
					ProductName = od.ProductName,
					ProductPrice = od.ProductPrice,
					Brand = od.Product.Brand.Name,
					ProductImage = od.Product.Image,
				}).ToList(),
			}).ToList();
			return orderVmList;
		}
		public List<OrderVm> GetOrderItems(int orderId)
		{
			var db = new AppDbContext();
			var orderItems = db.Orders.Where(o => o.Id == orderId).ToList();

			var orderItemVms = new List<OrderItemVm>();

			var orderItemList = orderItems.Select(o => new OrderVm
			{

				Id = orderId,
				MemberId = o.MemberId,
				OrderTime = o.OrderTime,
				Paytype = o.Paytype.Paytype1,
				Status = o.Status.Status1,
				City = o.District.City.Name,
				District = o.District.District1,
				Address = o.Address,
				Receiver = o.Receiver,
				TelePhone = o.TelePhone,
				Email = o.Email,
				OrderItems = o.OrderItems.Select(od => new OrderItemVm
				{
					Id = od.Id,
					OrderId = od.OrderId,
					ProductId = od.ProductId,
					Qty = od.Qty,
					ProductName = od.ProductName,
					ProductPrice = od.ProductPrice,
					Brand = od.Product.Brand.Name,
					ProductImage = od.Product.Image,
				}).ToList(),
			}).ToList();
			return orderItemList;
		}
	}
}