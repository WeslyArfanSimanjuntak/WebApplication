using Repository.Application.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
namespace Web.MainApplication.Controllers
{
    public class TransactionProductController : BaseController
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();

        // GET: TransactionProduct
        public ActionResult Index()
        {
            var transactionProduct = db.TransactionProduct.Where(x => x.IsActive != 0).Include(t => t.PRODUCT).Include(t => t.RITASE);
            return View(transactionProduct.ToList());
        }

        // GET: TransactionProduct/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionProduct transactionProduct = db.TransactionProduct.Find(id);
            if (transactionProduct == null)
            {
                return HttpNotFound();
            }
            return View(transactionProduct);
        }
        [HttpGet]
        public ActionResult AdjustProduct()
        {
            var lastPA = db.ProductAdjustment.OrderByDescending(x => x.AdjustmentId).FirstOrDefault();
            var lastId = lastPA != null ? lastPA.AdjustmentId : 0;
            ProductAdjustment pa = new ProductAdjustment()
            {
                AdjustmentNumber = WebAppUtility.TransactionProductAdjustmentNumberGenerator(lastId + 1),
                AdjustBy = User.Identity.Name,
                Ammount = 0
            };
            List<SelectListItem> selectListItemSite = new List<SelectListItem>();
            selectListItemSite.AddBlank();
            db.SITE.ToList().ForEach(x =>
            {
                selectListItemSite.AddItemValText(x.SITENAME, x.SITENAME + " - " + x.SITEADDRESS);
            });
            ViewBag.SiteName = selectListItemSite.ToSelectList();
            List<SelectListItem> selectListItemProduct = new List<SelectListItem>();
            selectListItemProduct.AddBlank();
            db.PRODUCT.ToList().ForEach(x =>
            {
                selectListItemProduct.AddItemValText(x.PRODUCTID, x.PRODUCTID + " - " + x.PRODUCTNAME);
            });
            ViewBag.ProductId = selectListItemProduct.ToSelectList();
            float convertTonToM3;
            float.TryParse((string)db.ParameterSetup.Where(x => x.Name == "Convertion(ton to m3)").FirstOrDefault().Value, out convertTonToM3);
            ViewBag.TonUnitToM3Unit = convertTonToM3;
            return View(pa);
        }

        [HttpPost]
        public ActionResult AdjustProduct(ProductAdjustment pa)
        {
            var lastPA = db.ProductAdjustment.OrderByDescending(x => x.AdjustmentId).FirstOrDefault();
            var lastId = lastPA != null ? lastPA.AdjustmentId : 0;

            var productSite = db.ProductSite.Where(x => x.ProductId == pa.ProductId).FirstOrDefault();
            if (productSite == null)
            {
                WarningMessagesAdd("Product \"" + pa.ProductId + "\" is not found.");
            }
            if (productSite != null && (productSite.TotalStock + pa.Ammount) < 0)
            {
                WarningMessagesAdd("Insuficient Stock. Total stock : " + productSite.TotalStock.ToString() + " ton");
            }

            if (ModelState.IsValid && WarningMessages().Count == 0)
            {
                using (var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        productSite.TotalStock = productSite.TotalStock + pa.Ammount;
                        productSite.SetPropertyUpdate();
                        db.Entry(productSite).State = System.Data.Entity.EntityState.Modified;
                        pa.AdjustmentNumber = WebAppUtility.TransactionProductAdjustmentNumberGenerator(lastId + 1);
                        pa.AdjustBy = User.Identity.Name;

                        var newTransactionProduct = new TransactionProduct();
                        newTransactionProduct.AdjustmentId = pa.AdjustmentId;
                        newTransactionProduct.Ammount = (double)pa.Ammount;
                        newTransactionProduct.Jenis = "Adjusment";
                        newTransactionProduct.ProductId = pa.ProductId;
                        newTransactionProduct.TypeDebitOrCredit = pa.Ammount >= 0 ? "Kredit" : "Debit";
                        newTransactionProduct.Remark = pa.Remark;
                        var lastTP = db.TransactionProduct.OrderByDescending(x => x.Id).FirstOrDefault();
                        var lastTPId = lastTP != null ? lastTP.Id : 0;
                        newTransactionProduct.TransactionProductNumber = WebAppUtility.TransactionProductNumberGenerator(lastTPId + 1);
                        newTransactionProduct.SetPropertyCreate();
                        db.TransactionProduct.Add(newTransactionProduct);
                        pa.SetPropertyCreate();
                        db.ProductAdjustment.Add(pa);
                        db.SaveChanges();

                        dbTransaction.Commit();
                        SuccessMessagesAdd("Adjusment Success With Number \"" + pa.AdjustmentNumber + "\"");
                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        dbTransaction.Rollback();
                        WarningMessagesAdd(e.Message);
                    }
                }




            }

            pa.AdjustmentNumber = WebAppUtility.TransactionProductAdjustmentNumberGenerator(lastId + 1);
            List<SelectListItem> selectListItemSite = new List<SelectListItem>();
            selectListItemSite.AddBlank();
            db.SITE.ToList().ForEach(x =>
            {
                selectListItemSite.AddItemValText(x.SITENAME, x.SITENAME + " - " + x.SITEADDRESS);
            });
            ViewBag.SiteName = selectListItemSite.ToSelectList();
            List<SelectListItem> selectListItemProduct = new List<SelectListItem>();
            selectListItemProduct.AddBlank();
            db.PRODUCT.ToList().ForEach(x =>
            {
                selectListItemProduct.AddItemValText(x.PRODUCTID, x.PRODUCTID + " - " + x.PRODUCTNAME);
            });
            ViewBag.ProductId = selectListItemProduct.ToSelectList();
            float convertTonToM3;
            float.TryParse((string)db.ParameterSetup.Where(x => x.Name == "Convertion(ton to m3)").FirstOrDefault().Value, out convertTonToM3);
            ViewBag.TonUnitToM3Unit = convertTonToM3;
            return View(pa);
        }
        [HttpGet]
        public ActionResult ConvertProduct()
        {

            var lastCP = db.ProductConvertion.OrderByDescending(x => x.ConvertionId).FirstOrDefault();
            var lastId = lastCP != null ? lastCP.ConvertionId : 0;
            ProductConvertion pc = new ProductConvertion()
            {
                ConvertionNumber = WebAppUtility.TransactionProductConvertionNumberGenerator(lastId + 1),
                Ammount = 0,
                ConvertBy = User.Identity.Name

            };
            List<SelectListItem> selectListItemSite = new List<SelectListItem>();
            selectListItemSite.AddBlank();
            db.SITE.ToList().ForEach(x =>
            {
                selectListItemSite.AddItemValText(x.SITENAME, x.SITENAME + " - " + x.SITEADDRESS);
            });
            ViewBag.Site = selectListItemSite.ToSelectList();
            List<SelectListItem> selectListItemProduct = new List<SelectListItem>();
            selectListItemProduct.AddBlank();
            db.PRODUCT.ToList().ForEach(x =>
            {
                selectListItemProduct.AddItemValText(x.PRODUCTID, x.PRODUCTID + " - " + x.PRODUCTNAME);
            });
            ViewBag.SourceProudct = selectListItemProduct.ToSelectList();
            var listSelectItemBatchProduct = new List<SelectListItem>();
            listSelectItemBatchProduct.AddBlank();
            db.BATCH.ToList().ForEach(x =>
            {
                listSelectItemBatchProduct.AddItemValText(x.BatchCode, x.BatchCode + " - " + x.BatchName + " [Raw Product = " + x.RawProduct + " - " + x.PRODUCT.PRODUCTNAME + "]");
            });
            ViewBag.BatchCode = listSelectItemBatchProduct.ToSelectList();
            float convertTonToM3;
            float.TryParse((string)db.ParameterSetup.Where(x => x.Name == "Convertion(ton to m3)").FirstOrDefault().Value, out convertTonToM3);
            ViewBag.TonUnitToM3Unit = convertTonToM3;
            return View(pc);
        }
        [HttpPost]
        public ActionResult ConvertProduct(ProductConvertion pc)
        {
            var productSiteStock = db.ProductSite.Where(x => x.SiteName == pc.Site && x.ProductId == pc.SourceProudct).FirstOrDefault();
            if (productSiteStock == null)
            {
                WarningMessagesAdd("Product \"" + pc.SourceProudct + "\" is not found in site \"" + pc.Site + "\".");
            }
            if (productSiteStock != null && (productSiteStock.TotalStock < pc.Ammount))
            {
                WarningMessagesAdd("Insufficient Stock. Total stock : " + productSiteStock.TotalStock.ToString() + " ton");
            }
            if (pc.Ammount < 0)
            {
                WarningMessagesAdd("Amount can not be lower than 0 ton/m3");
            }
            var batchProductFirst = db.BatchProduct.Where(x => x.BatchCode == pc.BatchCode).FirstOrDefault();
            string rawProduct = batchProductFirst.BATCH.RawProduct;
            if (batchProductFirst.BATCH.RawProduct != pc.SourceProudct)
            {
                WarningMessagesAdd("Product and Batch is not match.");
            }


            if (ModelState.IsValid && WarningMessages().Count == 0)
            {
                using (var dbTransaction = db.Database.BeginTransaction())
                {

                    try
                    {
                        var batchProduct = db.BatchProduct.Where(x => x.BatchCode == pc.BatchCode).ToList();
                        List<TransactionProduct> listTransactionProduct = new List<TransactionProduct>();

                        foreach (var item in batchProduct)
                        {
                            listTransactionProduct.Add(new TransactionProduct()
                            {
                                ProductId = item.ProductId,
                                ConvertionId = pc.ConvertionId,
                                Jenis = "Convertion",
                                TypeDebitOrCredit = "Kredit",
                                Ammount = item.ProductPercentage / 100 * pc.Ammount

                            });
                            var productSite = db.ProductSite.Where(x => x.ProductId == item.ProductId && x.SiteName == pc.Site).FirstOrDefault();
                            if (productSite != null)
                            {
                                productSite.TotalStock = productSite.TotalStock + (item.ProductPercentage / 100 * pc.Ammount);
                                productSite.SetPropertyUpdate();
                                db.Entry(productSite).State = System.Data.Entity.EntityState.Modified;
                            }
                            else
                            {
                                productSite = new ProductSite()
                                {
                                    SiteName = pc.Site,
                                    ProductId = item.ProductId,
                                    TotalStock = item.ProductPercentage / 100 * pc.Ammount
                                };
                                productSite.SetPropertyCreate();
                                db.ProductSite.Add(productSite);
                            }

                        }

                        //db.TransactionProduct.AddRange(listTransactionProduct);
                        pc.SetPropertyCreate();
                        var lastPC = db.TransactionProduct.OrderByDescending(z => z.Id).FirstOrDefault();
                        var lastPCId = lastPC != null ? lastPC.Id : 0;
                        pc.ConvertionNumber = WebAppUtility.TransactionProductConvertionNumberGenerator(lastPCId + 1);
                        db.ProductConvertion.Add(pc);
                        db.SaveChanges();
                        listTransactionProduct.ForEach(x =>
                        {
                            var lastTransaction = db.TransactionProduct.OrderByDescending(z => z.Id).FirstOrDefault();
                            var lastTPId = lastTransaction != null ? lastTransaction.Id : 0;
                            x.TransactionProductNumber = WebAppUtility.TransactionProductNumberGenerator(lastTPId + 1);
                            x.SetPropertyCreate();
                            x.ConvertionId = pc.ConvertionId;
                            x.Remark = pc.Remark;
                            db.TransactionProduct.Add(x);
                            db.SaveChanges();
                        });
                        var productSiteToUpdate = db.ProductSite.Find(pc.SourceProudct, pc.Site);
                        productSiteToUpdate.TotalStock = productSiteToUpdate.TotalStock - pc.Ammount;
                        db.Entry(productSiteToUpdate).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        dbTransaction.Commit();
                        SuccessMessagesAdd("Convertion Success With Number \"" + pc.ConvertionNumber + "\"");
                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        dbTransaction.Rollback();
                        WarningMessagesAdd(e.Message);
                    }
                }

            }

            var lastCP = db.ProductConvertion.OrderByDescending(x => x.ConvertionId).FirstOrDefault();
            var lastId = lastCP != null ? lastCP.ConvertionId : 0;

            pc.ConvertionNumber = WebAppUtility.TransactionProductConvertionNumberGenerator(lastId + 1);


            List<SelectListItem> selectListItemSite = new List<SelectListItem>();
            selectListItemSite.AddBlank();
            db.SITE.ToList().ForEach(x =>
            {
                selectListItemSite.AddItemValText(x.SITENAME, x.SITENAME + " - " + x.SITEADDRESS);
            });
            ViewBag.Site = selectListItemSite.ToSelectList(pc.Site);
            List<SelectListItem> selectListItemProduct = new List<SelectListItem>();
            selectListItemProduct.AddBlank();
            db.PRODUCT.ToList().ForEach(x =>
            {
                selectListItemProduct.AddItemValText(x.PRODUCTID, x.PRODUCTID + " - " + x.PRODUCTNAME);
            });
            ViewBag.SourceProudct = selectListItemProduct.ToSelectList(pc.SourceProudct);
            var listSelectItemBatchProduct = new List<SelectListItem>();
            listSelectItemBatchProduct.AddBlank();
            db.BATCH.ToList().ForEach(x =>
            {
                listSelectItemBatchProduct.AddItemValText(x.BatchCode, x.BatchCode + " - " + x.BatchName + " [Raw Product = " + x.RawProduct + " - " + x.PRODUCT.PRODUCTNAME + "]");

            });
            ViewBag.BatchCode = listSelectItemBatchProduct.ToSelectList(pc.BatchCode);
            float convertTonToM3;
            float.TryParse((string)db.ParameterSetup.Where(x => x.Name == "Convertion(ton to m3)").FirstOrDefault().Value, out convertTonToM3);
            ViewBag.TonUnitToM3Unit = convertTonToM3;
            return View(pc);
        }
        [HttpGet]
        public ActionResult MixProduct()
        {
            ProductMixing pm = new ProductMixing()
            {
                ProductMixingNumber = WebAppUtility.TransactionProductMixingNumberGenerator(this.db.ProductMixingSequence() + 1)
                ,
                Ammount = 0,
                MixedBy = User.Identity.Name
            };

            List<SelectListItem> selectListItemSite = new List<SelectListItem>();
            selectListItemSite.AddBlank();
            db.SITE.ToList().ForEach(x =>
            {
                selectListItemSite.AddItemValText(x.SITENAME, x.SITENAME + " - " + x.SITEADDRESS);
            });
            ViewBag.Site = selectListItemSite.ToSelectList();
            //List<SelectListItem> selectListItemProduct = new List<SelectListItem>();
            //selectListItemProduct.AddBlank();
            //db.PRODUCT.ToList().ForEach(x =>
            //{
            //    selectListItemProduct.AddItemValText(x.PRODUCTID, x.PRODUCTID + " - " + x.PRODUCTNAME);
            //});
            //ViewBag.SourceProudct = selectListItemProduct.ToSelectList();
            var listSelectItemBatchMixProduct = new List<SelectListItem>();
            listSelectItemBatchMixProduct.AddBlank();
            //db.BatchMix.ToList().ForEach(x =>
            //{
            //    listSelectItemBatchMixProduct.AddItemValText(x.BatchCode, x.BatchCode + " - " + x.BatchName + " [Raw Product = " + x.RawProduct + " - " + x.PRODUCT.PRODUCTNAME + "]");
            //});
            //ViewBag.BatchCode = listSelectItemBatchProduct.ToSelectList();
            float convertTonToM3;
            float.TryParse((string)db.ParameterSetup.Where(x => x.Name == "Convertion(ton to m3)").FirstOrDefault().Value, out convertTonToM3);
            ViewBag.TonUnitToM3Unit = convertTonToM3;
            var sliBatchMixCode = new List<SelectListItem>();
            sliBatchMixCode.AddBlank();
            db.BatchMix.Where(x => x.IsActive == 1).ToList().ForEach(x =>
            {
                sliBatchMixCode.AddItemValText(x.BatchMixName, x.BatchMixCode);
            });
            ViewBag.BatchMixCode = sliBatchMixCode.ToSelectList();

            
            //ViewBag.IsActive = WebAppUtility.SelectListIsActive();
            return View(pm);
        }
        //[HttpPost]
        //public ActionResult MixProduct(ProductMixing pm )
        //{
        //    var productSiteStock = db.ProductSite.Where(x => x.SiteName == pc.Site && x.ProductId == pc.SourceProudct).FirstOrDefault();
        //    if (productSiteStock == null)
        //    {
        //        WarningMessagesAdd("Product \"" + pc.SourceProudct + "\" is not found in site \"" + pc.Site + "\".");
        //    }
        //    if (productSiteStock != null && (productSiteStock.TotalStock < pc.Ammount))
        //    {
        //        WarningMessagesAdd("Insufficient Stock. Total stock : " + productSiteStock.TotalStock.ToString() + " ton");
        //    }
        //    if (pc.Ammount < 0)
        //    {
        //        WarningMessagesAdd("Amount can not be lower than 0 ton/m3");
        //    }
        //    var batchProductFirst = db.BatchProduct.Where(x => x.BatchCode == pc.BatchCode).FirstOrDefault();
        //    string rawProduct = batchProductFirst.BATCH.RawProduct;
        //    if (batchProductFirst.BATCH.RawProduct != pc.SourceProudct)
        //    {
        //        WarningMessagesAdd("Product and Batch is not match.");
        //    }


        //    if (ModelState.IsValid && WarningMessages().Count == 0)
        //    {
        //        using (var dbTransaction = db.Database.BeginTransaction())
        //        {

        //            try
        //            {
        //                var batchProduct = db.BatchProduct.Where(x => x.BatchCode == pc.BatchCode).ToList();
        //                List<TransactionProduct> listTransactionProduct = new List<TransactionProduct>();

        //                foreach (var item in batchProduct)
        //                {
        //                    listTransactionProduct.Add(new TransactionProduct()
        //                    {
        //                        ProductId = item.ProductId,
        //                        ConvertionId = pc.ConvertionId,
        //                        Jenis = "Convertion",
        //                        TypeDebitOrCredit = "Kredit",
        //                        Ammount = item.ProductPercentage / 100 * pc.Ammount

        //                    });
        //                    var productSite = db.ProductSite.Where(x => x.ProductId == item.ProductId && x.SiteName == pc.Site).FirstOrDefault();
        //                    if (productSite != null)
        //                    {
        //                        productSite.TotalStock = productSite.TotalStock + (item.ProductPercentage / 100 * pc.Ammount);
        //                        productSite.SetPropertyUpdate();
        //                        db.Entry(productSite).State = System.Data.Entity.EntityState.Modified;
        //                    }
        //                    else
        //                    {
        //                        productSite = new ProductSite()
        //                        {
        //                            SiteName = pc.Site,
        //                            ProductId = item.ProductId,
        //                            TotalStock = item.ProductPercentage / 100 * pc.Ammount
        //                        };
        //                        productSite.SetPropertyCreate();
        //                        db.ProductSite.Add(productSite);
        //                    }

        //                }

        //                //db.TransactionProduct.AddRange(listTransactionProduct);
        //                pc.SetPropertyCreate();
        //                var lastPC = db.TransactionProduct.OrderByDescending(z => z.Id).FirstOrDefault();
        //                var lastPCId = lastPC != null ? lastPC.Id : 0;
        //                pc.ConvertionNumber = WebAppUtility.TransactionProductConvertionNumberGenerator(lastPCId + 1);
        //                db.ProductConvertion.Add(pc);
        //                db.SaveChanges();
        //                listTransactionProduct.ForEach(x =>
        //                {
        //                    var lastTransaction = db.TransactionProduct.OrderByDescending(z => z.Id).FirstOrDefault();
        //                    var lastTPId = lastTransaction != null ? lastTransaction.Id : 0;
        //                    x.TransactionProductNumber = WebAppUtility.TransactionProductNumberGenerator(lastTPId + 1);
        //                    x.SetPropertyCreate();
        //                    x.ConvertionId = pc.ConvertionId;
        //                    x.Remark = pc.Remark;
        //                    db.TransactionProduct.Add(x);
        //                    db.SaveChanges();
        //                });
        //                var productSiteToUpdate = db.ProductSite.Find(pc.SourceProudct, pc.Site);
        //                productSiteToUpdate.TotalStock = productSiteToUpdate.TotalStock - pc.Ammount;
        //                db.Entry(productSiteToUpdate).State = System.Data.Entity.EntityState.Modified;
        //                db.SaveChanges();
        //                dbTransaction.Commit();
        //                SuccessMessagesAdd("Convertion Success With Number \"" + pc.ConvertionNumber + "\"");
        //                return RedirectToAction("Index");
        //            }
        //            catch (Exception e)
        //            {
        //                dbTransaction.Rollback();
        //                WarningMessagesAdd(e.Message);
        //            }
        //        }

        //    }

        //    var lastCP = db.ProductConvertion.OrderByDescending(x => x.ConvertionId).FirstOrDefault();
        //    var lastId = lastCP != null ? lastCP.ConvertionId : 0;

        //    pc.ConvertionNumber = WebAppUtility.TransactionProductConvertionNumberGenerator(lastId + 1);


        //    List<SelectListItem> selectListItemSite = new List<SelectListItem>();
        //    selectListItemSite.AddBlank();
        //    db.SITE.ToList().ForEach(x =>
        //    {
        //        selectListItemSite.AddItemValText(x.SITENAME, x.SITENAME + " - " + x.SITEADDRESS);
        //    });
        //    ViewBag.Site = selectListItemSite.ToSelectList(pc.Site);
        //    List<SelectListItem> selectListItemProduct = new List<SelectListItem>();
        //    selectListItemProduct.AddBlank();
        //    db.PRODUCT.ToList().ForEach(x =>
        //    {
        //        selectListItemProduct.AddItemValText(x.PRODUCTID, x.PRODUCTID + " - " + x.PRODUCTNAME);
        //    });
        //    ViewBag.SourceProudct = selectListItemProduct.ToSelectList(pc.SourceProudct);
        //    var listSelectItemBatchProduct = new List<SelectListItem>();
        //    listSelectItemBatchProduct.AddBlank();
        //    db.BATCH.ToList().ForEach(x =>
        //    {
        //        listSelectItemBatchProduct.AddItemValText(x.BatchCode, x.BatchCode + " - " + x.BatchName + " [Raw Product = " + x.RawProduct + " - " + x.PRODUCT.PRODUCTNAME + "]");

        //    });
        //    ViewBag.BatchCode = listSelectItemBatchProduct.ToSelectList(pc.BatchCode);
        //    float convertTonToM3;
        //    float.TryParse((string)db.ParameterSetup.Where(x => x.Name == "Convertion(ton to m3)").FirstOrDefault().Value, out convertTonToM3);
        //    ViewBag.TonUnitToM3Unit = convertTonToM3;
        //    var sliBatchMixCode = new List<SelectListItem>();
        //    sliBatchMixCode.AddBlank();
        //    db.BatchMix.Where(x => x.IsActive == 1).ToList().ForEach(x =>
        //    {
        //        sliBatchMixCode.Add(new SelectListItem()
        //        {
        //            Text = x.BatchMixCode,
        //            Value = x.BatchMixName
        //        });
        //    });
        //    ViewBag.BatchMixCode = sliBatchMixCode.ToSelectList(pm.BatchMixCode);


        //    var sliOutComeProduct = new List<SelectListItem>();
        //    sliOutComeProduct.AddBlank();

        //    db.PRODUCT.Where(x => x.IsActive == 1).ToList().ForEach(z =>
        //    {
        //        sliOutComeProduct.AddItemValText(z.PRODUCTID, z.PRODUCTID + " - " + z.PRODUCTNAME);
        //    });
        //    ViewBag.OutcomeProudct = sliOutComeProduct.ToSelectList(pm.OutcomeProudct);

        //    return View(pm);
        //}
        [HttpGet]
        public ActionResult ConvertionDetails(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductConvertion convertionProduct = db.ProductConvertion.Find(id);
            if (convertionProduct == null)
            {
                return HttpNotFound();
            }
            return View(convertionProduct);
        }

        [HttpGet]
        public ActionResult AdjustmentDetails(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductAdjustment productAdjustment = db.ProductAdjustment.Find(id);
            if (productAdjustment == null)
            {
                return HttpNotFound();
            }
            return View(productAdjustment);
        }

        [HttpGet]
        public ActionResult StockDetails()
        {
            var stockDetailSite = db.ProductSite;

            return View(stockDetailSite.ToList());
        }
        [HttpGet]
        public ActionResult StockSiteDetails()
        {
            var stockDetailSite = db.ProductSite;

            return View(stockDetailSite.ToList());
        }
        [HttpGet]
        public ActionResult SiteProductDetailsTransaction(string siteId, string productId)
        {
            if (string.IsNullOrWhiteSpace(siteId) || string.IsNullOrWhiteSpace(productId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var transactionDetails = db.TransactionProduct.Where(x => x.RITASE.SITE == siteId && x.ProductId == productId).ToList();
            transactionDetails.AddRange(db.TransactionProduct.Where(x => x.ProductAdjustment.SiteName == siteId && x.ProductId == productId).ToList());
            transactionDetails.AddRange(db.TransactionProduct.Where(x => x.ProductConvertion.Site == siteId && x.ProductId == productId).ToList());
            transactionDetails.OrderByDescending(x => x.CreatedDate);
            ViewBag.Stock = db.ProductSite.Where(x => x.SiteName == siteId && x.ProductId == productId).FirstOrDefault().TotalStock;
            ViewBag.SiteDetails = db.SITE.Where(x => x.SITENAME == siteId).FirstOrDefault();
            return View(transactionDetails);
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
