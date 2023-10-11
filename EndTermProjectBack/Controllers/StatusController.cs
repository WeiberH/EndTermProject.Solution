using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EndTermProjectBack.Models.EFModels;
using EndTermProjectBack.Models.ViewModels;

namespace EndTermProjectBack.Controllers
{
    public class StatusController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Status
        public ActionResult Index()
        {
            var vm = db.Status.Select(s => new StatusVm
            {
                Id = s.Id,
                Name = s.Status1,
            }).ToList();

			return View(vm);
        }

        
        // GET: Status/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Status/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StatusVm vm)
        {
            if (ModelState.IsValid)
            {
                var status = new Status
                {
                    Id = vm.Id,
                    Status1 = vm.Name,
                };

                db.Status.Add(status);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: Status/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Status status = db.Status.Find(id);
            if (status == null)
            {
                return HttpNotFound();
            }
            var vm = new StatusVm
            {
                Id = status.Id,
                Name = status.Status1
            };
            return View(vm);
        }

        // POST: Status/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StatusVm vm)
        {
            if (ModelState.IsValid)
            {
                var status = new Status
                {
                    Id= vm.Id,
                    Status1 = vm.Name,
                };

                db.Entry(status).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
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
