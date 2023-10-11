
using EndTermProject.Solution.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EndTermProject.Models.ViewModels
{
	public class CheckoutVm
	{
        [Display(Name ="收件人")]
        [Required]
        [MaxLength(30)]
        public string Receiver { get; set; }
		[Display(Name = "電話")]
		[Required]
		[MaxLength(10)]
		public string Cellphone { get; set; }
		[Display(Name = "電子信箱")]
		[Required]
		[MaxLength(100)]
		[EmailAddress]
		public string Email { get; set; }
		[Display(Name ="城市")]
		[Required]
        public int CityId { get; set; }
		[Display(Name ="鄉鎮")]
		[Required]
        public int DistrictId { get; set; }
		[Display(Name ="巷弄街道")]
		[Required]
		[MaxLength(200)]
        public string Address { get; set; }
		[Display(Name = "付款方式")]
		[Required]
		public int PaytypeId { get; set; }
		public List<Paytype> Paytype { get; set; }

    }
}