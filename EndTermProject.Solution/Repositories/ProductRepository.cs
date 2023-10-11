
using EndTermProject.Models.ViewModels;
using EndTermProject.Solution.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndTermProject.Repositories
{
	public class ProductRepository
	{
		public List<ProductVm> GetProducts()
		{
			var db = new AppDbContext();
			var products = db.Products.AsNoTracking()
										.Where(p => p.Enabled == true)
										.AsEnumerable()
										.Select(p => new ProductVm
										{
											Id = p.Id,
											Name = p.Name,
											Price = p.Price,
										})
										.ToList();
			return products;
		}
	}
}