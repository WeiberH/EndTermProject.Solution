using EndTermProject.Solution.Models.EFModels;
using EndTermProject.Solution.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndTermProject.Solution.Utilities
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
			};
		}
	}
}