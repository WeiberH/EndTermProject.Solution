using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndTermProjectBack.Models.Infra
{
	public class FileSizeValidator : IFileValidator
	{

		private readonly int _maxBytes, _maxKBs;

		public FileSizeValidator(int maxKBs)
		{
			if (maxKBs <= 0)
			{
				throw new ArgumentException("maxKBs must be greater than zero");
			}
			_maxKBs = maxKBs;
			_maxBytes = maxKBs * 1024;
		}

		public void Validate(HttpPostedFileBase file)
		{
			if (file == null || file.ContentLength == 0) return;

			if (file.ContentLength > _maxBytes)
			{
				throw new Exception("檔案太大了, 必須小於" + _maxKBs + "KB.");
			}
		}
	}
}