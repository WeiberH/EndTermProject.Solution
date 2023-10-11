using EndTermProjectBack.Models.EFModels;
using EndTermProjectBack.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndTermProjectBack.Utilities
{
	public static class MemberExts
	{
		public static EditProfileVm ToEditProfileVm(this Member member)
		{
			return new EditProfileVm
			{
				Id = member.Id,
				Account = member.Account,
				Name = member.Name,
				Email = member.Email,
				Telephone = member.Telephone,
				DateofBirth = member.DateofBirth,
				Gender = member.Gender,
				Enabled = member.Enabled,
				IsConfirmed = member.IsConfirmed,
			};
		}
	}
}