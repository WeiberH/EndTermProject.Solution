using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EndTermProjectBack.Models.EFModels;
using EndTermProjectBack.Models.Infra;
using EndTermProjectBack.Models.ViewModels;

namespace EndTermProjectBack.Controllers
{
	public class ProductsController : Controller
	{
		private AppDbContext db = new AppDbContext();

		// GET: Products/Search
		[HttpGet]
		public ActionResult Search(string productName, int? brandId, int? categoryId, int? noteId, int? page)
		{
			var productsQuery = db.Products.AsQueryable();

			if (!string.IsNullOrEmpty(productName))
			{
				productsQuery = productsQuery.Where(p => p.Name.Contains(productName));
			}

			if (brandId.HasValue)
			{
				productsQuery = productsQuery.Where(p => p.BrandId == brandId.Value);
			}

			if (categoryId.HasValue)
			{
				productsQuery = productsQuery.Where(p => p.SecondaryCategoryId == categoryId.Value);
			}

			if (noteId.HasValue)
			{
				productsQuery = productsQuery.Where(p => p.NoteId == noteId.Value);
			}

			productsQuery = PaginateQuery(productsQuery, page);

			ViewBag.ProductName = productName;

			// 篩選的條件
			ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name");
			ViewBag.CategoryId = new SelectList(db.SecondaryCategories, "Id", "Name");
			ViewBag.NoteId = new SelectList(db.Notes, "Id", "Name");

			// 保存篩選條件給page使用
			ViewBag.BrandIdValue = brandId;
			ViewBag.CategoryIdValue = categoryId;
			ViewBag.NoteIdValue = noteId;

			return View("Index", productsQuery.Include(p => p.Brand).Include(p => p.Capacity).Include(p => p.Note).Include(p => p.SecondaryCategory).ToList());
		}

		private const int PageSize = 10;
		private IQueryable<Product> PaginateQuery(IQueryable<Product> productsQuery, int? page)
		{
			int pageNumber = (page ?? 1); // 默認顯示第一頁

			// 計算總頁數
			int totalProducts = productsQuery.Count();
			int totalPages = (int)Math.Ceiling((double)totalProducts / PageSize);

			// 使用Skip和Take方法進行分頁
			productsQuery = productsQuery.OrderBy(p => p.Id) // 需要排序，否則Skip和Take可能不正確
						 .Skip((pageNumber - 1) * PageSize)
						 .Take(PageSize);

			// 將分頁信息傳遞給View
			ViewBag.CurrentPage = pageNumber;
			ViewBag.TotalPages = totalPages;

			return productsQuery;
		}

		// GET: Products
		public ActionResult Index(int? page)
		{
			var productsQuery = db.Products.AsQueryable();

			productsQuery = PaginateQuery(productsQuery, page);

			//ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name");
			//ViewBag.CategoryId = new SelectList(db.SecondaryCategories, "Id", "Name");
			//ViewBag.NoteId = new SelectList(db.Notes, "Id", "Name");

			ViewBag.BrandsList = new SelectList(db.Brands, "Id", "Name");
			ViewBag.CategoriesList = new SelectList(db.SecondaryCategories, "Id", "Name");
			ViewBag.NotesList = new SelectList(db.Notes, "Id", "Name");


			return View(productsQuery.Include(p => p.Brand).Include(p => p.Capacity).Include(p => p.Note).Include(p => p.SecondaryCategory).ToList());
		}

		// GET: Products/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = db.Products.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}

		// GET: Products/Create
		public ActionResult Create()
		{
			ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name");
			ViewBag.CapacityId = new SelectList(db.Capacities, "Id", "Name");
			ViewBag.NoteId = new SelectList(db.Notes, "Id", "Name");
			ViewBag.SecondaryCategoryId = new SelectList(db.SecondaryCategories, "Id", "Name");
			return View(new ProductViewModel());
		}

		// POST: Products/Create
		// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
		// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(ProductViewModel productVm)
		{
			try
			{
				// 初始化您的驗證器
				var validators = new IFileValidator[]
				{
					new FileSizeValidator(500), // 例如，500 KB為檔案大小上限
					new FileRequired(),
					new ImageValidator()
				};

				string productImagesPath = System.Configuration.ConfigurationManager.AppSettings["ProductImagesPath"];
				string productFilesPath = System.Configuration.ConfigurationManager.AppSettings["ProductFilesPath"];


				// 處理主圖片上傳
				if (productVm.UploadedImages != null && productVm.UploadedImages.Any(p => p != null && p.ContentLength > 0))
				{
					var image = productVm.UploadedImages.First(p => p != null && p.ContentLength > 0); // 取得第一張圖片

					// 使用UploadFileHelper保存和驗證檔案
					var fileName = UploadFileHelper.Save(image, Server.MapPath("~/Images/"), validators);

					// 將圖片的檔名保存到Product的Image資料行
					productVm.Image = fileName;

					// 複製文件到前台
					string sourceFullPath = Path.Combine(Server.MapPath("~/Images/"), fileName);
					string destFullPath = Path.Combine(productImagesPath, fileName);
					System.IO.File.Copy(sourceFullPath, destFullPath, true);
				}

				// 處理多圖片上傳
				if (productVm.ProductImages != null)
				{
					foreach (var img in productVm.ProductImages)
					{
						if (img != null && img.ContentLength > 0)
						{
							// 使用UploadFileHelper保存和驗證檔案
							var fileName = UploadFileHelper.Save(img, Server.MapPath("~/FileName/"), validators);

							var productImage = new ProductImage
							{
								FileName = fileName,
								ProductId = productVm.Id  // 設置關聯到剛剛保存的Product的Id
							};

							db.ProductImages.Add(productImage);

							// 複製文件到前台
							string sourceFullPath = Path.Combine(Server.MapPath("~/FileName/"), fileName);
							string destFullPath = Path.Combine(productFilesPath, fileName);
							System.IO.File.Copy(sourceFullPath, destFullPath, true);
						}
					}
				}

				// 如果上面的驗證全部通過，再進行下面的數據庫操作
				if (ModelState.IsValid)
				{
					Product product = new Product
					{
						Name = productVm.Name,
						BrandId = productVm.BrandId,
						CapacityId = productVm.CapacityId,
						SecondaryCategoryId = productVm.SecondaryCategoryId,
						NoteId = productVm.NoteId,
						Price = productVm.Price,
						Description = productVm.Description,
						Method = productVm.Method,
						Ingredient = productVm.Ingredient,
						Image = productVm.Image,
						Stock = productVm.Stock,
						Enabled = productVm.Enabled
					};
					db.Products.Add(product);
					TempData["SuccessMessage"] = "新增成功，請至最末頁查看";
					db.SaveChanges();
					return RedirectToAction("Index");
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("ProductImages", ex.Message);
			}

			ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", productVm.BrandId);
			ViewBag.CapacityId = new SelectList(db.Capacities, "Id", "Name", productVm.CapacityId);
			ViewBag.NoteId = new SelectList(db.Notes, "Id", "Name", productVm.NoteId);
			ViewBag.SecondaryCategoryId = new SelectList(db.SecondaryCategories, "Id", "Name", productVm.SecondaryCategoryId);
			return View(productVm);
		}


		// GET: Products/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = db.Products.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}

			// 轉換 Product 到 ProductViewModel
			ProductViewModel productVm = new ProductViewModel
			{
				Id = product.Id,
				Name = product.Name,
				BrandId = product.BrandId,
				CapacityId = product.CapacityId,
				SecondaryCategoryId = product.SecondaryCategoryId,
				NoteId = product.NoteId,
				Price = product.Price,
				Description = product.Description,
				Method = product.Method,
				Ingredient = product.Ingredient,
				Image = product.Image,
				Stock = product.Stock,
				Enabled = product.Enabled,
				// 如果ProductViewModel還有其他屬性，您需要在這裡繼續進行映射
			};

			ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", product.BrandId);
			ViewBag.CapacityId = new SelectList(db.Capacities, "Id", "Name", product.CapacityId);
			ViewBag.NoteId = new SelectList(db.Notes, "Id", "Name", product.NoteId);
			ViewBag.SecondaryCategoryId = new SelectList(db.SecondaryCategories, "Id", "Name", product.SecondaryCategoryId);

			// 返回 ProductViewModel 到視圉
			return View(productVm);
		}

		// POST: Products/Edit/5
		// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
		// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Name,BrandId,CapacityId,SecondaryCategoryId,NoteId,Price,Description,Method,Ingredient,Image,Stock,Enabled")] Product product)
		{
			var originalProduct = db.Products.Find(product.Id);

			if (originalProduct != null)
			{
				// 保留原始的Image值
				product.Image = originalProduct.Image;

				// Update properties
				db.Entry(originalProduct).CurrentValues.SetValues(product);

				TempData["SuccessMessage"] = "編輯成功，請至該商品查看";
				db.SaveChanges();

				return RedirectToAction("Index");
			}
			ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", product.BrandId);
			ViewBag.CapacityId = new SelectList(db.Capacities, "Id", "Name", product.CapacityId);
			ViewBag.NoteId = new SelectList(db.Notes, "Id", "Name", product.NoteId);
			ViewBag.SecondaryCategoryId = new SelectList(db.SecondaryCategories, "Id", "Name", product.SecondaryCategoryId);
			return View(product);
		}

		// GET: Products/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = db.Products.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}

		// POST: Products/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Product product = db.Products.Find(id);

			var relatedImages = db.ProductImages.Where(p => p.ProductId == id);
			db.ProductImages.RemoveRange(relatedImages);
			db.Products.Remove(product);

			TempData["SuccessMessage"] = "刪除成功，請檢查該商品是否已不在清單頁";
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
