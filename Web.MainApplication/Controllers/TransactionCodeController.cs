using Repository.Application.DataModel;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Web.MainApplication.Controllers
{
    public class TransactionCodeController : BaseController
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();

        // GET: TransactionCode
        public ActionResult Index()
        {
            return View(db.TransactionCode.ToList());
        }

        // GET: TransactionCode/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionCode transactionCode = db.TransactionCode.Find(id);
            if (transactionCode == null)
            {
                return HttpNotFound();
            }
            return View(transactionCode);
        }

        // GET: TransactionCode/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransactionCode/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransactionCode1,TransactionName,CreditOrDebit,UserOrSystem,Debit,Credit,Remark,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] TransactionCode transactionCode)
        {
            if (ModelState.IsValid)
            {
                db.TransactionCode.Add(transactionCode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transactionCode);
        }

        // GET: TransactionCode/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionCode transactionCode = db.TransactionCode.Find(id);
            if (transactionCode == null)
            {
                return HttpNotFound();
            }
            return View(transactionCode);
        }

        // POST: TransactionCode/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionCode1,TransactionName,CreditOrDebit,UserOrSystem,Debit,Credit,Remark,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] TransactionCode transactionCode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transactionCode).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transactionCode);
        }

        // GET: TransactionCode/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionCode transactionCode = db.TransactionCode.Find(id);
            if (transactionCode == null)
            {
                return HttpNotFound();
            }
            return View(transactionCode);
        }

        // POST: TransactionCode/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TransactionCode transactionCode = db.TransactionCode.Find(id);
            db.TransactionCode.Remove(transactionCode);
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
