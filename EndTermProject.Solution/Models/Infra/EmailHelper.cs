using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace EndTermProject.Solution.Models.Infra
{
	public class EmailHelper
	{
		private string senderEmail = "rajaboris60101@gmail.com"; // 寄件者
		public void SendForgetPasswordEmail(string url, string name, string email)
		{
			var subject = "[重設密碼通知]";
			var body = $@"Hi {name},
<br />
請點擊此連結 [<a href='{url}' target='_blank'>我要重設密碼</a>], 以進行重設密碼, 如果您沒有提出申請, 請忽略本信, 謝謝";

			var from = senderEmail;
			var to = email;

			SendViaGoogle(from, to, subject, body);
		}
		public void SendConfirmRegisterEmail(string url, string name, string email)
		{
			var subject = "[新會員確認信]";
			var body = $@"Hi {name},
<br />
請點擊此連結 [<a href='{url}' target='_blank'>的確是我申請會員</a>], 如果您沒有提出申請, 請忽略本信, 謝謝";

			var from = senderEmail;
			var to = email;

			SendViaGoogle(from, to, subject, body);
		}
		public virtual void SendViaGoogle(string from, string to, string subject, string body)
		{
			// 開發測試用, 只是建立text file, 不真的寄出信
			var path = HttpContext.Current.Server.MapPath("~/files/");
			CreateTextFile(path, from, to, subject, body);
			return;
		}
		private void CreateTextFile(string path, string from, string to, string subject, string body)
		{
			var fileName = $"{to.Replace("@", "_")} {DateTime.Now.ToString("yyyyMMdd_HHmmss")}.txt";

			// 文字檔案的完整路徑
			var fullPath = Path.Combine(path, fileName);

			// 檔案內容
			var contents = $@"from:{from}
to:{to}
subject:{subject}

{body}";

			// 建立文字檔案, 採用UTF8編碼
			File.WriteAllText(fullPath, contents, Encoding.UTF8);
		}
		public void SendRegisterEmail(string url, string name, string email)
		{
			var subject = "[帳號開通通知信]";
			var body = $@"Hi {name},
<br />
請點擊此連結 [<a href='{url}' target='_blank'>來啟動開通帳號</a>], 如果您已經開通, 請忽略本信, 謝謝";

			var from = senderEmail;
			var to = email;

			SendViaGoogle(from, to, subject, body);
		}
	}
}