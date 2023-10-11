using EndTermProjectBack.Models.EFModels;
using EndTermProjectBack.Models.VMModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using EndTermProjectBack.Models;
using System.Reflection;
using EndTermProjectBack.Utilities;
using System.Net;
using Dapper;
using System.Runtime.Remoting.Channels;

namespace EndTermProjectBack.Repositories
{
	public class OrderRepository
	{
		public List<Order> Search(OrderSearchCriteria criteria)
		{
			string receiver = criteria.Receiver;
			string telephone = criteria.Telephone;
			string email = criteria.Email;
			int? paytypeId = criteria.PaytypeId;
			int? statusId = criteria.StatusId;
			DateTime? startDate = criteria.StartDate;
			DateTime? endDate = criteria.EndDate;

			if (startDate.HasValue && endDate.HasValue)
			{
				if(startDate < endDate)
				{
					endDate = endDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
				}
				else if (startDate > endDate)
				{
					DateTime? temp = startDate;
					startDate = endDate;
					endDate = temp;

					endDate = endDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
				}
				else if (startDate == endDate)
				{
					endDate = endDate.Value.AddHours(23).AddMinutes(59).AddSeconds(59);
				}
			}
			else if (startDate.HasValue)
			{
				endDate = startDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
			}
			else if (endDate.HasValue)
			{
				startDate = endDate.Value.Date;
				endDate = endDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
			}


			var db = new AppDbContext();

			IQueryable<Order> query = db.Orders.AsNoTracking().Include(o => o.Member)
								.Include(o => o.District)
								.Include(o => o.Paytype)
								.Include(o => o.Status);

			if (!string.IsNullOrEmpty(criteria.Receiver))
			{
				query = query.Where(o => o.Receiver.Contains(receiver));
			}
			if (!string.IsNullOrEmpty(telephone))
			{
				query = query.Where(o => o.TelePhone.Contains(telephone));
			}
			if (!string.IsNullOrEmpty(email))
			{
				query = query.Where(o => o.Email.Contains(email));
			}
			if (paytypeId.HasValue)
			{
				query = query.Where(o => o.PaytypeId == paytypeId);
			}
			if (statusId.HasValue)
			{
				query = query.Where(o => o.StatusId == statusId);
			}
			if (startDate.HasValue)
			{
				query = query.Where(o => o.OrderTime > startDate);
			}
			if (endDate.HasValue)
			{
				query = query.Where(o => o.OrderTime < endDate);
			}

			return query.ToList();
		}

		public void UpdateStatus(int? id, int statusId)
		{
			var db = new AppDbContext();

			var order = db.Orders.FirstOrDefault(o => o.Id == id);
			if(order.StatusId != statusId)
			{
				order.StatusId = statusId;
				db.SaveChanges();
			}
		}
		public List<OrderVm> GetOrder()
		{
			var db = new AppDbContext();

			var order = db.Orders.AsNoTracking().Include(o => o.Member)
								.Include(o => o.District)
								.Include(o => o.Paytype)
								.Include(o => o.Status)
								.AsEnumerable()
								.Select(o => new OrderVm
								{
									Id = o.Id,
									MemberId = o.MemberId,
									Member = o.Member.Account,
									OrderTime = o.OrderTime,
									Paytype = o.Paytype.Paytype1,
									PaytypeId = o.PaytypeId,
									Status = o.Status.Status1,
									StatusId = o.StatusId,
									City = o.District.City.Name,
									District = o.District.District1,
									Address = o.Address,
									Receiver = o.Receiver,
									TelePhone = o.TelePhone,
									Email = o.Email,
									OrderItems = o.OrderItems.ToList().Select(oi => new OrderItemVm
									{
										Id = oi.Id,
										OrderId = oi.OrderId,
										Qty = oi.Qty,
										ProductName = oi.ProductName,
										ProductPrice = oi.ProductPrice,
									}).ToList()
								}).ToList();
			return order;
		}
		public List<OrderVm> GetSearchResult(OrderSearchCriteria criteria)
		{
			int? statusId = criteria.StatusId;
			int? paytypeId = criteria.PaytypeId;

			List<OrderVm> ordervms = new List<OrderVm>();

			string sql = @"SELECTo.Id,o.OrderTime,o.Receiver,
o.TelePhone,o.Email,m.Account,
p.Paytype,s.Status,d.District,
c.Name,oi.ProductName,oi.ProductPrice,
oi.Qty,oi.Qty * oi.ProductPrice AS Subtotal
FROM Orders AS o
INNER JOIN Members AS m ON m.Id = o.MemberId
INNER JOIN Paytypes AS p ON p.Id = o.PaytypeId
INNER JOIN Status AS s ON s.Id = o.StatusId
INNER JOIN Districts AS d ON d.Id = o.DistrictsId
INNER JOIN Cities AS c ON d.CityId = c.Id
INNER JOIN OrderItems AS oi ON oi.OrderId = o.Id
WHERE
(o.PaytypeId = @PaytypeId OR @PaytypeId IS NULL)
AND (o.StatusId = @StatusId OR @StatusId IS NULL);";

			using (var conn = new SqlDb().GetConnection("AppDbContext"))
			{
				var result = conn.Query<Order, OrderItem, Models.EFModels.Member, Paytype, Status, District, City, Order>(
					sql,
					(o, oi, m, p, s, d, c) =>
					{
						o.Member = m;
						o.Paytype = p;
						o.Status = s;
						o.District = d;
						o.District.City = c;
						o.OrderItems.Add(oi);
						return o;
					}).ToList();

				
				ordervms = result.Select(order => new OrderVm
				{
					Id = order.Id,
					MemberId = order.MemberId,
					Member = order.Member.Account,  
					OrderTime = order.OrderTime,
					Paytype = order.Paytype.Paytype1,
					PaytypeId = order.PaytypeId,
					Status = order.Status.Status1,
					StatusId = order.StatusId,
					City = order.District.City.Name, 
					District = order.District.District1,
					Address = order.Address, 
					Receiver = order.Receiver,
					TelePhone = order.TelePhone,
					Email = order.Email,
					OrderItems = order.OrderItems.Select(oi => new OrderItemVm
					{
						Id = oi.Id,
						OrderId = oi.OrderId,
						Qty = oi.Qty,
						ProductName = oi.ProductName,
						ProductPrice = oi.ProductPrice
					}).ToList(),
				}).ToList();
			}

			return ordervms;
		}


	}
}