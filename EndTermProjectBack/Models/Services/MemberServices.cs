using EndTermProjectBack.Models.Criterias;
using EndTermProjectBack.Models.DTO;
using EndTermProjectBack.Models.EFModels;
using EndTermProjectBack.Models.ViewModels;
using EndTermProjectBack.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndTermProjectBack.Models.Services
{
	public class MemberServices
	{
		public IEnumerable<MemberDTO> MemberSearch(MemberSearchCriteria memberSearchCriteria)
		{
			var members = new MemberRepository().GetMember();

			if (!string.IsNullOrEmpty(memberSearchCriteria.Account))
			{
				members = members.Where(m => m.Account.ToLower().Contains(memberSearchCriteria.Account.ToLower()));
			}

			if (!string.IsNullOrEmpty(memberSearchCriteria.Name))
			{
				members = members.Where(m => m.Name.ToLower().Contains(memberSearchCriteria.Name.ToLower()));
			}

			if (!string.IsNullOrEmpty(memberSearchCriteria.Email))
			{
				members = members.Where(m => m.Email.ToLower().Contains(memberSearchCriteria.Email.ToLower()));
			}

			if (!string.IsNullOrEmpty(memberSearchCriteria.Telephone))
			{
				members = members.Where(m => m.Telephone.Contains(memberSearchCriteria.Telephone));
			}

			if (!string.IsNullOrEmpty(memberSearchCriteria.Gender))
			{
				bool.TryParse(memberSearchCriteria.Gender, out bool genderData);
				members = members.Where(m => m.Gender == genderData);
			}

			if (!string.IsNullOrEmpty(memberSearchCriteria.Enabled))
			{
				bool.TryParse(memberSearchCriteria.Enabled, out bool enabledData);
				members = members.Where(m => m.Enabled == enabledData);
			}

			if (!string.IsNullOrEmpty(memberSearchCriteria.IsConfirmed))
			{
				bool.TryParse(memberSearchCriteria.IsConfirmed, out bool isConfirmedData);
				members = members.Where(m => m.Enabled == isConfirmedData);
			}
			
			return members;

		}
	}
}