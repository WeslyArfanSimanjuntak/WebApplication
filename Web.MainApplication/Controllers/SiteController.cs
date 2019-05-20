using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Repository.Application.DataModel;

namespace Web.MainApplication.Controllers
{
    public class SiteController : BaseController
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();

        // GET: Site
        public ActionResult Index()
        {
            return View(db.SITE.ToList());
        }

        // GET: Site/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SITE sITE = db.SITE.Find(id);
            if (sITE == null)
            {
                return HttpNotFound();
            }
            return View(sITE);
        }

        // GET: Site/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Site/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SITENAME,SITEADDRESS")] SITE sITE)
        {
            if (ModelState.IsValid)
            {
                db.SITE.Add(sITE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sITE);
        }

        // GET: Site/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SITE sITE = db.SITE.Find(id);
            if (sITE == null)
            {
                return HttpNotFound();
            }
            return View(sITE);
        }

        // POST: Site/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SITENAME,SITEADDRESS")] SITE sITE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sITE).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sITE);
        }

        // GET: Site/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SITE sITE = db.SITE.Find(id);
            if (sITE == null)
            {
                return HttpNotFound();
            }
            return View(sITE);
        }

        // POST: Site/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {

                SITE sITE = db.SITE.Find(id);
                db.SITE.Remove(sITE);
                db.SaveChanges();
                SuccessMessagesAdd("Deleting is Success");
            }
            catch (System.Exception ex)
            {
                ErrorMessagesAdd("Deleting is Fail. "+ ex.Message);
            }
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
