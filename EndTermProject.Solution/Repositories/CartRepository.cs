
using EndTermProject.Models.ViewModels;
using EndTermProject.Solution.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndTermProject.Repositories
{
	public class CartRepository
	{
		public CartVm GetOrCreateCart(string account)
		{
			var db = new AppDbContext();

			var cart = db.Carts.FirstOrDefault(c => c.MemberAccount == account);
			if (cart == null)
			{
				cart = new Cart { MemberAccount = account };
				db.Carts.Add(cart);
				db.SaveChanges();

				return new CartVm
				{
					Id = cart.Id,
					MemberAccount = account,
					CartItems = new List<CartItemVm>()
				};
			}

			return new CartVm
			{
				Id = cart.Id,
				MemberAccount = account,
				CartItems = cart.CartItems.ToList().Select(c => new CartItemVm
				{
					Id = c.Id,
					CartId = c.CartId,
					ProductId = c.ProductId,
					Qty = c.Qty,
					Product = new ProductVm
					{
						Id = c.Product.Id,
						Name = c.Product.Name,
						Price = c.Product.Price,
						Image = c.Product.Image,
					}
				}).ToList(),
			};
		}

		public void AddToCart(string buyer, int productId, int qty)
		{
			CartVm cart = GetOrCreateCart(buyer);

			AddCartItem(cart, productId, qty);
		}

		public void AddCartItem(CartVm cart, int productId, int qty)
		{
			var db = new AppDbContext();

			var cartItem = db.CartItems.FirstOrDefault(c => c.CartId == cart.Id && c.ProductId == productId);
			if (cartItem == null)
			{
				var newItem = new CartItem
				{
					CartId = cart.Id,
					ProductId = productId,
					Qty = qty,
				};
				db.CartItems.Add(newItem);
				db.SaveChanges();
				return;
			}

			cartItem.Qty += qty;
			db.SaveChanges();
		}

		public void UpdateItemQty(string buyer, int productId, int qty)
		{
			var cart = GetOrCreateCart(buyer);
			var cartItem = cart.CartItems.FirstOrDefault(c => c.ProductId == productId);
			if (cartItem == null) return;

			var db = new AppDbContext();
			if (qty == 0)
			{
				var item = db.CartItems.Find(cartItem.Id);
				db.CartItems.Remove(item);
				db.SaveChanges();
				return;
			}
			else
			{
				var cartItemInDb = db.CartItems.FirstOrDefault(ci => ci.Id == cartItem.Id);
				cartItemInDb.Qty = qty;
				db.SaveChanges();
			}
		}
	}
}