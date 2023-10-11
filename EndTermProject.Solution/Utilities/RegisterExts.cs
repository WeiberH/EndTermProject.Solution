using System;
using EndTermProject.Solution.Models.EFModels;
using EndTermProject.Solution.Infra;
using EndTermProject.Solution.Models.ViewModels;

namespace EndTermProject.Solution.Repositories.Extensions
{
	public static class RegisterExts
	{
		public static Member ToEFModel(this RegisterVm vm)
		{
			var salt = HashUtility.GetSalt();
			var hashPassword = HashUtility.ToSHA256(vm.Password, salt);
			var confirmCode = Guid.NewGuid().ToString("N");

			return new Member
			{
				Id =vm.Id,
				Account =vm.Account,
				Password = hashPassword,
				Name =vm.Name,
				Email =vm.Email,
				Telephone =vm.Telephone,
				DateofBirth =vm.DateofBirth,
				Gender =vm.Gender,
				IsConfirmed =false,
				ConfirmCode = confirmCode
			};
		}
	}
}