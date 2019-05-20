using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repository.Application.DataModel;

namespace Web.MainApplication.Controllers
{
    public class DeliveryOrderInvoiceController : BaseController
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();

        // GET: DeliveryOrderInvoice
        public ActionResult Index()
        {
            return View(db.DeliveryOrderInvoice.ToList());
        }

        // GET: DeliveryOrderInvoice/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryOrderInvoice deliveryOrderInvoice = db.DeliveryOrderInvoice.Find(id);
            if (deliveryOrderInvoice == null)
            {
                return HttpNotFound();
            }
            return View(deliveryOrderInvoice);
        }

        // GET: DeliveryOrderInvoice/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeliveryOrderInvoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeliveryOrderInvoiceNumber,DeliveryOrderNumber,Amount,Remark,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] DeliveryOrderInvoice deliveryOrderInvoice)
        {
            if (ModelState.IsValid)
            {
                db.DeliveryOrderInvoice.Add(deliveryOrderInvoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(deliveryOrderInvoice);
        }

        // GET: DeliveryOrderInvoice/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryOrderInvoice deliveryOrderInvoice = db.DeliveryOrderInvoice.Find(id);
            if (deliveryOrderInvoice == null)
            {
                return HttpNotFound();
            }
            return View(deliveryOrderInvoice);
        }

        // POST: DeliveryOrderInvoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeliveryOrderInvoiceNumber,DeliveryOrderNumber,Amount,Remark,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] DeliveryOrderInvoice deliveryOrderInvoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deliveryOrderInvoice).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deliveryOrderInvoice);
        }

        // GET: DeliveryOrderInvoice/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryOrderInvoice deliveryOrderInvoice = db.DeliveryOrderInvoice.Find(id);
            if (deliveryOrderInvoice == null)
            {
                return HttpNotFound();
            }
            return View(deliveryOrderInvoice);
        }

        // POST: DeliveryOrderInvoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DeliveryOrderInvoice deliveryOrderInvoice = db.DeliveryOrderInvoice.Find(id);
            db.DeliveryOrderInvoice.Remove(deliveryOrderInvoice);
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
