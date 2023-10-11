using Dapper;
using EndTermProject.Solution.Models.EFModels;
using EndTermProject.Solution.Models.Entities;
using EndTermProject.Solution.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http.Results;


namespace EndTermProject.Solution.Repositories
{
	public class NewsRepository
	{

		public IEnumerable<Models.Entities.News> GetAll(string season)
		{

			string sql = @"SELECT * FROM News";

			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AppDbContext"].ConnectionString))
			{
				if (!string.IsNullOrEmpty(season))
				{
					sql += " WHERE Title = @season";

				}

				sql += " ORDER BY Time DESC";

				var result = conn.Query<Models.Entities.News>(sql, new { season = season });

				return result;
			}

		}
		public NewsVm GetContent(int id)
		{
			var db = new AppDbContext();
			var news = db.News.FirstOrDefault(n => n.Id == id);
			var vm = new NewsVm
			{
				Title = news.Title,
				NewsList = news.Description,
			};
			return vm;
		}
	}
}