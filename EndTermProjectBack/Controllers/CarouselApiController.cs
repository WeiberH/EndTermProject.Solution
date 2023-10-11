using EndTermProjectBack.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace EndTermProjectBack.Controllers
{
    public class CarouselApiController : ApiController
    {
		[System.Web.Http.HttpGet]
		public IHttpActionResult Index()
		{
			var bannerCarousels = new AppDbContext().BannerCarousels;

			
			return Ok(bannerCarousels);
		}
	}
}
