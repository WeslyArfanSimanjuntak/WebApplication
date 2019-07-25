using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Repository.Application.DataModel;

namespace Web.MainApplication.Controllers
{
    public class MenuController : BaseController
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();

        // GET: Menu
        public ActionResult Index()
        {
            var menu = db.Menu.Include(m => m.AspNetRoles1).Include(m => m.Menu2);

            return View(menu.ToList());
        }


        // GET: Menu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Menu/Create
        public ActionResult Create()
        {
            var sliRoles = new List<SelectListItem>();
            sliRoles.AddBlank();
            db.AspNetRoles.ToList().ForEach(x =>
            {
                sliRoles.AddItemValText(x.Id.ToString(), x.AspNetRoles2 != null ? (x.AspNetRoles2.Name + " - " + x.Name) : x.Name);
            });

            var sliMenuParentid = new List<SelectListItem>();
            sliMenuParentid.AddBlank();
            db.Menu.ToList().ForEach(x =>
            {
                sliMenuParentid.AddItemValText(x.MenuId.ToString(), x.Menu2 != null ? x.Menu2.MenuName + " - " + x.MenuName : x.MenuName);
            });

            ViewBag.MenuParentId = sliMenuParentid.OrderBy(x => x.Text).ToList().ToSelectList();
            ViewBag.AspNetRoles = sliRoles.ToSelectList();

            return View();
        }

        // POST: Menu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MenuId,MenuName,MenuParentId,Sequence,AspNetRoles,MenuIClass,MenuLevel,Remark,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                menu.SetPropertyCreate();
                db.Menu.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var sliRoles = new List<SelectListItem>();
            sliRoles.AddBlank();
            db.AspNetRoles.ToList().ForEach(x =>
            {
                sliRoles.AddItemValText(x.Id.ToString(), x.AspNetRoles2 != null ? (x.AspNetRoles2.Name + " - " + x.Name) : x.Name);
            });

            var sliMenuParentid = new List<SelectListItem>();
            sliMenuParentid.AddBlank();
            db.Menu.ToList().ForEach(x =>
            {
                sliMenuParentid.AddItemValText(x.MenuId.ToString(), x.Menu2 != null ? x.Menu2.MenuName + " - " + x.MenuName : x.MenuName);
            });

            ViewBag.MenuParentId = sliMenuParentid.OrderBy(x => x.Text).ToList().ToSelectList(menu.MenuParentId);
            ViewBag.AspNetRoles = sliRoles.ToSelectList(menu.AspNetRoles);
            return View(menu);
        }

        // GET: Menu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            var sliRoles = new List<SelectListItem>();
            sliRoles.AddBlank();
            db.AspNetRoles.ToList().ForEach(x =>
            {
                sliRoles.AddItemValText(x.Id.ToString(), x.AspNetRoles2 != null ? (x.AspNetRoles2.Name + " - " + x.Name) : x.Name);
            });

            var sliMenuParentid = new List<SelectListItem>();
            sliMenuParentid.AddBlank();
            db.Menu.ToList().ForEach(x =>
            {
                sliMenuParentid.AddItemValText(x.MenuId.ToString(), x.Menu2 != null ? x.Menu2.MenuName + " - " + x.MenuName : x.MenuName);
            });

            ViewBag.MenuParentId = sliMenuParentid.OrderBy(x => x.Text).ToList().ToSelectList(menu.MenuParentId);
            ViewBag.AspNetRoles = sliRoles.OrderBy(x=>x.Text).ToList().ToSelectList(menu.AspNetRoles);
            return View(menu);
        }

        // POST: Menu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MenuId,MenuName,MenuParentId,Sequence,AspNetRoles,MenuIClass,MenuLevel,Remark,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                menu.SetPropertyUpdate();
                db.Entry(menu).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var sliRoles = new List<SelectListItem>();
            sliRoles.AddBlank();
            db.AspNetRoles.ToList().ForEach(x =>
            {
                sliRoles.AddItemValText(x.Id.ToString(), x.AspNetRoles2 != null ? (x.AspNetRoles2.Name + " - " + x.Name) : x.Name);
            });

            var sliMenuParentid = new List<SelectListItem>();
            sliMenuParentid.AddBlank();
            db.Menu.ToList().ForEach(x =>
            {
                sliMenuParentid.AddItemValText(x.MenuId.ToString(), x.Menu2 != null ? x.Menu2.MenuName + " - " + x.MenuName : x.MenuName);
            });

            ViewBag.MenuParentId = sliMenuParentid.OrderBy(x => x.Text).ToList().ToSelectList(menu.MenuParentId);
            ViewBag.AspNetRoles = sliRoles.OrderBy(x => x.Text).ToList().ToSelectList(menu.AspNetRoles); return View(menu);
        }

        // GET: Menu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menu.Find(id);
            db.Menu.Remove(menu);
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
