using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Repository.Application.DataModel;

namespace Web.MainApplication.Controllers
{
    public class SourceController : BaseController
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();

        // GET: Source
        public ActionResult Index()
        {
            return View(db.SOURCE.ToList());
        }

        // GET: Source/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SOURCE sOURCE = db.SOURCE.Find(id);
            if (sOURCE == null)
            {
                return HttpNotFound();
            }
            return View(sOURCE);
        }

        // GET: Source/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Source/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,Location,Description")] SOURCE sOURCE)
        {
            if (ModelState.IsValid)
            {
                db.SOURCE.Add(sOURCE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sOURCE);
        }

        // GET: Source/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SOURCE sOURCE = db.SOURCE.Find(id);
            if (sOURCE == null)
            {
                return HttpNotFound();
            }
            var sliIsActive = new List<SelectListItem>();
            sliIsActive.AddBlank();
            sliIsActive.AddItemValText("1", "Active");
            sliIsActive.AddItemValText("0", "Non-active");
            ViewBag.IsActive = WebAppUtility.SelectListIsActive(sOURCE.IsActive);
            //sliIsActive.ToSelectList(sOURCE.IsActive);
            return View(sOURCE);
        }

        // POST: Source/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,Location,Description,IsActive")] SOURCE sOURCE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sOURCE).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sOURCE);
        }

        // GET: Source/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SOURCE sOURCE = db.SOURCE.Find(id);
            if (sOURCE == null)
            {
                return HttpNotFound();
            }
            return View(sOURCE);
        }

        // POST: Source/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                SOURCE sOURCE = db.SOURCE.Find(id);
                db.SOURCE.Remove(sOURCE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                ErrorMessagesAdd(ex.Message);
                return RedirectToAction("Index");
            }
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
