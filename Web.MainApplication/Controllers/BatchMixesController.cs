using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.Application.DataModel;
using EntityState = System.Data.Entity.EntityState;

namespace Web.MainApplication.Controllers
{
    public class BatchMixesController : BaseController
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();

        // GET: BatchMixes
        public ActionResult Index()
        {
            var batchMix = db.BatchMix.Include(b => b.PRODUCT);
            return View(batchMix.ToList());
        }

        // GET: BatchMixes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BatchMix batchMix = db.BatchMix.Find(id);
            if (batchMix == null)
            {
                return HttpNotFound();
            }
            return View(batchMix);
        }

        // GET: BatchMixes/Create
        public ActionResult Create()
        {
            List<SelectListItem> lsProduct = new List<SelectListItem>();
            lsProduct.AddBlank();
            db.PRODUCT.OrderBy(x => x.PRODUCTNAME).ToList().ForEach(x =>
            {
                lsProduct.AddItemValText(x.PRODUCTID, x.PRODUCTNAME + " - " + x.PRODUCTDESCRIPTION);
            });
            ViewBag.RawProduct = lsProduct.ToSelectList();
            List<SelectListItem> lsProductMature = new List<SelectListItem>();
            lsProductMature.AddBlank();
            db.PRODUCT.Where(x => x.TypeMaterial == "Mature").OrderBy(x => x.PRODUCTNAME).ToList().ForEach(x =>
            {
                lsProductMature.AddItemValText(x.PRODUCTID, x.PRODUCTNAME + " - " + x.PRODUCTDESCRIPTION);
            });
            BatchMix batch = new BatchMix();
            batch.BatchMixProduct.Add(new BatchMixProduct());
            ViewBag.MatureProduct = lsProductMature.ToSelectList();
            return View(batch);
            //ViewBag.OutputProduct = new SelectList(db.PRODUCT, "PRODUCTID", "PRODUCTNAME");
            //return View();
        }

        // POST: BatchMixes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BatchMixCode,BatchMixName,OutputProduct,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] BatchMix batchMix)
        {
            if (ModelState.IsValid)
            {
                db.BatchMix.Add(batchMix);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OutputProduct = new SelectList(db.PRODUCT, "PRODUCTID", "PRODUCTNAME", batchMix.OutputProduct);
            return View(batchMix);
        }

        // GET: BatchMixes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BatchMix batchMix = db.BatchMix.Find(id);
            if (batchMix == null)
            {
                return HttpNotFound();
            }
            ViewBag.OutputProduct = new SelectList(db.PRODUCT, "PRODUCTID", "PRODUCTNAME", batchMix.OutputProduct);
            return View(batchMix);
        }

        // POST: BatchMixes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BatchMixCode,BatchMixName,OutputProduct,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] BatchMix batchMix)
        {
            if (ModelState.IsValid)
            {
                db.Entry(batchMix).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OutputProduct = new SelectList(db.PRODUCT, "PRODUCTID", "PRODUCTNAME", batchMix.OutputProduct);
            return View(batchMix);
        }

        // GET: BatchMixes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BatchMix batchMix = db.BatchMix.Find(id);
            if (batchMix == null)
            {
                return HttpNotFound();
            }
            return View(batchMix);
        }

        // POST: BatchMixes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BatchMix batchMix = db.BatchMix.Find(id);
            db.BatchMix.Remove(batchMix);
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
