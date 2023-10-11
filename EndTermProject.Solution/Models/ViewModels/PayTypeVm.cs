using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EndTermProject.Models.ViewModels
{
	public class PayTypeVm
	{
        public int Id { get; set; }
        [Display(Name ="付款方式")]
        public string PayType { get; set; }
    }
}