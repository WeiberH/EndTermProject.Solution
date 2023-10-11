
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndTermProject.Models.ViewModels
{
	public class CityVm
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DistrictVm> Districts { get; set; }
    }
}