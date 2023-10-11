using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EndTermProjectBack.Models.ViewModels
{
	public class EditProfileVm
	{
		public int Id { get; set; }

		[Display(Name = "帳號")]
		public string Account { get; set; }

		[Display(Name = "姓名")]
		public string Name { get; set; }

		[Display(Name = "電子信箱")]
		public string Email { get; set; }

		[Display(Name = "手機")]
		public string Telephone { get; set; }


		[Display(Name = "出生年月日")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[DataType(DataType.Date)]
		public DateTime DateofBirth { get; set; }


		[Display(Name = "性別")]
		public bool Gender { get; set; }


		[Display(Name = "啟用狀況")]
		public bool Enabled { get; set; }

		[Display(Name = "開通狀況")]
		public bool? IsConfirmed { get; set; }
	}
}