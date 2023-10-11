using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace EndTermProject.Solution.Models.ViewModels
{
	public class EditProfileVm
	{
		public int Id { get; set; }

		[Display(Name = "帳號")]
		[Required]
		public string Account { get; set; }

		[Display(Name = "電子郵件")]
		[Required]
		[StringLength(256)]
		[EmailAddress]
		public string Email { get; set; }

		[Display(Name = "姓名")]
		[Required]
		[StringLength(30)]
		public string Name { get; set; }

		[Display(Name = "手機")]
		[StringLength(10)]
		public string Telephone { get; set; }

		
		[Display(Name = "出生年月日")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[DataType(DataType.Date)]
		public DateTime DateofBirth { get; set; }

	}
}