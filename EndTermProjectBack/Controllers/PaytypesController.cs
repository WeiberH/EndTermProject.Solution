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
    public class PaytypesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Paytypes
        public ActionResult Index()
        {

            var vm = db.Paytypes.ToList().Select(p => new PaytypeVm
            {
                Id = p.Id,
                Name = p.Paytype1,
                Enabled = p.Enabled,
            });

			return View(vm);
        }

        // GET: Paytypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Paytypes/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaytypeVm vm)
        {
            if (ModelState.IsValid)
            {
                var paytype = new Paytype
                {
                    Id= vm.Id,
                    Paytype1 = vm.Name,
                    Enabled = vm.Enabled,                    
                };
                db.Paytypes.Add(paytype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: Paytypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paytype paytype = db.Paytypes.Find(id);
            if (paytype == null)
            {
                return HttpNotFound();
            }
            var vm = new PaytypeVm
            {
                Id = paytype.Id,
                Name = paytype.Paytype1,
                Enabled = paytype.Enabled,
            };
            return View(vm);
        }

        // POST: Paytypes/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PaytypeVm vm)
        {
            if (ModelState.IsValid)
            {
                var paytype = new Paytype
                {
                    Id = vm.Id,
                    Paytype1 = vm.Name,
                    Enabled = vm.Enabled,
                };

                db.Entry(paytype).State = EntityState.Modified;
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
