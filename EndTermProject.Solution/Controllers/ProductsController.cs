using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EndTermProject.Solution.Models.EFModels;

namespace EndTermProject.Solution.Controllers
{
	public class ProductsController : Controller
	{
		private AppDbContext db = new AppDbContext();

		// GET: Products
		public ActionResult Index()
		{
			var products = db.Products.Include(p => p.Brand).Include(p => p.Capacity).Include(p => p.Note).Include(p => p.SecondaryCategory);
			return View(products.ToList());
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
			return View();
		}

		// POST: Products/Create
		// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
		// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Name,BrandId,CapacityId,SecondaryCategoryId,NoteId,Price,Description,Method,Ingredient,Image,Stock,Enabled")] Product product)
		{
			if (ModelState.IsValid)
			{
				db.Products.Add(product);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", product.BrandId);
			ViewBag.CapacityId = new SelectList(db.Capacities, "Id", "Name", product.CapacityId);
			ViewBag.NoteId = new SelectList(db.Notes, "Id", "Name", product.NoteId);
			ViewBag.SecondaryCategoryId = new SelectList(db.SecondaryCategories, "Id", "Name", product.SecondaryCategoryId);
			return View(product);
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
			ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", product.BrandId);
			ViewBag.CapacityId = new SelectList(db.Capacities, "Id", "Name", product.CapacityId);
			ViewBag.NoteId = new SelectList(db.Notes, "Id", "Name", product.NoteId);
			ViewBag.SecondaryCategoryId = new SelectList(db.SecondaryCategories, "Id", "Name", product.SecondaryCategoryId);
			return View(product);
		}

		// POST: Products/Edit/5
		// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
		// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Name,BrandId,CapacityId,SecondaryCategoryId,NoteId,Price,Description,Method,Ingredient,Image,Stock,Enabled")] Product product)
		{
			if (ModelState.IsValid)
			{
				db.Entry(product).State = EntityState.Modified;
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
			db.Products.Remove(product);
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
