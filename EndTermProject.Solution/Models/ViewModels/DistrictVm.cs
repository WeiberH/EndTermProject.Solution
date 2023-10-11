using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndTermProject.Models.ViewModels
{
	public class DistrictVm
	{
        public int Id { get; set; }
        public int CityId { get; set; }
        public string District { get; set; }
    }
}