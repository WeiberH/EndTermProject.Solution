using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace EndTermProject.Solution.Models.ViewModels
{
	public class RegisterVm
	{
		public int Id { get; set; }

		[Display(Name = "帳號")]
		[Required]
		[StringLength(30)]
		public string Account { get; set; }

		/// <summary>
		/// 明碼
		/// </summary>
		[Display(Name = "密碼")]
		[Required]
		[StringLength(50)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "確認密碼")]
		[Required]
		[StringLength(50)]
		[Compare(nameof(Password))]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

		[Display(Name = "姓名")]
		[Required]
		[StringLength(30)]
		public string Name { get; set; }

		[Display(Name = "出生年月日")]
		[Required]
		[DataType(DataType.Date)]
		public DateTime DateofBirth { get; set; }


		[Display(Name = "性別")]
		[Required]
		public bool Gender { get; set; }

		[Display(Name = "電子郵件")]
		[Required]
		[StringLength(256)]
		[EmailAddress]
		public string Email { get; set; }

		[Display(Name = "手機")]
		[StringLength(10)]
		public string Telephone { get; set; }

		[Required(ErrorMessage = "必須勾選同意使用者條款")]
		public bool Agree { get; set; }

	}
}