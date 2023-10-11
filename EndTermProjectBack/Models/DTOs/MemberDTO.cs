using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace EndTermProjectBack.Models.DTO
{
	public class MemberDTO
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
		public DateTime DateofBirth { get; set; }

		[Display(Name = "性別")]
		public bool Gender { get; set; }


		[Display(Name = "帳號狀態")]
		public bool Enabled { get; set; }

		[Display(Name = "開通狀態")]
		public bool? IsConfirmed { get; set; }
	}
}