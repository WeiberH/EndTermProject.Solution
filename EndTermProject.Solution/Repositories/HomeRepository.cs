using EndTermProject.Solution.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndTermProjectBack.Repositories
{
	public class HomeRepository
	{
		public List<BannerCarousel> GetBannerCarousel()
		{
			var db = new AppDbContext();
			var bannerCarousel = db.BannerCarousels;

			return bannerCarousel.ToList();
		}
	}
}