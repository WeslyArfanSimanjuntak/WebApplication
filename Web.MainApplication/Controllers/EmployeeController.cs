using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Repository.Application.DataModel;

namespace Web.MainApplication.Controllers
{
    public class EmployeeController : BaseController
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();

        // GET: Employee
        public ActionResult Index()
        {
            return View(db.EMPLOYEE.ToList());
        }

        // GET: Employee/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLOYEE eMPLOYEE = db.EMPLOYEE.Find(id);
            if (eMPLOYEE == null)
            {
                return HttpNotFound();
            }
            return View(eMPLOYEE);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            var siteNameList = new List<SelectListItem>();
            siteNameList.AddBlank();
            db.SITE.ToList().ForEach(x =>
            {
                siteNameList.AddItemValText(x.SITENAME, x.SITENAME + " - " + x.SITEADDRESS);
            });
            var activeInActiveList = new List<SelectListItem>();
            activeInActiveList.AddBlank();
            activeInActiveList.AddItemValText("active", "Active");
            activeInActiveList.AddItemValText("non-active", "Non-Active");
            ViewBag.EMPLOYEESTATUS = activeInActiveList.ToSelectList("");
            ViewBag.SITENAME = siteNameList.ToSelectList("");
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SITENAME,EMPLOYEEID,EMPLOYEENAME,EMPLOYEEAGE,EMPLOYEEFULLNAME,EMPLOYEEJOINAT,EMPLOYEESTATUS,EmployeeGroup")] EMPLOYEE eMPLOYEE)
        {
            if (ModelState.IsValid)
            {
                db.EMPLOYEE.Add(eMPLOYEE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var siteNameList = new List<SelectListItem>();
            siteNameList.AddBlank();
            db.SITE.ToList().ForEach(x =>
            {
                siteNameList.AddItemValText(x.SITENAME, x.SITENAME + " - " + x.SITEADDRESS);
            });
            ViewBag.SITENAME = siteNameList.ToSelectList(eMPLOYEE.SiteName);
            var activeInActiveList = new List<SelectListItem>();
            activeInActiveList.AddBlank();
            activeInActiveList.AddItemValText("active", "Active");
            activeInActiveList.AddItemValText("non-active", "Non-Active");
            ViewBag.EMPLOYEESTATUS = activeInActiveList.ToSelectList(eMPLOYEE.EMPLOYEESTATUS);
           
            return View(eMPLOYEE);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLOYEE eMPLOYEE = db.EMPLOYEE.Find(id);
            if (eMPLOYEE == null)
            {
                return HttpNotFound();
            }
            var siteNameList = new List<SelectListItem>();
            siteNameList.AddBlank();
            db.SITE.ToList().ForEach(x =>
            {
                siteNameList.AddItemValText(x.SITENAME, x.SITENAME + " - " + x.SITEADDRESS);
            });
            ViewBag.SITENAME = siteNameList.ToSelectList(eMPLOYEE.SiteName);
            var activeInActiveList = new List<SelectListItem>();
            activeInActiveList.AddBlank();
            activeInActiveList.AddItemValText("active", "Active");
            activeInActiveList.AddItemValText("non-active", "Non-Active");
            ViewBag.EMPLOYEESTATUS = activeInActiveList.ToSelectList(eMPLOYEE.EMPLOYEESTATUS);

            return View(eMPLOYEE);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SITENAME,EMPLOYEEID,EMPLOYEENAME,EMPLOYEEAGE,EMPLOYEEFULLNAME,EMPLOYEEJOINAT,EMPLOYEESTATUS")] EMPLOYEE eMPLOYEE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eMPLOYEE).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var siteNameList = new List<SelectListItem>();
            siteNameList.AddBlank();
            db.SITE.ToList().ForEach(x =>
            {
                siteNameList.AddItemValText(x.SITENAME, x.SITENAME + " - " + x.SITEADDRESS);
            });
            ViewBag.SITENAME = siteNameList.ToSelectList(eMPLOYEE.SiteName);
            return View(eMPLOYEE);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLOYEE eMPLOYEE = db.EMPLOYEE.Find(id);
            if (eMPLOYEE == null)
            {
                return HttpNotFound();
            }
            return View(eMPLOYEE);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            EMPLOYEE eMPLOYEE = db.EMPLOYEE.Find(id);
            db.EMPLOYEE.Remove(eMPLOYEE);
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
