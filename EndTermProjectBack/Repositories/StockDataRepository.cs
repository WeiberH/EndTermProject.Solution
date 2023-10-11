using Dapper;
using EndTermProjectBack.Models.Entities;
using EndTermProjectBack.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EndTermProjectBack.Repositories
{
	public class StockDataRepository
	{
		public List<SummaryStock> GetAll()
		{
			string sql = @"CalculateSummaryByBrandAndCategory";

			using (var conn = new SqlDb().GetConnection("AppDbContext"))
			{
				return conn.Query<SummaryStock>(sql, commandType: CommandType.StoredProcedure)
							.ToList();
			}
		}
	}
}