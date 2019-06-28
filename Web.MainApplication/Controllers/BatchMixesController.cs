using System;
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
    public class BatchMixesController : BaseController
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();

        // GET: BatchMixes
        public ActionResult Index()
        {
            var batchMix = db.BatchMix.Include(b => b.PRODUCT);
            return View(batchMix.ToList());
        }

        // GET: BatchMixes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BatchMix batchMix = db.BatchMix.Find(id);
            if (batchMix == null)
            {
                return HttpNotFound();
            }
            return View(batchMix);
        }

        // GET: BatchMixes/Create
        public ActionResult Create()
        {
            List<SelectListItem> lsProduct = new List<SelectListItem>();
            lsProduct.AddBlank();
            db.PRODUCT.Where(x => x.TypeMaterial == "Raw").OrderBy(x => x.PRODUCTNAME).ToList().ForEach(x =>
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
            BatchMix batchMix = new BatchMix();
            batchMix.BatchMixProduct.Add(new BatchMixProduct());
            ViewBag.IsActive = WebAppUtility.SelectListIsActive(batchMix.IsActive);
            ViewBag.MatureProduct = lsProductMature.ToSelectList();
            return View(batchMix);
            //ViewBag.OutputProduct = new SelectList(db.PRODUCT, "PRODUCTID", "PRODUCTNAME");
            //return View();
        }

        // POST: BatchMixes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BatchMixCode,BatchMixName,OutputProduct,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] BatchMix batchMix)
        {
            int totalComposedProduct = 0;
            int.TryParse(Request.Params["totalComposedProduct"], out totalComposedProduct);
            List<BatchMixProduct> batchMixProduct = new List<BatchMixProduct>();

            if (totalComposedProduct > 0)
            {
                for (int a = 1; a <= totalComposedProduct; a++)
                {
                    double percentage = 0;
                    double.TryParse(Request.Params["ProductPercentageChild_" + a.ToString()], out percentage);
                    batchMixProduct.Add(new BatchMixProduct()
                    {
                        BatchMixCode = batchMix.BatchMixCode,
                        ProductSourceId = Request.Params["ComposedProductChild_" + a.ToString()],
                        ProductSourcePercentage = percentage
                    });
                }
            }
            if (totalComposedProduct == 0)
            {
                WarningMessagesAdd("Total Processed Product Must be Greater Than 1");
            }
            if (batchMixProduct.Sum(x => x.ProductSourcePercentage) != 100)
            {
                WarningMessagesAdd("Total Percentage of All Processed Product must be 100");

            }

            if (ModelState.IsValid && totalComposedProduct > 0 && (batchMixProduct.Sum(x => x.ProductSourcePercentage) == 100))
            {


                db.BatchMix.Add(batchMix);
                db.BatchMixProduct.AddRange(batchMixProduct);
                db.SaveChanges();
                SuccessMessagesAdd("BatchMix " + batchMix.BatchMixCode + " added successfully");
                return RedirectToAction("Index");
            }

            List<SelectListItem> lsProduct = new List<SelectListItem>();
            lsProduct.AddBlank();
            db.PRODUCT.Where(x => x.TypeMaterial == "Raw").OrderBy(x => x.PRODUCTNAME).ToList().ForEach(x =>
            {
                lsProduct.AddItemValText(x.PRODUCTID, x.PRODUCTNAME + " - " + x.PRODUCTDESCRIPTION);
            });
            List<SelectListItem> lsProductMature = new List<SelectListItem>();
            lsProductMature.AddBlank();
            db.PRODUCT.Where(x => x.TypeMaterial == "Mature").OrderBy(x => x.PRODUCTNAME).ToList().ForEach(x =>
            {
                lsProductMature.AddItemValText(x.PRODUCTID, x.PRODUCTNAME + " - " + x.PRODUCTDESCRIPTION);
            });

            ViewBag.RawProduct = lsProduct.ToSelectList();
            batchMix.BatchMixProduct = batchMixProduct;
            ViewBag.MatureProduct = lsProductMature.ToSelectList(batchMix.OutputProduct);
            ViewBag.IsActive = WebAppUtility.SelectListIsActive(batchMix.IsActive);

            return View(batchMix);
        }

        // GET: BatchMixes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BatchMix batchMix = db.BatchMix.Find(id);
            if (batchMix == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> lsProduct = new List<SelectListItem>();
            lsProduct.AddBlank();
            db.PRODUCT.Where(x => x.TypeMaterial == "Raw").OrderBy(x => x.PRODUCTNAME).ToList().ForEach(x =>
            {
                lsProduct.AddItemValText(x.PRODUCTID, x.PRODUCTNAME + " - " + x.PRODUCTDESCRIPTION);
            });
            List<SelectListItem> lsProductMature = new List<SelectListItem>();
            lsProductMature.AddBlank();
            db.PRODUCT.Where(x => x.TypeMaterial == "Mature").OrderBy(x => x.PRODUCTNAME).ToList().ForEach(x =>
            {
                lsProductMature.AddItemValText(x.PRODUCTID, x.PRODUCTNAME + " - " + x.PRODUCTDESCRIPTION);
            });

            ViewBag.RawProduct = lsProduct.ToSelectList();
            ViewBag.MatureProduct = lsProductMature.ToSelectList(batchMix.OutputProduct);
            ViewBag.IsActive = WebAppUtility.SelectListIsActive(batchMix.IsActive);

            return View(batchMix);
        }

        // POST: BatchMixes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BatchMixCode,BatchMixName,OutputProduct,CreatedBy,UpdatedBy,CreatedDate,UpdatedDate,IsActive")] BatchMix batchMix)
        {
            int totalComposedProduct = 0;
            int.TryParse(Request.Params["totalComposedProduct"], out totalComposedProduct);
            List<BatchMixProduct> batchMixProduct = new List<BatchMixProduct>();

            if (totalComposedProduct > 0)
            {
                for (int a = 1; a <= totalComposedProduct; a++)
                {
                    double percentage = 0;
                    double.TryParse(Request.Params["ProductPercentageChild_" + a.ToString()], out percentage);
                    batchMixProduct.Add(new BatchMixProduct()
                    {
                        BatchMixCode = batchMix.BatchMixCode,
                        ProductSourceId = Request.Params["ComposedProductChild_" + a.ToString()],
                        ProductSourcePercentage = percentage
                    });
                }
            }
            if (totalComposedProduct == 0)
            {
                WarningMessagesAdd("Total Processed Product Must be Greater Than 1");
            }
            if (batchMixProduct.Sum(x => x.ProductSourcePercentage) != 100)
            {
                WarningMessagesAdd("Total Percentage of All Source Product must be 100");

            }
            if (batchMixProduct.Select(x => x.ProductSourceId).Distinct().Count() != batchMixProduct.Count())
            {
                WarningMessagesAdd("Contain duplicate source product");
            }

            if (ModelState.IsValid && WarningMessages().Count == 0)
            {


                batchMix.SetPropertyUpdate();
                db.Entry(batchMix).State = EntityState.Modified;
                batchMixProduct.ForEach(x =>
                {
                    var batchMixOrigin = db.BatchMixProduct.Where(z => z.ProductSourceId == x.ProductSourceId && x.BatchMixCode == x.BatchMixCode).FirstOrDefault();
                    if (batchMixOrigin != null)
                    {
                        x.CreatedBy = batchMixOrigin.CreatedBy;
                        x.CreatedDate = batchMixOrigin.CreatedDate;
                        x.SetPropertyUpdate();
                    }
                    else
                    {
                        x.SetPropertyCreate();
                    }
                });
                db.BatchMixProduct.RemoveRange(db.BatchMixProduct.Where(x => x.BatchMixCode == batchMix.BatchMixCode));
                db.BatchMixProduct.AddRange(batchMixProduct);
                db.SaveChanges();
                SuccessMessagesAdd(Message.UpdateSuccess + ", " + batchMix.BatchMixCode);
                return RedirectToAction("Index");
            }

            List<SelectListItem> lsProduct = new List<SelectListItem>();
            lsProduct.AddBlank();
            db.PRODUCT.Where(x => x.TypeMaterial == "Raw").OrderBy(x => x.PRODUCTNAME).ToList().ForEach(x =>
            {
                lsProduct.AddItemValText(x.PRODUCTID, x.PRODUCTNAME + " - " + x.PRODUCTDESCRIPTION);
            });
            List<SelectListItem> lsProductMature = new List<SelectListItem>();
            lsProductMature.AddBlank();
            db.PRODUCT.Where(x => x.TypeMaterial == "Mature").OrderBy(x => x.PRODUCTNAME).ToList().ForEach(x =>
            {
                lsProductMature.AddItemValText(x.PRODUCTID, x.PRODUCTNAME + " - " + x.PRODUCTDESCRIPTION);
            });

            ViewBag.RawProduct = lsProduct.ToSelectList();
            batchMix.BatchMixProduct = batchMixProduct;
            ViewBag.MatureProduct = lsProductMature.ToSelectList(batchMix.OutputProduct);
            ViewBag.IsActive = WebAppUtility.SelectListIsActive(batchMix.IsActive);

            return View(batchMix);
        }

        // GET: BatchMixes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BatchMix batchMix = db.BatchMix.Find(id);
            if (batchMix == null)
            {
                return HttpNotFound();
            }
            return View(batchMix);
        }

        // POST: BatchMixes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BatchMix batchMix = db.BatchMix.Find(id);
            db.BatchMix.Remove(batchMix);
            db.BatchMixProduct.RemoveRange(db.BatchMixProduct.Where(x => x.BatchMixCode == batchMix.BatchMixCode));
            db.SaveChanges();
            SuccessMessagesAdd(Message.DeleteSuccess + ", " + batchMix.BatchMixCode);
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
