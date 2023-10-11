
using EndTermProject.Models.ViewModels;
using EndTermProject.Solution.Models.EFModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndTermProject.Repositories
{
	public class CityRepository
	{
		private static List<City> _cities = new List<City>();

		static CityRepository()
		{
			var db = new AppDbContext();

			var result = db.Cities
				   .OrderBy(c => c.Id)
				   .Select(city => new
				   {
					   value = city.Id,
					   text = city.Name,
					   districts = city.Districts
								   .OrderBy(d => d.Id)
								   .Select(district => new
								   {
									   value = district.Id,
									   text = district.District1
								   })
								   .ToList()
				   })
				   .ToList();
		}
		public List<City> GetAll()
		{
			return _cities;
		}
	}
}