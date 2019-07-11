using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Repository.Application.DataModel;
using EntityState = System.Data.Entity.EntityState;

namespace Web.MainApplication.Controllers
{
    public class ClientController : BaseController
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();

        // GET: Client
        public ActionResult Index()
        {
            return View(db.CLIENT.ToList());
        }

        // GET: Client/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENT cLIENT = db.CLIENT.Find(id);
            if (cLIENT == null)
            {
                return HttpNotFound();
            }
            return View(cLIENT);
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            List<SelectListItem> clientType = new List<SelectListItem>();
            clientType.AddBlank();
            clientType.AddItemValText("individual", "Individual");
            clientType.AddItemValText("company", "Company");
            ViewBag.ClientType = clientType;
            ViewBag.IsActive = WebAppUtility.SelectListIsActive();
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClientEmail,ClientId,ClientName,ClientAddress,ClientType,ClientCompanyName,ClientCompanyPIC,ClientCompanyLeaderEmailAddress,ClientCompanyPICPhoneNumber,ClientCompanyPIC2,ClientCompanyLeaderEmailAddress2,ClientCompanyPICPhoneNumber2,ClientCompanyPIC3,ClientCompanyLeaderEmailAddress3,ClientCompanyPICPhoneNumber3,ClientPhoneNumber,Remark,IsActive")] CLIENT cLIENT)
        {
            if (db.CLIENT.Where(x => x.ClientId == cLIENT.ClientId).FirstOrDefault() != null)
            {
                var temp = db.CLIENT.Where(x => x.ClientId == cLIENT.ClientId).FirstOrDefault();
                WarningMessagesAdd("Client Id \"" + cLIENT.ClientId + "\" Is Exist");
                var sliClientType = new List<SelectListItem>();
                sliClientType.AddBlank();
                sliClientType.AddItemValText("individual", "Individual");
                sliClientType.AddItemValText("company", "Company");
                sliClientType.ToSelectList(cLIENT.ClientType);

                ViewBag.ClientType = sliClientType;
                return View(cLIENT);
            }

            if (ModelState.IsValid)
            {

                if (cLIENT.ClientType.ToLower() == "company")
                {
                    cLIENT.ClientName = cLIENT.ClientCompanyName;
                }
                cLIENT.SetPropertyCreate();
                db.CLIENT.Add(cLIENT);
                db.SaveChanges();
                SuccessMessagesAdd("Client Id \"" + cLIENT.ClientId + "\" Inserted");

                return RedirectToAction("Index");
            }
            List<SelectListItem> clientType = new List<SelectListItem>();
            clientType.AddBlank();
            clientType.AddItemValText("individual", "Individual");
            clientType.AddItemValText("company", "Company");
            ViewBag.ClientType = clientType.ToSelectList(cLIENT.ClientType);
            ViewBag.IsActive = WebAppUtility.SelectListIsActive(cLIENT.IsActive.ToString());
            return View(cLIENT);
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENT cLIENT = db.CLIENT.Find(id);
            if (cLIENT == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> clientType = new List<SelectListItem>();
            clientType.AddBlank();
            clientType.AddItemValText("individual", "Individual");
            clientType.AddItemValText("company", "Company");
            ViewBag.ClientType = clientType.ToSelectList(cLIENT.ClientType, clientType.Except(clientType.Where(x => x.Value == cLIENT.ClientType)).Select(x => x.Value).ToList());
            ViewBag.IsActive = WebAppUtility.SelectListIsActive(cLIENT.IsActive.ToString());
            return View(cLIENT);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClientEmail,ClientId,ClientName,ClientAddress,ClientType,ClientCompanyName,ClientCompanyPIC,ClientCompanyLeaderEmailAddress,ClientNPWP,ClientPhoneNumber,ClientCompanyPICPhoneNumber,Remark,IsActive")] CLIENT cLIENT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cLIENT).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<SelectListItem> clientType = new List<SelectListItem>();
            clientType.AddBlank();
            clientType.AddItemValText("individual", "Individual");
            clientType.AddItemValText("company", "Company");
            ViewBag.ClientType = clientType;
            ViewBag.IsActive = WebAppUtility.SelectListIsActive(cLIENT.IsActive.ToString());
            return View(cLIENT);
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENT cLIENT = db.CLIENT.Find(id);
            if (cLIENT == null)
            {
                return HttpNotFound();
            }
            return View(cLIENT);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CLIENT cLIENT = db.CLIENT.Find(id);
            db.CLIENT.Remove(cLIENT);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ClientNostroBank(int? id) {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var clientNostroBank = db.ClientNostroBank.Where(x => x.Client == id);
            if (clientNostroBank == null)
            {
                return HttpNotFound();
            }
            return View(clientNostroBank.ToList());

        }
        public ActionResult ClientNostroBankCreate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = db.CLIENT.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            var newClientNostro = new ClientNostroBank();
            newClientNostro.Client = client.Id;
            newClientNostro.CLIENT1 = client;
            return View(newClientNostro);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClientNostroBankCreate(ClientNostroBank clientNostroBank)
        {
            if (ModelState.IsValid) {
                try
                {
                    clientNostroBank.SetPropertyCreate();
                    db.ClientNostroBank.Add(clientNostroBank);
                    db.SaveChanges();
                    return RedirectToAction("ClientNostroBank");
                }
                catch (System.Exception e)
                {
                    WarningMessagesAdd(e.MessageToList());
                }

            }
            
            var client = db.CLIENT.Find(clientNostroBank.Client);
            if (client == null)
            {
                return HttpNotFound();
            }
            clientNostroBank.Client = client.Id;
            clientNostroBank.CLIENT1 = client;
            return View(clientNostroBank);

        }

        public ActionResult ClientNostroBankDetails(int? client, string clientNostroBankId)
        {
            if (client == null || clientNostroBankId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientNostroBank clientNostroBank = db.ClientNostroBank.Find(clientNostroBankId,client);
            if (clientNostroBank == null)
            {
                return HttpNotFound();
            }
            return View(clientNostroBank);
        }
        public ActionResult ClientNostroBankEdit(int? client, string clientNostroBankId)
        {
            if (client == null || clientNostroBankId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientNostroBank clientNostroBank = db.ClientNostroBank.Find(clientNostroBankId, client);
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
        public ActionResult ClientNostroBankEdit([Bind(Include = "ClientNostroBankId,Client,BankId,BankBranchCode,NostroBankName,NostroAccountNumber,NostroAccountName,BankCategory,Remark,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] ClientNostroBank clientNostroBank)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientNostroBank).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ClientNostroBank");
            }
            ViewBag.Client = new SelectList(db.CLIENT, "Id", "ClientId", clientNostroBank.Client);
            return View(clientNostroBank);
        }

        // GET: ClientNostroBanks/Delete/5
        public ActionResult ClientNostroBankDelete(string id)
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
        [HttpPost, ActionName("ClientNostroBankDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ClientNostroBank clientNostroBank = db.ClientNostroBank.Find(id);
            db.ClientNostroBank.Remove(clientNostroBank);
            db.SaveChanges();
            return RedirectToAction("ClientNostroBank");
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
