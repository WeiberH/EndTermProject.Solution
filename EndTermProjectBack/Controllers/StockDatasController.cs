using EndTermProjectBack.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EndTermProjectBack.Controllers
{
	public class StockDatasController : Controller
	{
		// GET: StockDatas

		private readonly StockDataRepository _stockDataRepository;

		public StockDatasController()
		{
			_stockDataRepository = new StockDataRepository(); // 直接實例化，但在真實世界中建議使用DI（依賴注入）
		}

		public ActionResult Index()
		{
			var summaries = _stockDataRepository.GetAll();
			return View(summaries);
		}
	}
}