using System;
using System.Linq;
using EndTermProject.Solution.Models.EFModels;
using EndTermProject.Solution.Models.ViewModels;
using EndTermProject.Solution.Infra;
using EndTermProject.Solution.Repositories.Extensions;
using EndTermProject.Solution.Utilities;
using EndTermProject.Solution.Models.Infra;
using System.Web.Helpers;

namespace EndTermProject.Solution.Repositories
{
	public class MemberRepository
	{
		public void ValidLogin(LoginVm vm)
		{
			var db = new AppDbContext();

			var member = db.Members.FirstOrDefault(m => m.Account == vm.Account);


			if (member == null)
			{
				throw new Exception
					("帳號或密碼錯誤");
			}

			if (member.IsConfirmed == false)
			{
				throw new Exception
					("您尚未開通會員資格, 請先收確認信, 並點選連結開通, 完成認證後即可登入網站");
			}
			// 將vm裡的密碼先雜湊之後,再與db裡的密碼比對
			var salt = HashUtility.GetSalt();
			var hashedPassword = HashUtility.ToSHA256(vm.Password, salt);

			if (string.Compare(member.Password, hashedPassword, true) != 0)
			{
				throw new Exception("帳號或密碼有誤");
			}
		}
		public void RegisterMember(RegisterVm vm)
		{
			var db = new AppDbContext();

			var memberInDb = db.Members.FirstOrDefault(m => m.Account == vm.Account);

			if (memberInDb != null)
			{
				throw new Exception("帳號已經存在");
			}

			var member = vm.ToEFModel();

			db.Members.Add(member);
			db.SaveChanges();


		}

		public bool ActiveMember(int memberId, string confirmCode)
		{
			if (memberId <= 0 || string.IsNullOrEmpty(confirmCode))
			{
				return false;
			}

			var db = new AppDbContext();

			var member = db.Members.FirstOrDefault
				(p => p.Id == memberId && p.ConfirmCode == confirmCode && p.IsConfirmed == false);

			if (member == null)
			{
				return false;
			}

			member.IsConfirmed = true;
			member.ConfirmCode = null;
			member.Enabled = true;
			db.SaveChanges();

			return true;

		}

		public EditProfileVm GetMemberProfile(string account)
		{
			var db = new AppDbContext();

			var member = db.Members.FirstOrDefault(p => p.Account == account);
			if (member == null)
			{
				throw new Exception("帳號不存在");
			}

			var vm = member.ToEditProfileVm();

			return vm;
		}
		public void UpdateProfile(EditProfileVm vm, string account)
		{
			var db = new AppDbContext();

			var memberInDb = db.Members.FirstOrDefault(p => p.Id == vm.Id);
			if (memberInDb.Account.ToLower() != account.ToLower())
			{
				throw new Exception("您沒有權限修改別人的資料");
			}

			memberInDb.Name = vm.Name;
			memberInDb.Email = vm.Email;
			memberInDb.Telephone = vm.Telephone;
			memberInDb.DateofBirth = vm.DateofBirth;

			db.SaveChanges();
		}
		public void ChangePassword(EditPasswordVm vm, string account)
		{
			var db = new AppDbContext();
			var memberInDb = db.Members.FirstOrDefault(p => p.Account == account);
			if (memberInDb == null)
			{
				throw new Exception("帳號不存在");
			}

			var salt = HashUtility.GetSalt();

			// 判斷輸入的原始密碼是否正確
			var hashedOrigPassword = HashUtility.ToSHA256(vm.OriginalPassword, salt);
			if (string.Compare(memberInDb.Password, hashedOrigPassword, true) != 0)
			{
				throw new Exception("原始密碼不正確");
			}

			// 將新密碼雜湊
			var hashedPassword = HashUtility.ToSHA256(vm.Password, salt);

			// 更新記錄
			memberInDb.Password = hashedPassword;
			db.SaveChanges();
		}

		public void ProcessResetPassword(string account, string email, string urlTemplate)
		{
			var db = new AppDbContext();
			// 檢查account,email正確性
			var memberInDb = db.Members.FirstOrDefault(m => m.Account == account);
			if (memberInDb == null)
			{
				throw new Exception("帳號不存在");
			}

			if (string.Compare(email, memberInDb.Email, StringComparison.CurrentCultureIgnoreCase) != 0)
			{
				throw new Exception("帳號或 Email 錯誤");
			}

			// 檢查 IsConfirmed必需是true
			if (memberInDb.IsConfirmed == false)
			{
				throw new Exception("您還沒有啟用本帳號, 請先完成才能重設密碼");
			}

			// 更新記錄, 填入 confirmCode
			var confirmCode = Guid.NewGuid().ToString("N");
			memberInDb.ConfirmCode = confirmCode;
			db.SaveChanges();

			// 發送重設密碼信
			var url = string.Format(urlTemplate, memberInDb.Id, confirmCode);

			new EmailHelper().SendForgetPasswordEmail(url, memberInDb.Name, email);
		}
		public void ProcessResetPassword(int memberId, string confirmCode, ResetPasswordVM vm)
		{
			var db = new AppDbContext();

			// 檢查 memberId, confirmCode 是否正確
			var memberInDb = db.Members.FirstOrDefault(m => m.Id == memberId &&
														m.IsConfirmed == true &&
														m.ConfirmCode == confirmCode);
			if (memberInDb == null) return; // 不做動作回上頁

			// 重設密碼
			var salt = HashUtility.GetSalt();
			var hashedPassword = HashUtility.ToSHA256(vm.Password, salt);
			memberInDb.Password = hashedPassword;
			memberInDb.ConfirmCode = null;
			db.SaveChanges();
		}
		public void ProcessRegisterPassword(string account, string email, string urlTemplate)
		{
			var db = new AppDbContext();
			// 檢查account,email正確性
			var memberInDb = db.Members.FirstOrDefault(m => m.Account == account);

			// 發送註冊信
			var url = string.Format(urlTemplate, memberInDb.Id, memberInDb.ConfirmCode);

			new EmailHelper().SendRegisterEmail(url, memberInDb.Name, email);
		}
	}
}