using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Repository.Application.DataModel;

namespace Web.MainApplication.Controllers
{
    public class ArmadaController : BaseController
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();
        //add something after line 13

        // GET: Armada
        public ActionResult Index()
        {
            return View(db.ARMADA.ToList());
        }

        // GET: Armada/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARMADA aRMADA = db.ARMADA.Find(id);
            if (aRMADA == null)
            {
                return HttpNotFound();
            }
            return View(aRMADA);
        }

        // GET: Armada/Create
        public ActionResult Create()
        {
            var selectListSite = new List<SelectListItem>();
            selectListSite.Add(new SelectListItem()
            {
                Value = "",
                Text = "---"
            });
            db.SITE.OrderBy(x => x.SITENAME).ToList().ForEach(x =>
            {
                selectListSite.Add(new SelectListItem()
                {
                    Value = x.SITENAME,
                    Text = x.SITENAME + " - " + x.SOURCE
                });

            });

            ViewBag.SITENAME = new SelectList(selectListSite, "Value", "Text");
            ViewBag.IsActive = WebAppUtility.SelectListIsActive();
            return View();
        }

        // POST: Armada/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IsActive,SITENAME,ARMADAID,ARMADANAME,ARMADACAPACITYINKG,ARMADATOTALTIRE,ARMADACOLOR,ARMADAMERK")] ARMADA aRMADA)
        {
            if (ModelState.IsValid)
            {
                db.ARMADA.Add(aRMADA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var selectListSite = new List<SelectListItem>();
            selectListSite.Add(new SelectListItem()
            {
                Value = "",
                Text = "---"
            });
            db.SITE.OrderBy(x => x.SITENAME).ToList().ForEach(x =>
            {
                selectListSite.Add(new SelectListItem()
                {
                    Value = x.SITENAME,
                    Text = x.SITENAME + " - " + x.SOURCE
                });

            });
            ViewBag.IsActive = WebAppUtility.SelectListIsActive(aRMADA.IsActive);
            ViewBag.SiteName = new SelectList(selectListSite, "Value", "Text");
            return View(aRMADA);
        }

        // GET: Armada/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARMADA aRMADA = db.ARMADA.Find(id);
            if (aRMADA == null)
            {
                return HttpNotFound();
            }
            var siteNameList = new List<SelectListItem>();
            siteNameList.AddBlank();
            db.SITE.ToList().ForEach(x =>
            {
                siteNameList.AddItemValText(x.SITENAME, x.SITENAME + " - " + x.SITEADDRESS);
            });
            ViewBag.SITENAME = siteNameList.ToSelectList(aRMADA.SITENAME);
            ViewBag.IsActive = WebAppUtility.SelectListIsActive(aRMADA.IsActive);
            return View(aRMADA);
        }

        // POST: Armada/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SITENAME,ARMADAID,ARMADANAME,ARMADACAPACITYINKG,ARMADATOTALTIRE,ARMADACOLOR,ARMADAMERK")] BatchProduct aRMADA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aRMADA).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var siteNameList = new List<SelectListItem>();
            siteNameList.AddBlank();
            db.SITE.ToList().ForEach(x =>
            {
                siteNameList.AddItemValText(x.SITENAME, x.SITENAME + " - " + x.SITEADDRESS);
            });
            ViewBag.IsActive = WebAppUtility.SelectListIsActive(aRMADA.IsActive);
            return View(aRMADA);
        }

        // GET: Armada/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ARMADA aRMADA = db.ARMADA.Find(id);
            if (aRMADA == null)
            {
                return HttpNotFound();
            }
            return View(aRMADA);
        }

        // POST: Armada/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ARMADA aRMADA = db.ARMADA.Find(id);
            db.ARMADA.Remove(aRMADA);
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
