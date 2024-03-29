﻿using System;
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
    public class NostroBankController : BaseController
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();

        // GET: NostroBank
        public ActionResult Index()
        {
            return View(db.NostroBank.ToList());
        }

        // GET: NostroBank/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NostroBank nostroBank = db.NostroBank.Find(id);
            if (nostroBank == null)
            {
                return HttpNotFound();
            }
            return View(nostroBank);
        }

        // GET: NostroBank/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NostroBank/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NostroBankId,BankId,BankBranchCode,NostroBankName,NostroAccountNumber,NostroAccountName,BankCategory,Remark,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] NostroBank nostroBank)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    nostroBank.SetPropertyCreate();
                    db.NostroBank.Add(nostroBank);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    WarningMessagesAdd(e.MessageToList());
                }
                
            }

            return View(nostroBank);
        }

        // GET: NostroBank/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NostroBank nostroBank = db.NostroBank.Find(id);
            if (nostroBank == null)
            {
                return HttpNotFound();
            }
            return View(nostroBank);
        }

        // POST: NostroBank/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NostroBankId,BankId,BankBranchCode,NostroBankName,NostroAccountNumber,NostroAccountName,BankCategory,Remark,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] NostroBank nostroBank)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nostroBank).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nostroBank);
        }

        // GET: NostroBank/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NostroBank nostroBank = db.NostroBank.Find(id);
            if (nostroBank == null)
            {
                return HttpNotFound();
            }
            return View(nostroBank);
        }

        // POST: NostroBank/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NostroBank nostroBank = db.NostroBank.Find(id);
            db.NostroBank.Remove(nostroBank);
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
