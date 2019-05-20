using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using Repository.Application.DataModel;
using Web.MainApplication.ReportModels;

namespace Web.MainApplication.Controllers
{
    public class DeliveryRequestController : BaseController
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();

        // GET: DeliveryRequest
        public ActionResult Index()
        {
            var deliveryRequest = db.DeliveryRequest.Include(d => d.CONTRACT);
            return View(deliveryRequest.ToList());
        }
        // GET: DeliveryRequest
        public ActionResult IndexClient()
        {
            var userClient = User.Identity.Name;
            var deliveryRequest = db.DeliveryRequest.Join(db.CONTRACT, dr => dr.ContractId, c => c.ContractId, (dr, c) => new { CONTRACT = c, DeliveryRequest = dr })
          .Join(db.CLIENT, contract => contract.CONTRACT.ClientIdPK, client => client.Id, (contractDr, client) => new { DeliveryRequest = contractDr.DeliveryRequest, CLIENT = client })
          .Join(db.UserClientMapping.Where(a => a.UserName == userClient), client => client.CLIENT.Id, uCM => uCM.ClientId, (CLIENT, uCM) => CLIENT.DeliveryRequest);
            return View(deliveryRequest.ToList());
        }
        //Print Delivery Request
        public ActionResult PrintDeliveryRequest(long? id)
        {
            ReportDocument rd = new ReportDocument();
            var listBatch = new List<BATCH>();
            listBatch.Add(new BATCH()
            {
                BatchCode = "xx1",
                BatchName = "xx1",
                PRODUCT = new PRODUCT(),
                RawProduct = "xx",
                UpdatedBy = "xx",
                CreatedBy = "xx",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsActive = 1
            });
            rd.Load(Path.Combine(Server.MapPath("~/Reports/CrystalReport1.rpt")));
            var ds = listBatch.Select(x => new
            {

                BatchCode = "xx1",
                BatchName = "xx1",
                PRODUCT = new PRODUCT(),
                RawProduct = "xx",
                UpdatedBy = "xx",
                CreatedBy = "xx",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsActive = 1
            });
            var dsFirst = ds;
            rd.SetDataSource(dsFirst);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream streamReport = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            streamReport.Seek(0, SeekOrigin.Begin);

            Response.AddHeader("content-disposition", "inline;filename=" + "DeliveryRequest");
            var file = File(streamReport, "application/pdf");



            return file;
            //var deliveryRequest = db.DeliveryRequest.Find(id);
            //var deliveryRequestModelReport = new ReportModels.DeliveryRequests();
            //deliveryRequestModelReport.DeliveryRequestNumber = deliveryRequest.DeliveryRequestNumber;
            //deliveryRequestModelReport.ContractId = deliveryRequest.ContractId.Value;
            //deliveryRequestModelReport.ContractNumber = deliveryRequest.CONTRACT.ContractNumber;
            //deliveryRequestModelReport.DeliveryRequestTime = deliveryRequest.DeliveryRequestTime.Value;
            //List<DeliveryRequests> lds = new List<DeliveryRequests>();
            //lds.Add(deliveryRequestModelReport);
            //var ds = new
            //{
            //    DeliveryRequestNumber = deliveryRequestModelReport.DeliveryRequestNumber,
            //    ContractId = deliveryRequestModelReport.ContractId,
            //    ContractNumber = deliveryRequestModelReport.ContractNumber,
            //    DeliveryRequestTime = deliveryRequestModelReport.DeliveryRequestTime
            //};
            //using (ReportDocument rd = new ReportDocument())
            //{
            //    rd.Load(Path.Combine(Server.MapPath("~/Reports/DeliveryRequestToClient.rpt")));
            //    rd.SetDataSource(lds);
            //    Response.Buffer = false;
            //    Response.ClearContent();
            //    Response.ClearHeaders();
            //    using (Stream streamReport = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat))
            //    {

            //        streamReport.Seek(0, SeekOrigin.Begin);

            //        Response.AddHeader("content-disposition", "inline;filename=" + "DeliveryRequest");
            //        var file = File(streamReport, "application/pdf");

            //        return file;
            //    }

            //}




        }

        // GET: DeliveryRequest/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryRequest deliveryRequest = db.DeliveryRequest.Find(id);
            if (deliveryRequest == null)
            {
                return HttpNotFound();
            }
            return View(deliveryRequest);
        }

        // GET: DeliveryRequest/Create
        public ActionResult Create()
        {
            ViewBag.ContractId = new SelectList(db.CONTRACT, "ContractId", "ContractNumber");
            return View();
        }

        // POST: DeliveryRequest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeliveryRequestId,DeliveryRequestNumber,ContractId,DeliveryRequestTime,TokenToLoad,IsActiveToken,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] DeliveryRequest deliveryRequest)
        {
            if (ModelState.IsValid)
            {
                db.DeliveryRequest.Add(deliveryRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContractId = new SelectList(db.CONTRACT, "ContractId", "ContractNumber", deliveryRequest.ContractId);
            return View(deliveryRequest);
        }

        // GET: DeliveryRequest/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryRequest deliveryRequest = db.DeliveryRequest.Find(id);
            if (deliveryRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContractId = new SelectList(db.CONTRACT, "ContractId", "ContractNumber", deliveryRequest.ContractId);
            return View(deliveryRequest);
        }

        // POST: DeliveryRequest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeliveryRequestId,DeliveryRequestNumber,ContractId,DeliveryRequestTime,TokenToLoad,IsActiveToken,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] DeliveryRequest deliveryRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deliveryRequest).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContractId = new SelectList(db.CONTRACT, "ContractId", "ContractNumber", deliveryRequest.ContractId);
            return View(deliveryRequest);
        }

        // GET: DeliveryRequest/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryRequest deliveryRequest = db.DeliveryRequest.Find(id);
            if (deliveryRequest == null)
            {
                return HttpNotFound();
            }

            return View(deliveryRequest);
        }

        // POST: DeliveryRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            DeliveryRequest deliveryRequest = db.DeliveryRequest.Find(id);
            db.DeliveryRequest.Remove(deliveryRequest);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: DeliveryRequest/Create
        public ActionResult CreateDeliveryRequestClient(long? contractId)
        {
            InfoMessagesAdd("Expired Date Of Delivery Request Is 7 Days.");
            InfoMessagesAdd("You Can Only Include One Product.");
            if (contractId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userMapping = db.UserClientMapping.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            if (userMapping == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
            var contract = db.CONTRACT.Find(contractId);
            if (contract == null)
            {
                return HttpNotFound();
            }
            if (contract.ClientIdPK != userMapping.ClientId)
            {
                new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
            var newDeliveryRequest = new DeliveryRequest();
            newDeliveryRequest.ContractId = contractId;
            var lastDR = db.DeliveryRequest.OrderByDescending(x => x.DeliveryRequestId).FirstOrDefault();
            newDeliveryRequest.DeliveryRequestNumber = WebAppUtility.DeliveryRequestNumberGenerator(lastDR == null ? 1 : (lastDR.DeliveryRequestId + 1));
            newDeliveryRequest.CONTRACT = contract;


            foreach (var item in newDeliveryRequest.CONTRACT.ContractProduct)
            {
                newDeliveryRequest.DeliveryRequestProductDetailTransaction.Add(new DeliveryRequestProductDetailTransaction()
                {
                    ProductId = item.ProductId,
                    PRODUCT = item.PRODUCT,
                    Amount = 0
                });
            }
            var listOutStandingDR = contract.DeliveryRequest.Where(x => (x.DeliveryRequestTime.Value.AddDays(contract.MaxExpiredDR.Value)) >= DateTime.Now).ToList();
            ViewBag.ListOutStandingDR = listOutStandingDR;
            ViewBag.tonUnitToM3Unit = db.ParamSetupTonToM3();
            return View(newDeliveryRequest);
        }

        // POST: DeliveryRequest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDeliveryRequestClient([Bind(Include = "DeliveryRequestId,DeliveryRequestNumber,ContractId,DeliveryRequestTime,TokenToLoad,IsActiveToken,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] DeliveryRequest deliveryRequest)
        {
            CONTRACT contract = db.CONTRACT.Find(deliveryRequest.ContractId);
            var listDeliveryRequestProductDetailTransaction = new List<DeliveryRequestProductDetailTransaction>();
            foreach (var item in contract.ContractProduct)
            {
                listDeliveryRequestProductDetailTransaction.Add(new DeliveryRequestProductDetailTransaction()
                {
                    DeliveryRequestId = deliveryRequest.DeliveryRequestId,
                    ProductId = Request.Params[item.ProductId],
                    Amount = Convert.ToDouble(Request.Params[item.ProductId + "Amount"]),
                    DriverName = Request.Params[item.ProductId + "DriverName"],
                    ArmadaPlatNumber = Request.Params[item.ProductId + "ArmadaPlatNumber"]
                });
            }
            if (contract == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (listDeliveryRequestProductDetailTransaction.Where(x => x.ProductId != null).Count() == 0)
            {
                WarningMessagesAdd("Select At Least 1 Product");
            }
            else if (listDeliveryRequestProductDetailTransaction.Where(x => x.ProductId != null).Count() > 1)
            {
                WarningMessagesAdd("You Can Only Include One Product.");
            }
            else if (listDeliveryRequestProductDetailTransaction.Where(x => x.ProductId != null).Count() == 1)
            {
                var productId = listDeliveryRequestProductDetailTransaction.Where(z => z.ProductId != null).FirstOrDefault().ProductId;
                var contractProduct = contract.ContractProduct.Where(x => x.ContractId == contract.ContractId && x.ProductId == productId).FirstOrDefault();
                if ((contract.FinanceBalance.ActualAmount + contract.FinanceBalance.AvailableAmount) < ((decimal)listDeliveryRequestProductDetailTransaction.Where(z => z.ProductId != null).FirstOrDefault().Amount * contractProduct.PricePerTon))
                {
                    WarningMessagesAdd("Value Of Delivery Request Can Not Be Greater Than Balance Remaining. <br/>" +
                        "Your balance Remaining = " + (contract.FinanceBalance.ActualAmount + contract.FinanceBalance.AvailableAmount) + "<br/>" +
                        "Delivery Request Value = " + ((decimal)listDeliveryRequestProductDetailTransaction.Where(z => z.ProductId != null).FirstOrDefault().Amount * contractProduct.PricePerTon));
                }
            }

            if (listDeliveryRequestProductDetailTransaction.Sum(x => x.Amount) <= 0)
            {
                WarningMessagesAdd("Total Amount Must Be Greater Than 0");
            }
            if (listDeliveryRequestProductDetailTransaction.Where(x => x.Amount <= 0 && x.ProductId != null).FirstOrDefault() != null)
            {
                WarningMessagesAdd("Total Amount Must Be Greater Than 0");
            }

            if (ModelState.IsValid && WarningMessages().Count == 0)
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        deliveryRequest.SetPropertyCreate();
                        db.DeliveryRequest.Add(deliveryRequest);

                        db.SaveChanges();
                        foreach (var item in listDeliveryRequestProductDetailTransaction.Where(x => x.ProductId != null))
                        {
                            var itemToInsert = new DeliveryRequestProductDetailTransaction();
                            itemToInsert.Amount = item.Amount;
                            itemToInsert.ArmadaPlatNumber = item.ArmadaPlatNumber;
                            itemToInsert.DeliveryRequestId = deliveryRequest.DeliveryRequestId;
                            itemToInsert.ProductId = item.ProductId;
                            itemToInsert.DriverName = item.DriverName;
                            itemToInsert.SetPropertyCreate();
                            db.DeliveryRequestProductDetailTransaction.Add(itemToInsert);
                        }
                        db.SaveChanges();
                        transaction.Commit();

                        SuccessMessagesAdd("Delivery Request Created Succesfully.");
                        return RedirectToAction("IndexClient");
                    }
                    catch (Exception e)
                    {
                        WarningMessagesAdd(e.Message);
                        transaction.Rollback();
                    }
                }

            }

            deliveryRequest.CONTRACT = contract;
            foreach (var item in deliveryRequest.CONTRACT.ContractProduct)
            {
                deliveryRequest.DeliveryRequestProductDetailTransaction.Add(new DeliveryRequestProductDetailTransaction()
                {
                    ProductId = item.ProductId,
                    PRODUCT = item.PRODUCT,
                    Amount = Convert.ToDouble(Request.Params[item.ProductId + "Amount"]),
                    DriverName = Request.Params[item.ProductId + "DriverName"],
                    ArmadaPlatNumber = Request.Params[item.ProductId + "ArmadaPlatNumber"]
                });
            }
            InfoMessagesAdd("Expired Date Of Delivery Request Is 7 Days.");
            InfoMessagesAdd("You Can Only Include One Product.");
            var listOutStandingDR = contract.DeliveryRequest.Where(x => (x.DeliveryRequestTime.Value.AddDays(contract.MaxExpiredDR.Value)) >= DateTime.Now).ToList();
            ViewBag.ListOutStandingDR = listOutStandingDR;
            return View(deliveryRequest);
        }

        public ActionResult VerifyDeliveryRequest(long? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryRequest deliveryRequest = db.DeliveryRequest.Find(id);
            if (deliveryRequest == null)
            {
                return HttpNotFound();
            }
            var sliStatusDR = new List<SelectListItem>();
            sliStatusDR.AddBlank();
            sliStatusDR.AddItemValText("Loading Material", "Load Material");
            sliStatusDR.AddItemValText("Finish Loading", "Finish Loading");
            ViewBag.Status = sliStatusDR.ToSelectList(deliveryRequest.Status);

            return View(deliveryRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VerifyDeliveryRequest(long? id, string status)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryRequest deliveryRequest = db.DeliveryRequest.Find(id);
            if (deliveryRequest == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {

                deliveryRequest.Status = status;

                if (deliveryRequest.Status == "Finish Loading")
                {
                    deliveryRequest.SetPropertyUpdate();
                    db.Entry(deliveryRequest).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    SuccessMessagesAdd("Delivery Request \"" + deliveryRequest.DeliveryRequestNumber + "\" Status Has Been Updated To \"" + status + "\"");
                    SuccessMessagesAdd("Invoice For Delivery Request \"" + deliveryRequest.DeliveryRequestNumber + "\" Status Has Been Generated With Invoice Number \"" + status + "\"");
                    return RedirectToAction("Index");
                }
                else
                {
                    deliveryRequest.SetPropertyUpdate();
                    db.Entry(deliveryRequest).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    SuccessMessagesAdd("Delivery Request \"" + deliveryRequest.DeliveryRequestNumber + "\" Status Has Been Updated To \"" + status + "\"");
                    return RedirectToAction("Index");
                }
            }

            var sliStatusDR = new List<SelectListItem>();
            sliStatusDR.AddBlank();
            sliStatusDR.AddItemValText("Loading Material", "Load Material");
            sliStatusDR.AddItemValText("Finish Loading", "Finish Loading");
            ViewBag.Status = sliStatusDR.ToSelectList();

            return View(deliveryRequest);
        }

        [HttpGet]
        public ActionResult CreateDeliveryOrder(long? idDeliveryRequest)
        {


            if (idDeliveryRequest == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryRequest deliveryRequest = db.DeliveryRequest.Find(idDeliveryRequest);
            if (deliveryRequest == null)
            {
                return HttpNotFound();
            }
            var deliveryOrder = new DeliveryOrder();
            deliveryOrder.DeliveryRequestId = deliveryRequest.DeliveryRequestId;
            ViewBag.DeliveryRequest = deliveryRequest;
            deliveryOrder.Amount = 0;
            deliveryOrder.DeliveryOrderNumber = WebAppUtility.DeliveryOrderNumberGenerator(db.DeliveryOrderSequence() + 1);
            return View(deliveryOrder);
        }
        [HttpPost]
        public ActionResult CreateDeliveryOrder(DeliveryOrder deliveryOrder)
        {


            if (deliveryOrder.DeliveryRequestId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryRequest deliveryRequest = db.DeliveryRequest.Find(deliveryOrder.DeliveryRequestId);
            if (deliveryRequest == null)
            {
                return HttpNotFound();
            }
            if (deliveryOrder.Amount <= 0)
            {
                WarningMessagesAdd("Amount Can Not Be 0 or Less.");
            }

            if (ModelState.IsValid && WarningMessages().Count == 0)
            {
                using (var dbTransaction = db.Database.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        deliveryOrder.SetPropertyCreate();
                        db.DeliveryOrder.Add(deliveryOrder);
                        db.SaveChanges();

                        var deliveryOrderInvoice = new DeliveryOrderInvoice();
                        deliveryOrderInvoice.DeliveryOrderInvoiceNumber = WebAppUtility.DeliveryOrderInvoiceNumberGenerator(db.DeliveryOrderInvoiceSequence() + 1);
                        deliveryOrderInvoice.DeliveryOrderNumber = deliveryOrder.DeliveryOrderNumber;
                        foreach (var item in deliveryRequest.DeliveryRequestProductDetailTransaction)
                        {
                            deliveryOrderInvoice.Amount += (Decimal)item.Amount.Value * deliveryRequest.CONTRACT.ContractProduct.Where(x => x.ProductId == item.ProductId).FirstOrDefault().PricePerTon.Value;
                        }
                        deliveryOrderInvoice.SetPropertyCreate();
                        db.DeliveryOrderInvoice.Add(deliveryOrderInvoice);
                        db.SaveChanges();
                        SuccessMessagesAdd("Delivery Order \"" + deliveryOrder.DeliveryOrderNumber + "\" Has Been Created.");
                        SuccessMessagesAdd("Delivery Order Invoice \"" + deliveryOrderInvoice.DeliveryOrderInvoiceNumber + "\" Has Been Created.");
                        dbTransaction.Commit();
                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        dbTransaction.Rollback();
                        ErrorMessagesAdd(e.Message);
                    }


                }
            }
            return View(deliveryOrder);
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
