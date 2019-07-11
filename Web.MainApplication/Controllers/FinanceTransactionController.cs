using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Repository.Application.DataModel;

namespace Web.MainApplication.Controllers
{
    public class FinanceTransactionController : BaseController
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();

        public ActionResult Deposit(long? contractId)
        {
            if (contractId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTRACT contract = db.CONTRACT.Find(contractId);
            if (contract == null)
            {
                return HttpNotFound();
            }

            var financeTransactionDeposit = db.FinanceTransaction.Where(x => x.ContractId == contract.ContractId && x.TransactionCode == "DEP").FirstOrDefault();
            if (financeTransactionDeposit != null)
            {
                WarningMessagesAdd("This contract has been deposit with finance transaction code \"" + financeTransactionDeposit.TransactionNumber + "\"");
            }

            //var contractUser = db.UserClientMapping.Where(x => x.ClientId == contract.ClientIdPK).FirstOrDefault();
            //if (contractUser.UserName != User.Identity.Username())
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            var financeTransaction = new FinanceTransaction();
            financeTransaction.CONTRACT = contract;
            var lastFinanceTransaction = db.FinanceTransaction.OrderByDescending(x => x.Id).FirstOrDefault();
            financeTransaction.TransactionNumber = WebAppUtility.FinanceTransactionNumberGenerator(lastFinanceTransaction != null ? lastFinanceTransaction.Id + 1 : 1);
            financeTransaction.Amount = 0;
            financeTransaction.Date = DateTime.Now;
            var lsliNostroBank = new List<SelectListItem>();
            lsliNostroBank.AddBlank();
            db.NostroBank.ToList().ForEach(x =>
            {
                lsliNostroBank.AddItemValText(x.NostroBankId, x.NostroBankName);
            });
            ViewBag.NostroBank = lsliNostroBank.ToSelectList();
            return View(financeTransaction);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deposit([Bind(Include = "Id,TransactionNumber,ContractId,Amount,TransactionCode,ValueDate,DueDate,Date,Remark,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive,ReferenceNumber")] FinanceTransaction financeTransaction)
        {
            var contractId = Convert.ToInt64(Request.Params["ContractId"]);

            CONTRACT contract = db.CONTRACT.Find(contractId);
            if (contract == null)
            {
                return HttpNotFound();
            }
            //var contractUser = db.UserClientMapping.Where(x => x.ClientId == contract.ClientIdPK).FirstOrDefault();
            //if (contractUser.UserName != User.Identity.Username())
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            if (financeTransaction.ValueDate < contract.StartPeriode || financeTransaction.ValueDate > contract.EndPeriode)
            {
                WarningMessagesAdd("Transaction Value Date Must Be Within Start And End Periode Of Contract. Start Periode is " + contract.StartPeriode.Value.ToString("dd/MM/yyyy") + " and End Periode is " + contract.EndPeriode.Value.ToString("dd/MM/yyyy") + ".");
            }
            var financeTransactionDeposit = db.FinanceTransaction.Where(x => x.ContractId == contract.ContractId && x.TransactionCode == "DEP").FirstOrDefault();
            if (financeTransactionDeposit != null)
            {
                WarningMessagesAdd("This contract has been deposit with finance transaction code \"" + financeTransactionDeposit.TransactionNumber + "\"");
            }
            if (Request.Params["NostroBank"] == null)
            {
                WarningMessagesAdd("Nostro Bank Can Not Be Empty");
            }

            if (financeTransaction.Amount > contract.Value)
            {
                WarningMessagesAdd("Amount can not be bigger than contract value");
            }

            if (ModelState.IsValid && WarningMessages().Count() == 0)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        financeTransaction.TransactionCode = "DEP";
                        financeTransaction.SetPropertyCreate();
                        db.FinanceTransaction.Add(financeTransaction);
                        var financeBalance = db.FinanceBalance.Find(financeTransaction.ContractId);
                        financeBalance.ActualAmount = financeBalance.ActualAmount + financeTransaction.Amount;
                        financeBalance.SetPropertyUpdate();
                        db.Entry(financeBalance).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        // add data to financetransactionnostro
                        var ftNostro = new FinanceTransactionNostro();
                        ftNostro.FinanceTransactionId = financeTransaction.Id;
                        ftNostro.NostroBankId = Request.Params["NostroBank"];
                        ftNostro.SetPropertyCreate();
                        db.FinanceTransactionNostro.Add(ftNostro);

                        // add data to financetransactionclientnostro
                        var ftClientNostro = new FinanceTransactionClientNostro();
                        ftClientNostro.Client = contract.ClientIdPK;
                        ftClientNostro.ClientNostroBankId = contract.CLIENT.ClientNostroBank.FirstOrDefault().ClientNostroBankId;
                        ftClientNostro.FinanceTransactionId = financeTransaction.Id;
                        db.FinanceTransactionClientNostro.Add(ftClientNostro);
                        db.SaveChanges();
                        transaction.Commit();
                        SuccessMessagesAdd("Inserting Deposit " + financeTransaction.TransactionNumber + " Success");
                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        ErrorMessagesAdd(e.MessageToList());
                        transaction.Rollback();
                    }

                }
            }

            financeTransaction.CONTRACT = contract;
            var lastFinanceTransaction = db.FinanceTransaction.OrderByDescending(x => x.Id).FirstOrDefault();
            financeTransaction.TransactionNumber = WebAppUtility.FinanceTransactionNumberGenerator(lastFinanceTransaction != null ? lastFinanceTransaction.Id + 1 : 1);
            financeTransaction.Amount = 0;

            var lsliNostroBank = new List<SelectListItem>();
            lsliNostroBank.AddBlank();
            db.NostroBank.ToList().ForEach(x =>
            {
                lsliNostroBank.AddItemValText(x.NostroBankId, x.NostroBankName);
            });
            ViewBag.NostroBank = lsliNostroBank.ToSelectList(Request.Params["NostroBank"]);
            return View(financeTransaction);
        }




        // GET: FinanceTransaction
        public ActionResult Index()
        {
            var financeTransaction = db.FinanceTransaction.Include(f => f.CONTRACT);
            return View(financeTransaction.ToList());
        }

        // GET: FinanceTransaction/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinanceTransaction financeTransaction = db.FinanceTransaction.Find(id);
            if (financeTransaction == null)
            {
                return HttpNotFound();
            }
            return View(financeTransaction);
        }

        // GET: FinanceTransaction/Create
        public ActionResult Create()
        {
            ViewBag.ContractId = new SelectList(db.CONTRACT, "ContractId", "ContractNumber");
            return View();
        }


        // POST: FinanceTransaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TransactionNumber,ContractId,Amount,TransactionCode,ValueDate,DueDate,Date,Remark,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] FinanceTransaction financeTransaction)
        {
            if (ModelState.IsValid)
            {
                db.FinanceTransaction.Add(financeTransaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContractId = new SelectList(db.CONTRACT, "ContractId", "ContractNumber", financeTransaction.ContractId);
            return View(financeTransaction);
        }

        // GET: FinanceTransaction/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinanceTransaction financeTransaction = db.FinanceTransaction.Find(id);
            if (financeTransaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContractId = new SelectList(db.CONTRACT, "ContractId", "ContractNumber", financeTransaction.ContractId);
            return View(financeTransaction);
        }

        // POST: FinanceTransaction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TransactionNumber,ContractId,Amount,TransactionCode,ValueDate,DueDate,Date,Remark,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] FinanceTransaction financeTransaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(financeTransaction).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContractId = new SelectList(db.CONTRACT, "ContractId", "ContractNumber", financeTransaction.ContractId);
            return View(financeTransaction);
        }

        // GET: FinanceTransaction/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinanceTransaction financeTransaction = db.FinanceTransaction.Find(id);
            if (financeTransaction == null)
            {
                return HttpNotFound();
            }
            return View(financeTransaction);
        }

        // POST: FinanceTransaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            FinanceTransaction financeTransaction = db.FinanceTransaction.Find(id);
            db.FinanceTransaction.Remove(financeTransaction);
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
