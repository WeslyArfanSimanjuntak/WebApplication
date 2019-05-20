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
    public class DeliveryOrderController : BaseController
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();

        // GET: DeliveryOrder
        public ActionResult Index()
        {
            var deliveryOrder = db.DeliveryOrder.Include(d => d.DeliveryRequest);
            return View(deliveryOrder.ToList());
        }

        // GET: DeliveryOrder/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryOrder deliveryOrder = db.DeliveryOrder.Find(id);
            if (deliveryOrder == null)
            {
                return HttpNotFound();
            }
            return View(deliveryOrder);
        }

        // GET: DeliveryOrder/Create
        public ActionResult Create()
        {
            ViewBag.DeliveryRequestId = new SelectList(db.DeliveryRequest, "DeliveryRequestId", "DeliveryRequestNumber");
            return View();
        }

        // POST: DeliveryOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeliveryOrderNumber,DeliveryRequestId,Amount,Remark,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] DeliveryOrder deliveryOrder)
        {
            if (ModelState.IsValid)
            {
                db.DeliveryOrder.Add(deliveryOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeliveryRequestId = new SelectList(db.DeliveryRequest, "DeliveryRequestId", "DeliveryRequestNumber", deliveryOrder.DeliveryRequestId);
            return View(deliveryOrder);
        }

        // GET: DeliveryOrder/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryOrder deliveryOrder = db.DeliveryOrder.Find(id);
            if (deliveryOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeliveryRequestId = new SelectList(db.DeliveryRequest, "DeliveryRequestId", "DeliveryRequestNumber", deliveryOrder.DeliveryRequestId);
            return View(deliveryOrder);
        }

        // POST: DeliveryOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeliveryOrderNumber,DeliveryRequestId,Amount,Remark,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] DeliveryOrder deliveryOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deliveryOrder).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeliveryRequestId = new SelectList(db.DeliveryRequest, "DeliveryRequestId", "DeliveryRequestNumber", deliveryOrder.DeliveryRequestId);
            return View(deliveryOrder);
        }

        // GET: DeliveryOrder/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryOrder deliveryOrder = db.DeliveryOrder.Find(id);
            if (deliveryOrder == null)
            {
                return HttpNotFound();
            }
            return View(deliveryOrder);
        }

        // POST: DeliveryOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DeliveryOrder deliveryOrder = db.DeliveryOrder.Find(id);
            db.DeliveryOrder.Remove(deliveryOrder);
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
