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
    public class BatchController : BaseController
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();

        // GET: Batch
        public ActionResult Index()
        {
            var bATCH = db.BATCH.Include(b => b.PRODUCT);
            return View(bATCH.ToList());
        }

        // GET: Batch/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BATCH bATCH = db.BATCH.Find(id);
            if (bATCH == null)
            {
                return HttpNotFound();
            }

            return View(bATCH);
        }

        // GET: Batch/Create
        public ActionResult Create()
        {
            List<SelectListItem> lsProduct = new List<SelectListItem>();
            lsProduct.AddBlank();
            db.PRODUCT.OrderBy(x => x.PRODUCTNAME).ToList().ForEach(x =>
                {
                    lsProduct.AddItemValText(x.PRODUCTID, x.PRODUCTNAME + " - " + x.PRODUCTDESCRIPTION);
                });
            ViewBag.RawProduct = lsProduct.ToSelectList();
            List<SelectListItem> lsProductMature = new List<SelectListItem>();
            lsProductMature.AddBlank();
            db.PRODUCT.Where(x => x.TypeMaterial == "Mature").OrderBy(x => x.PRODUCTNAME).ToList().ForEach(x =>
            {
                lsProductMature.AddItemValText(x.PRODUCTID, x.PRODUCTNAME + " - " + x.PRODUCTDESCRIPTION);
            });
            BATCH batch = new BATCH();
            batch.BatchProduct.Add(new BatchProduct());
            ViewBag.MatureProduct = lsProductMature.ToSelectList();
            return View(batch);
        }

        // POST: Batch/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BatchCode,BatchName,RawProduct,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] BATCH bATCH)
        {
            int totalComposedProduct = 0;
            int.TryParse(Request.Params["totalComposedProduct"], out totalComposedProduct);
            List<BatchProduct> batchProduct = new List<BatchProduct>();

            if (totalComposedProduct > 0)
            {
                for (int a = 1; a <= totalComposedProduct; a++)
                {
                    double percentage = 0;
                    double.TryParse(Request.Params["ProductPercentageChild_" + a.ToString()], out percentage);
                    batchProduct.Add(new BatchProduct()
                    {
                        BatchCode = bATCH.BatchCode,
                        ProductId = Request.Params["ComposedProductChild_" + a.ToString()],
                        ProductPercentage = percentage
                    });
                }
            }
            if (totalComposedProduct == 0)
            {
                WarningMessagesAdd("Total Processed Product Must be Greater Than 1");
            }
            if (batchProduct.Sum(x => x.ProductPercentage) != 100)
            {
                WarningMessagesAdd("Total Percentage of All Processed Product must be 100");

            }

            if (ModelState.IsValid && totalComposedProduct > 0 && !(batchProduct.Sum(x => x.ProductPercentage) != 100))
            {


                db.BATCH.Add(bATCH);
                db.BatchProduct.AddRange(batchProduct);
                db.SaveChanges();
                SuccessMessagesAdd("Batch " + bATCH.BatchCode + " added successfully");
                return RedirectToAction("Index");
            }

            List<SelectListItem> lsProduct = new List<SelectListItem>();
            lsProduct.AddBlank();
            db.PRODUCT.OrderBy(x => x.PRODUCTNAME).ToList().ForEach(x =>
            {
                lsProduct.AddItemValText(x.PRODUCTID, x.PRODUCTNAME + " - " + x.PRODUCTDESCRIPTION);
            });
            List<SelectListItem> lsProductMature = new List<SelectListItem>();
            lsProductMature.AddBlank();
            db.PRODUCT.Where(x => x.TypeMaterial == "Mature").OrderBy(x => x.PRODUCTNAME).ToList().ForEach(x =>
            {
                lsProductMature.AddItemValText(x.PRODUCTID, x.PRODUCTNAME + " - " + x.PRODUCTDESCRIPTION);
            });
            ViewBag.RawProduct = lsProduct.ToSelectList(bATCH.RawProduct);
            bATCH.BatchProduct = batchProduct;
            ViewBag.MatureProduct = lsProductMature.ToSelectList();
            return View(bATCH);
        }

        // GET: Batch/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BATCH bATCH = db.BATCH.Find(id);

            if (bATCH == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> lsProduct = new List<SelectListItem>();
            lsProduct.AddBlank();
            db.PRODUCT.OrderBy(x => x.PRODUCTNAME).ToList().ForEach(x =>
            {
                lsProduct.AddItemValText(x.PRODUCTID, x.PRODUCTNAME + " - " + x.PRODUCTDESCRIPTION);
            });
            List<SelectListItem> lsProductMature = new List<SelectListItem>();
            lsProductMature.AddBlank();
            db.PRODUCT.Where(x => x.TypeMaterial == "Mature").OrderBy(x => x.PRODUCTNAME).ToList().ForEach(x =>
            {
                lsProductMature.AddItemValText(x.PRODUCTID, x.PRODUCTNAME + " - " + x.PRODUCTDESCRIPTION);
            });
            ViewBag.MatureProduct = lsProductMature.ToSelectList();
            ViewBag.RawProduct = lsProduct.ToSelectList(bATCH.RawProduct);
            return View(bATCH);
        }

        // POST: Batch/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BatchCode,BatchName,RawProduct,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] BATCH bATCH)
        {
            int totalComposedProduct = 0;
            Int32.TryParse(Request.Params["totalComposedProduct"], out totalComposedProduct);
            List<BatchProduct> batchProduct = new List<BatchProduct>();

            if (totalComposedProduct > 0)
            {
                for (int a = 1; a <= totalComposedProduct; a++)
                {
                    double percentage = 0;
                    Double.TryParse(Request.Params["ProductPercentageChild_" + a.ToString()], out percentage);
                    batchProduct.Add(new BatchProduct()
                    {
                        BatchCode = bATCH.BatchCode,
                        ProductId = Request.Params["ComposedProductChild_" + a.ToString()],
                        ProductPercentage = percentage
                    });
                }
            }
            if (totalComposedProduct == 0)
            {
                WarningMessagesAdd("Total Processed Product Must be Greater Than 1");
            }
            if (batchProduct.Sum(x => x.ProductPercentage) != 100)
            {
                WarningMessagesAdd("Total Percentage of All Processed Product must be 100");

            }
            if (ModelState.IsValid && WarningMessages().Count == 0)
            {
                batchProduct.ForEach(z =>
                {
                    var batchProductOrigin = db.BatchProduct.Where(x => x.BatchCode == bATCH.BatchCode && x.ProductId == z.ProductId).FirstOrDefault();
                    if (batchProductOrigin != null)
                    {
                        z.CreatedBy = batchProductOrigin.CreatedBy;
                        z.CreatedDate = batchProductOrigin.CreatedDate;
                        z.SetPropertyUpdate();
                    }
                    else
                    {
                        z.SetPropertyCreate();
                    }


                });
                db.BatchProduct.RemoveRange(db.BatchProduct.Where(x => x.BatchCode == bATCH.BatchCode));
                db.SaveChanges();
                db.BatchProduct.AddRange(batchProduct);
                db.Entry(bATCH).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                WarningMessagesAdd(Message.UpdateSuccess + ", " + bATCH.BatchCode);

                return RedirectToAction("Index");
            }
            List<SelectListItem> lsProduct = new List<SelectListItem>();
            lsProduct.AddBlank();
            db.PRODUCT.OrderBy(x => x.PRODUCTNAME).ToList().ForEach(x =>
            {
                lsProduct.AddItemValText(x.PRODUCTID, x.PRODUCTNAME + " - " + x.PRODUCTDESCRIPTION);
            });
            List<SelectListItem> lsProductMature = new List<SelectListItem>();
            lsProductMature.AddBlank();
            db.PRODUCT.Where(x => x.TypeMaterial == "Mature").OrderBy(x => x.PRODUCTNAME).ToList().ForEach(x =>
                {
                    lsProductMature.AddItemValText(x.PRODUCTID, x.PRODUCTNAME + " - " + x.PRODUCTDESCRIPTION);
                });
            ViewBag.RawProduct = lsProduct.ToSelectList(bATCH.RawProduct);
            ViewBag.MatureProduct = lsProductMature.ToSelectList();
            bATCH.BatchProduct = batchProduct;
            return View(bATCH);
        }

        // GET: Batch/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BATCH bATCH = db.BATCH.Find(id);
            if (bATCH == null)
            {
                return HttpNotFound();
            }
            return View(bATCH);
        }

        // POST: Batch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                BATCH bATCH = db.BATCH.Find(id);
                db.BATCH.Remove(bATCH);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                WarningMessagesAdd(e.Message);
            }

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
