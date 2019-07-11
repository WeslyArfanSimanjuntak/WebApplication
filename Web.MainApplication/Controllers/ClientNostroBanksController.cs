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
    public class ClientNostroBanksController : Controller
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();

        // GET: ClientNostroBanks
        public ActionResult Index()
        {
            var clientNostroBank = db.ClientNostroBank.Include(c => c.CLIENT1);
            return View(clientNostroBank.ToList());
        }

        // GET: ClientNostroBanks/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientNostroBank clientNostroBank = db.ClientNostroBank.Find(id);
            if (clientNostroBank == null)
            {
                return HttpNotFound();
            }
            return View(clientNostroBank);
        }

        // GET: ClientNostroBanks/Create
        public ActionResult Create()
        {
            ViewBag.Client = new SelectList(db.CLIENT, "Id", "ClientId");
            return View();
        }

        // POST: ClientNostroBanks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientNostroBankId,Client,BankId,BankBranchCode,NostroBankName,NostroAccountNumber,NostroAccountName,BankCategory,Remark,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] ClientNostroBank clientNostroBank)
        {
            if (ModelState.IsValid)
            {
                db.ClientNostroBank.Add(clientNostroBank);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Client = new SelectList(db.CLIENT, "Id", "ClientId", clientNostroBank.Client);
            return View(clientNostroBank);
        }

        // GET: ClientNostroBanks/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientNostroBank clientNostroBank = db.ClientNostroBank.Find(id);
            if (clientNostroBank == null)
            {
                return HttpNotFound();
            }
            ViewBag.Client = new SelectList(db.CLIENT, "Id", "ClientId", clientNostroBank.Client);
            return View(clientNostroBank);
        }

        // POST: ClientNostroBanks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientNostroBankId,Client,BankId,BankBranchCode,NostroBankName,NostroAccountNumber,NostroAccountName,BankCategory,Remark,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] ClientNostroBank clientNostroBank)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientNostroBank).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Client = new SelectList(db.CLIENT, "Id", "ClientId", clientNostroBank.Client);
            return View(clientNostroBank);
        }

        // GET: ClientNostroBanks/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientNostroBank clientNostroBank = db.ClientNostroBank.Find(id);
            if (clientNostroBank == null)
            {
                return HttpNotFound();
            }
            return View(clientNostroBank);
        }

        // POST: ClientNostroBanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ClientNostroBank clientNostroBank = db.ClientNostroBank.Find(id);
            db.ClientNostroBank.Remove(clientNostroBank);
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
