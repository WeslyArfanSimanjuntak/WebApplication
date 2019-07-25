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
    public class RitaseController : BaseController
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();

        // GET: Ritase
        public ActionResult Index()
        {
            var rITASE = db.RITASE.Include(r => r.ARMADA1).Include(r => r.EMPLOYEE).Include(r => r.PRODUCT1).Include(r => r.SITE1);
            return View(rITASE.ToList());
        }

        // GET: Ritase/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RITASE rITASE = db.RITASE.Find(id);
            if (rITASE == null)
            {
                return HttpNotFound();
            }
            return View(rITASE);
        }

        // GET: Ritase/Create
        public ActionResult Create()
        {
            var lastRitase = db.RITASE.OrderByDescending(x => x.RITASEID).FirstOrDefault();
            RITASE ritase = new RITASE
            {
                RITASENUMBER = "[Auto Generated]",
                //RITASENUMBER = WebAppUtility.RitaseNumberGenerator(lastRitase != null ? lastRitase.RITASEID + 1 : 1),
                QuantityInTon = 0

            };
            var listArmada = new List<SelectListItem>();
            listArmada.AddBlank();
            db.ARMADA.ToList().ForEach(x =>
            {
                listArmada.AddItemValText(x.ARMADAID, x.ARMADAID + " - " + x.ARMADANAME + " - " + x.ARMADACAPACITYINKG.GetValueOrDefault().ToString() + " kg");
            });
            ViewBag.ARMADA = listArmada.ToSelectList();
            var selectListDriverArmada = new List<SelectListItem>();
            selectListDriverArmada.AddBlank();
            db.EMPLOYEE.OrderBy(x => x.EMPLOYEENAME).ToList().ForEach(x =>
            {
                selectListDriverArmada.AddItemValText(x.EMPLOYEEID, x.EMPLOYEENAME);
            });
            ViewBag.DRIVERARMADA = selectListDriverArmada.ToSelectList();
            var selectListProduct = new List<SelectListItem>();
            selectListProduct.AddBlank();
            db.PRODUCT.Where(x => x.TypeMaterial == "Raw").OrderBy(x => x.PRODUCTNAME).ToList().ForEach(x =>
              {
                  selectListProduct.AddItemValText(x.PRODUCTID, x.PRODUCTNAME);
              });
            ViewBag.PRODUCT = selectListProduct.ToSelectList();
            var selectListSite = new List<SelectListItem>();
            selectListSite.AddBlank();
            db.SITE.OrderBy(x => x.SITENAME).ToList().ForEach(x =>
            {
                selectListSite.AddItemValText(x.SITENAME, x.SITENAME + " - " + x.SITEADDRESS);
            });
            ViewBag.SITE = selectListSite.ToSelectList();

            var selectListStatusRitase = new List<SelectListItem>();
            selectListStatusRitase.AddBlank();
            selectListStatusRitase.AddItemValText("Process", "Process");
            selectListStatusRitase.AddItemValText("Accumulate", "Acumulate/Not Process");
            ViewBag.RITASESTATUS = selectListStatusRitase.ToSelectList();
            var selectListBatchCode = new List<SelectListItem>();
            selectListBatchCode.AddBlank();
            db.BATCH.OrderBy(x => x.BatchCode).ToList().ForEach(x =>
            {
                selectListBatchCode.AddItemValText(x.BatchCode, x.BatchCode + " - " + x.BatchName);
            });
            ViewBag.BatchCode = selectListBatchCode.ToSelectList();

            var lsliSource = new List<SelectListItem>();
            lsliSource.AddBlank();
            db.SOURCE.Where(x => x.IsActive == 1).ToList().ForEach(z =>
            {
                lsliSource.AddItemValText(z.Id.ToString(), z.Name);
            });
            ViewBag.SOURCEID = lsliSource.ToSelectList();
            float convertTonToM3;
            float.TryParse((string)db.ParameterSetup.Where(x => x.Name == "Convertion(ton to m3)").FirstOrDefault().Value, out convertTonToM3);
            ViewBag.TonUnitToM3Unit = convertTonToM3;
            return View(ritase);
        }

        // POST: Ritase/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RITASEID,RITASENUMBER,SITE,PRODUCT,DRIVERARMADA,ARMADA,RITASETIME,RITASESTATUS,BatchCode,QuantityInTon,SOURCEID")] RITASE rITASE)
        {
            if (rITASE.QuantityInTon <= 0)
            {
                WarningMessagesAdd("Quantity Must Be Greater Than \"0\"");
            }

            if (ModelState.IsValid && WarningMessages().Count == 0)
            {
                if (rITASE.RITASESTATUS == "Process")
                {
                    using (var dbTransaction = db.Database.BeginTransaction())
                    {

                        try
                        {
                            rITASE.RITASENUMBER = WebAppUtility.RitaseNumberGenerator(db.RitaseSequence() + 1);
                            var batchProduct = db.BatchProduct.Where(x => x.BatchCode == rITASE.BatchCode);
                            List<TransactionProduct> listTransactionProduct = new List<TransactionProduct>();
                            foreach (var item in batchProduct)
                            {
                                listTransactionProduct.Add(new TransactionProduct()
                                {
                                    ProductId = item.ProductId,
                                    Jenis = "Ritase",
                                    TypeDebitOrCredit = "Kredit",
                                    Ammount = item.ProductPercentage / 100 * rITASE.QuantityInTon

                                });
                            }
                            rITASE.SetPropertyCreate();
                            db.RITASE.Add(rITASE);
                            db.SaveChanges();

                            listTransactionProduct.ForEach(x =>
                            {
                                var lstTP = db.TransactionProduct.OrderByDescending(lp => lp.Id).FirstOrDefault();
                                x.TransactionProductNumber = WebAppUtility.TransactionProductNumberGenerator(lstTP != null ? lstTP.Id + 1 : 0 + 1);
                                x.RitaseId = rITASE.RITASEID;
                                x.SetPropertyCreate();
                                var productSite = db.ProductSite.Where(z => x.ProductId == z.ProductId && z.SiteName == rITASE.SITE).FirstOrDefault();

                                if (productSite != null)
                                {
                                    productSite.TotalStock = productSite.TotalStock + x.Ammount;
                                    productSite.SetPropertyUpdate();
                                    db.Entry(productSite).State = System.Data.Entity.EntityState.Modified;
                                }
                                else
                                {
                                    productSite = new ProductSite()
                                    {
                                        SiteName = rITASE.SITE,
                                        ProductId = x.ProductId,
                                        TotalStock = x.Ammount
                                    };
                                    productSite.SetPropertyCreate();
                                    db.ProductSite.Add(productSite);
                                }

                                db.TransactionProduct.Add(x);
                                db.SaveChanges();
                            });

                            dbTransaction.Commit();
                            SuccessMessagesAdd("Ritase Success Inserted.");
                            return RedirectToAction("Index");
                        }
                        catch (Exception e)
                        {
                            dbTransaction.Rollback();
                            WarningMessagesAdd(e.Message);
                        }
                    }
                }
                else if (rITASE.RITASESTATUS == "Accumulate")
                {
                    using (var dbTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var lastSequence = db.TableSequence.Find("Ritase", 2019);
                            var lastRitase = db.RITASE.OrderByDescending(x => x.RITASEID).FirstOrDefault();

                            rITASE.RITASENUMBER = WebAppUtility.RitaseNumberGenerator(db.RitaseSequence() + 1);
                            db.RitaseSequencePlusOne();
                            
                            var product = db.PRODUCT.Where(x => x.PRODUCTID == rITASE.PRODUCT);
                            List<TransactionProduct> listTransactionProduct = new List<TransactionProduct>();
                            foreach (var item in product)
                            {
                                var lastTP = db.TransactionProduct.OrderByDescending(x => x.Id).FirstOrDefault();
                                listTransactionProduct.Add(new TransactionProduct()
                                {
                                    TransactionProductNumber = WebAppUtility.TransactionProductNumberGenerator(lastTP != null ? lastTP.Id + 1 : 0 + 1),
                                    ProductId = item.PRODUCTID,
                                    Jenis = "Ritase",
                                    RitaseId = rITASE.RITASEID,
                                    TypeDebitOrCredit = "Kredit",
                                    Ammount = rITASE.QuantityInTon

                                });

                                var productSite = db.ProductSite.Where(z => z.ProductId == rITASE.PRODUCT && z.SiteName == rITASE.SITE).FirstOrDefault();

                                if (productSite == null)
                                {
                                    productSite = new ProductSite()
                                    {
                                        SiteName = rITASE.SITE,
                                        ProductId = rITASE.PRODUCT,
                                        TotalStock = rITASE.QuantityInTon
                                    };
                                    productSite.SetPropertyCreate();
                                    db.ProductSite.Add(productSite);
                                }
                                else
                                {

                                    productSite.TotalStock = productSite.TotalStock + rITASE.QuantityInTon;
                                    productSite.SetPropertyUpdate();
                                    db.Entry(productSite).State = System.Data.Entity.EntityState.Modified;
                                }

                            }

                            db.RITASE.Add(rITASE);
                            db.TransactionProduct.AddRange(listTransactionProduct);
                            db.SaveChanges();
                            dbTransaction.Commit();
                            SuccessMessagesAdd("Ritase Success Inserted.");
                            return RedirectToAction("Index");

                        }
                        catch (Exception e)
                        {
                            dbTransaction.Rollback();
                            ErrorMessagesAdd(e.Message);
                            throw;
                        }
                    }
                }


            }

            var listArmada = new List<SelectListItem>();
            listArmada.AddBlank();
            db.ARMADA.ToList().ForEach(x =>
            {
                listArmada.AddItemValText(x.ARMADAID, x.ARMADAID + " - " + x.ARMADANAME + " - " + x.ARMADACAPACITYINKG.GetValueOrDefault().ToString() + " kg");
            });
            ViewBag.ARMADA = listArmada.ToSelectList(rITASE.ARMADA);
            var selectListDriverArmada = new List<SelectListItem>();
            selectListDriverArmada.AddBlank();
            db.EMPLOYEE.OrderBy(x => x.EMPLOYEENAME).ToList().ForEach(x =>
            {
                selectListDriverArmada.AddItemValText(x.EMPLOYEEID, x.EMPLOYEENAME);
            });
            ViewBag.DRIVERARMADA = selectListDriverArmada.ToSelectList(rITASE.DRIVERARMADA);
            var selectListProduct = new List<SelectListItem>();
            selectListProduct.AddBlank();
            db.PRODUCT.Where(x => x.TypeMaterial == "Raw").OrderBy(x => x.PRODUCTNAME).ToList().ForEach(x =>
            {
                selectListProduct.AddItemValText(x.PRODUCTID, x.PRODUCTNAME);
            });
            ViewBag.PRODUCT = selectListProduct.ToSelectList(rITASE.PRODUCT);
            var selectListSite = new List<SelectListItem>();
            selectListSite.AddBlank();
            db.SITE.OrderBy(x => x.SITENAME).ToList().ForEach(x =>
            {
                selectListSite.AddItemValText(x.SITENAME, x.SITENAME + " - " + x.SITEADDRESS);
            });
            ViewBag.SITE = selectListSite.ToSelectList(rITASE.SITE);
            var selectListStatusRitase = new List<SelectListItem>();
            selectListStatusRitase.AddBlank();
            selectListStatusRitase.AddItemValText("Process", "Process");
            selectListStatusRitase.AddItemValText("Accumulate", "Acumulate/Not Process");
            ViewBag.RITASESTATUS = selectListStatusRitase.ToSelectList(rITASE.RITASESTATUS);
            var selectListBatchCode = new List<SelectListItem>();
            selectListBatchCode.AddBlank();
            db.BATCH.OrderBy(x => x.BatchCode).ToList().ForEach(x =>
            {
                selectListBatchCode.AddItemValText(x.BatchCode, x.BatchCode + " - " + x.BatchName);
            });
            ViewBag.BatchCode = selectListBatchCode.ToSelectList(rITASE.BatchCode);
            var lsliSource = new List<SelectListItem>();
            lsliSource.AddBlank();
            db.SOURCE.Where(x => x.IsActive == 1).ToList().ForEach(z =>
            {
                lsliSource.AddItemValText(z.Id.ToString(), z.Name);
            });
            ViewBag.SOURCEID = lsliSource.ToSelectList(rITASE.SOURCEID);
            float convertTonToM3;
            float.TryParse((string)db.ParameterSetup.Where(x => x.Name == "Convertion(ton to m3)").FirstOrDefault().Value, out convertTonToM3);
            ViewBag.TonUnitToM3Unit = convertTonToM3;

            return View(rITASE);
        }

        //// GET: Ritase/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RITASE rITASE = db.RITASE.Find(id);
        //    if (rITASE == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    var listArmada = new List<SelectListItem>();
        //    listArmada.AddBlank();
        //    db.ARMADA.ToList().ForEach(x =>
        //    {
        //        listArmada.AddItemValText(x.ARMADAID, x.ARMADAID + " - " + x.ARMADANAME + " - " + x.ARMADACAPACITYINKG.GetValueOrDefault().ToString() + " kg");
        //    });
        //    ViewBag.ARMADA = listArmada.ToSelectList(rITASE.ARMADA);
        //    var selectListDriverArmada = new List<SelectListItem>().AddBlank();
        //    db.EMPLOYEE.OrderBy(x => x.EMPLOYEENAME).ToList().ForEach(x =>
        //    {
        //        selectListDriverArmada.AddItemValText(x.EMPLOYEEID, x.EMPLOYEENAME);
        //    });
        //    ViewBag.DRIVERARMADA = selectListDriverArmada.ToSelectList(rITASE.DRIVERARMADA);
        //    var selectListProduct = new List<SelectListItem>().AddBlank();
        //    db.PRODUCT.Where(x => x.TypeMaterial == "Raw").OrderBy(x => x.PRODUCTNAME).ToList().ForEach(x =>
        //    {
        //        selectListProduct.AddItemValText(x.PRODUCTID, x.PRODUCTNAME);
        //    });
        //    ViewBag.PRODUCT = selectListProduct.ToSelectList(rITASE.PRODUCT);
        //    var selectListSite = new List<SelectListItem>().AddBlank();
        //    db.SITE.OrderBy(x => x.SITENAME).ToList().ForEach(x =>
        //    {
        //        selectListSite.AddItemValText(x.SITENAME, x.SITENAME + " - " + x.SITEADDRESS);
        //    });
        //    ViewBag.SITE = selectListSite.ToSelectList(rITASE.SITE);
        //    var selectListStatusRitase = new List<SelectListItem>().AddBlank().AddItemValText("Process", "Process").AddItemValText("Accumulate", "Acumulate/Not Process");
        //    ViewBag.RITASESTATUS = selectListStatusRitase.ToSelectList(rITASE.RITASESTATUS);
        //    var selectListBatchCode = new List<SelectListItem>().AddBlank();
        //    db.BATCH.OrderBy(x => x.BatchCode).ToList().ForEach(x =>
        //    {
        //        selectListBatchCode.AddItemValText(x.BatchCode, x.BatchCode + " - " + x.BatchName);
        //    });
        //    ViewBag.BatchCode = selectListBatchCode.ToSelectList(rITASE.BatchCode);
        //    return View(rITASE);
        //}

        //// POST: Ritase/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "RITASEID,RITASENUMBER,SITE,PRODUCT,DRIVERARMADA,ARMADA,RITASETIME,RITASESTATUS")] RITASE rITASE)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(rITASE).State = System.Data.Entity.EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    var listArmada = new List<SelectListItem>();
        //    listArmada.AddBlank();
        //    db.ARMADA.ToList().ForEach(x =>
        //    {
        //        listArmada.AddItemValText(x.ARMADAID, x.ARMADAID + " - " + x.ARMADANAME + " - " + x.ARMADACAPACITYINKG.GetValueOrDefault().ToString() + " kg");
        //    });
        //    ViewBag.ARMADA = listArmada.ToSelectList(rITASE.ARMADA);
        //    var selectListDriverArmada = new List<SelectListItem>().AddBlank();
        //    db.EMPLOYEE.OrderBy(x => x.EMPLOYEENAME).ToList().ForEach(x =>
        //    {
        //        selectListDriverArmada.AddItemValText(x.EMPLOYEEID, x.EMPLOYEENAME);
        //    });
        //    ViewBag.DRIVERARMADA = selectListDriverArmada.ToSelectList(rITASE.DRIVERARMADA);
        //    var selectListProduct = new List<SelectListItem>().AddBlank();
        //    db.PRODUCT.Where(x => x.TypeMaterial == "Raw").OrderBy(x => x.PRODUCTNAME).ToList().ForEach(x =>
        //    {
        //        selectListProduct.AddItemValText(x.PRODUCTID, x.PRODUCTNAME);
        //    });
        //    ViewBag.PRODUCT = selectListProduct.ToSelectList(rITASE.PRODUCT);
        //    var selectListSite = new List<SelectListItem>().AddBlank();
        //    db.SITE.OrderBy(x => x.SITENAME).ToList().ForEach(x =>
        //    {
        //        selectListSite.AddItemValText(x.SITENAME, x.SITENAME + " - " + x.SITEADDRESS);
        //    });
        //    ViewBag.SITE = selectListSite.ToSelectList(rITASE.SITE);
        //    var selectListStatusRitase = new List<SelectListItem>().AddBlank().AddItemValText("Process", "Process").AddItemValText("Accumulate", "Acumulate/Not Process");
        //    ViewBag.RITASESTATUS = selectListStatusRitase.ToSelectList(rITASE.RITASESTATUS);
        //    var selectListBatchCode = new List<SelectListItem>().AddBlank();
        //    db.BATCH.OrderBy(x => x.BatchCode).ToList().ForEach(x =>
        //    {
        //        selectListBatchCode.AddItemValText(x.BatchCode, x.BatchCode + " - " + x.BatchName);
        //    });
        //    ViewBag.BatchCode = selectListBatchCode.ToSelectList(rITASE.BatchCode);

        //    return View(rITASE);
        //}

        //// GET: Ritase/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RITASE rITASE = db.RITASE.Find(id);
        //    if (rITASE == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(rITASE);
        //}

        //// POST: Ritase/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    RITASE rITASE = db.RITASE.Find(id);
        //    db.RITASE.Remove(rITASE);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
