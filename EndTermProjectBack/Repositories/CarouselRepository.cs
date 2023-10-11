using EndTermProjectBack.Models.EFModels;
using EndTermProjectBack.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndTermProjectBack.Repositories
{
	public class CarouselRepository
	{
		public List<TextCarouselVm> GetTextCarousel()
		{
			var db = new AppDbContext();
			var carousel = db.TextCarousels.OrderByDescending(c => c.CreatTime).Select(c => new TextCarouselVm
			{
				Id = c.Id,
				Description = c.Description,
				CreatTime = c.CreatTime,
				Enabled = c.Enabled,
			});

			return carousel.ToList();
		}

		public TextCarousel EditTextCarousel(int id)
		{
			var db = new AppDbContext();

			var carousel = db.TextCarousels.FirstOrDefault(c => c.Id == id);
			if (carousel == null)
			{
				throw new Exception("此帳號不存在");
			}

			return carousel;
		}

		public void UpdateTextCarousel(TextCarousel vm)
		{
			var db = new AppDbContext();
			var carousel = db.TextCarousels.FirstOrDefault(c => c.Id == vm.Id);

			carousel.Description = vm.Description;
			carousel.Enabled = vm.Enabled;

			db.SaveChanges();
		}

		public void CreateTextCarousel(TextCarousel vm)
		{
			var db = new AppDbContext();
			var carousel = new TextCarousel();

			carousel.Description = vm.Description;
			carousel.Enabled = vm.Enabled;
			carousel.CreatTime = DateTime.Now;

			db.TextCarousels.Add(carousel);
			db.SaveChanges();
		}

		public List<BannerCarouselVm> GetBannerCarousel()
		{
			var db = new AppDbContext();
			var carousel = db.BannerCarousels
				.OrderByDescending(b => b.CreatTime)
				.Select(b => new BannerCarouselVm
				{
					Id = b.Id,
					Headline = b.Headline,
					Description = b.Description,
					Image = b.Image,
					CreatTime = b.CreatTime,
					Enabled = b.Enabled,
				});

			return carousel.ToList();
		}
		public BannerCarousel EditBannerCarousel(int id)
		{
			var db = new AppDbContext();

			var carousel = db.BannerCarousels.FirstOrDefault(c => c.Id == id);
			if (carousel == null)
			{
				throw new Exception("此帳號不存在");
			}

			return carousel;
		}
		public void UpdateBannerCarousel(BannerCarousel vm)
		{
			var db = new AppDbContext();
			var carousel = db.BannerCarousels.FirstOrDefault(c => c.Id == vm.Id);

			carousel.Headline = vm.Headline;
			carousel.Description = vm.Description;
			carousel.Enabled = vm.Enabled;
			carousel.Image = vm.Image;

			db.SaveChanges();
		}
		public void CreateBannerCarousel(BannerCarousel vm)
		{
			var db = new AppDbContext();
			var carousel = new BannerCarousel();

			carousel.Headline = vm.Headline;
			carousel.Description = vm.Description;
			carousel.Enabled = vm.Enabled;
			carousel.CreatTime =DateTime.Now;
			carousel.Image = vm.Image;

			db.BannerCarousels.Add(carousel);
			db.SaveChanges();

		}
	}
}