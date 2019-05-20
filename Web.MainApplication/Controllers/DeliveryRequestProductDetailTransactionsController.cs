using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Repository.Application.DataModel;

namespace Web.MainApplication.Controllers
{
    public class DeliveryRequestProductDetailTransactionsController : Controller
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();

        // GET: DeliveryRequestProductDetailTransactions
        public ActionResult Index()
        {
            var deliveryRequestProductDetailTransaction = db.DeliveryRequestProductDetailTransaction.Include(d => d.DeliveryRequest).Include(d => d.PRODUCT);
            return View(deliveryRequestProductDetailTransaction.ToList());
        }

        // GET: DeliveryRequestProductDetailTransactions/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryRequestProductDetailTransaction deliveryRequestProductDetailTransaction = db.DeliveryRequestProductDetailTransaction.Find(id);
            if (deliveryRequestProductDetailTransaction == null)
            {
                return HttpNotFound();
            }
            return View(deliveryRequestProductDetailTransaction);
        }

        // GET: DeliveryRequestProductDetailTransactions/Create
        public ActionResult Create()
        {
            ViewBag.DeliveryRequestId = new SelectList(db.DeliveryRequest, "DeliveryRequestId", "DeliveryRequestNumber");
            ViewBag.ProductId = new SelectList(db.PRODUCT, "PRODUCTID", "PRODUCTNAME");
            return View();
        }

        // POST: DeliveryRequestProductDetailTransactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DeliveryRequestId,ProductId,Amount,Status,DriverName,ArmadaPlatNumber,Remark,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] DeliveryRequestProductDetailTransaction deliveryRequestProductDetailTransaction)
        {
            if (ModelState.IsValid)
            {
                db.DeliveryRequestProductDetailTransaction.Add(deliveryRequestProductDetailTransaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeliveryRequestId = new SelectList(db.DeliveryRequest, "DeliveryRequestId", "DeliveryRequestNumber", deliveryRequestProductDetailTransaction.DeliveryRequestId);
            ViewBag.ProductId = new SelectList(db.PRODUCT, "PRODUCTID", "PRODUCTNAME", deliveryRequestProductDetailTransaction.ProductId);
            return View(deliveryRequestProductDetailTransaction);
        }

        // GET: DeliveryRequestProductDetailTransactions/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryRequestProductDetailTransaction deliveryRequestProductDetailTransaction = db.DeliveryRequestProductDetailTransaction.Find(id);
            if (deliveryRequestProductDetailTransaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeliveryRequestId = new SelectList(db.DeliveryRequest, "DeliveryRequestId", "DeliveryRequestNumber", deliveryRequestProductDetailTransaction.DeliveryRequestId);
            ViewBag.ProductId = new SelectList(db.PRODUCT, "PRODUCTID", "PRODUCTNAME", deliveryRequestProductDetailTransaction.ProductId);
            return View(deliveryRequestProductDetailTransaction);
        }

        // POST: DeliveryRequestProductDetailTransactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DeliveryRequestId,ProductId,Amount,Status,DriverName,ArmadaPlatNumber,Remark,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] DeliveryRequestProductDetailTransaction deliveryRequestProductDetailTransaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deliveryRequestProductDetailTransaction).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeliveryRequestId = new SelectList(db.DeliveryRequest, "DeliveryRequestId", "DeliveryRequestNumber", deliveryRequestProductDetailTransaction.DeliveryRequestId);
            ViewBag.ProductId = new SelectList(db.PRODUCT, "PRODUCTID", "PRODUCTNAME", deliveryRequestProductDetailTransaction.ProductId);
            return View(deliveryRequestProductDetailTransaction);
        }

        // GET: DeliveryRequestProductDetailTransactions/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryRequestProductDetailTransaction deliveryRequestProductDetailTransaction = db.DeliveryRequestProductDetailTransaction.Find(id);
            if (deliveryRequestProductDetailTransaction == null)
            {
                return HttpNotFound();
            }
            return View(deliveryRequestProductDetailTransaction);
        }

        // POST: DeliveryRequestProductDetailTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            DeliveryRequestProductDetailTransaction deliveryRequestProductDetailTransaction = db.DeliveryRequestProductDetailTransaction.Find(id);
            db.DeliveryRequestProductDetailTransaction.Remove(deliveryRequestProductDetailTransaction);
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
