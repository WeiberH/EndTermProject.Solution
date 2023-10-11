using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using EndTermProject.Solution.Models.EFModels;
using EndTermProject.Solution.Models.Infra;
using EndTermProject.Solution.Models.ViewModels;
using EndTermProject.Solution.Repositories;

namespace EndTermProject.Solution.Controllers
{
	public class MembersController : Controller
	{
		// GET: Members
		public ActionResult Index()
		{
			return View();
		}
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Register(RegisterVm vm)
		{
			var repo = new MemberRepository();


			if (!ModelState.IsValid)
			{
				return View(vm);
			}

			try
			{
				repo.RegisterMember(vm);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View(vm);
			}

			var urlTemplate = Request.Url.Scheme + "://" +  // 生成 http:.// 或 https://
				  Request.Url.Authority + "/" + // 生成網域名稱或 ip
				  "Members/ActiveRegister?memberid={0}&confirmCode={1}"; // 生成網頁 url;

			repo.ProcessRegisterPassword(vm.Account, vm.Email, urlTemplate);

			return View("RegisterConfirm");

		}

		public ActionResult ActiveRegister(int memberId, string confirmCode)
		{
			var repo = new MemberRepository();
			repo.ActiveMember(memberId, confirmCode);

			return View();
		}

		public ActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Login(LoginVm vm)
		{
			var repo = new MemberRepository();

			if (!ModelState.IsValid)
			{
				return View(vm);
			}

			try
			{
				repo.ValidLogin(vm);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View(vm);
			}

			var processResult = ProcessLogin(vm);

			Response.Cookies.Add(processResult.Cookie);

			return Redirect(processResult.ReturnUrl);
		}
		public ActionResult Logout()
		{
			Session.Abandon();
			FormsAuthentication.SignOut();
			return Redirect("/Members/Login");
		}

		[Authorize]
		public ActionResult EditProfile()
		{

			var repo = new MemberRepository();

			var currentUserAccount = User.Identity.Name;
			var vm = repo.GetMemberProfile(currentUserAccount);

			return View(vm);
		}

		[Authorize]
		[HttpPost]
		public ActionResult EditProfile(EditProfileVm vm)
		{
			var repo = new MemberRepository();

			var currentUserAccount = User.Identity.Name;
			if (!ModelState.IsValid)
			{
				return View(vm);
			}
			try
			{
				repo.UpdateProfile(vm, currentUserAccount);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View(vm);
			}

			return RedirectToAction("Index", "Home");
		}

		[Authorize]
		public ActionResult EditPassword()
		{
			return View();
		}

		[Authorize]
		[HttpPost]
		public ActionResult EditPassword(EditPasswordVm vm)
		{

			var repo = new MemberRepository();

			if (!ModelState.IsValid)
			{
				return View(vm);
			}
			try
			{
				var currentAccount = User.Identity.Name;
				repo.ChangePassword(vm, currentAccount);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View(vm);
			}

			return RedirectToAction("Index", "Home");
		}
		public ActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		public ActionResult ForgetPassword(ForgotPasswordVm vm)
		{
			var repo = new MemberRepository();

			if (!ModelState.IsValid)
			{
				return View(vm);
			}

			var urlTemplate = Request.Url.Scheme + "://" +  // 生成 http:.// 或 https://
							  Request.Url.Authority + "/" + // 生成網域名稱或 ip
							  "Members/ResetPassword?memberid={0}&confirmCode={1}"; // 生成網頁 url

			try
			{
				repo.ProcessResetPassword(vm.Account, vm.Email, urlTemplate);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View(vm);
			}

			return View("ForgetPasswordConfirm");
		}
		public ActionResult ResetPassword(int memberId, string confirmCode)
		{
			return View();
		}

		[HttpPost]
		public ActionResult ResetPassword(int memberId, string confirmCode, ResetPasswordVM vm)
		{
			var repo = new MemberRepository();

			if (!ModelState.IsValid)
			{
				return View(vm);
			}

			try
			{
				repo.ProcessResetPassword(memberId, confirmCode, vm);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View(vm);

			}

			// 顯示重設密碼成功畫面
			return View("ConfirmResetPassword");
		}
		
		private (string ReturnUrl, HttpCookie Cookie) ProcessLogin(LoginVm vm)
		{
			var rememberMe = false;
			var account = vm.Account;
			var roles = string.Empty; // 沒有用到角色權限,所以存入空白

			var ticket =
				new FormsAuthenticationTicket(
					1,
					account,
					DateTime.Now,
					DateTime.Now.AddDays(2),
					rememberMe,
					roles,          // userdata
					"/"
				);


			var value = FormsAuthentication.Encrypt(ticket);

			var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, value);

			var url = FormsAuthentication.GetRedirectUrl(account, true);

			return (url, cookie);
		}
		public ActionResult IsAuthenticated()
		{
			bool isAuthenticated = User.Identity.IsAuthenticated;
			return Json(new { isAuthenticated = isAuthenticated }, JsonRequestBehavior.AllowGet);
		}
	}
}