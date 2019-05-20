using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Repository.Application.DataModel;

namespace Web.MainApplication.Controllers
{
    public class ProductController : BaseController
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();

        // GET: Product
        public ActionResult Index()
        {
            return View(db.PRODUCT.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            var slTypeMaterial = new List<SelectListItem>();
            slTypeMaterial.AddBlank();
            slTypeMaterial.AddItemValText("Raw", "Raw");
            slTypeMaterial.AddItemValText("Mature", "Processed");


            ViewBag.TypeMaterial = slTypeMaterial.ToSelectList();
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PRODUCTID,PRODUCTNAME,PRODUCTDESCRIPTION,PRODUCTIMAGEPATH,TypeMaterial")] PRODUCT pRODUCT)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    db.PRODUCT.Add(pRODUCT);
                    db.SaveChanges();
                    SuccessMessagesAdd("Adding Product Success");
                    return RedirectToAction("Index");
                }
                catch (System.Exception ex)
                {
                    ErrorMessagesAdd(ex.Message);
                }

            }

            var slTypeMaterial = new List<SelectListItem>();
            slTypeMaterial.AddBlank();
            slTypeMaterial.AddItemValText("Raw", "Raw");
            slTypeMaterial.AddItemValText("Mature", "Processed");
            ViewBag.TypeMaterial = slTypeMaterial.ToSelectList(pRODUCT.TypeMaterial);
            return View(pRODUCT);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PRODUCTID,PRODUCTNAME,PRODUCTDESCRIPTION,PRODUCTIMAGEPATH")] PRODUCT pRODUCT)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var productToSave = db.PRODUCT.Find(pRODUCT.PRODUCTID);
                    productToSave.PRODUCTDESCRIPTION = pRODUCT.PRODUCTDESCRIPTION;
                    productToSave.PRODUCTIMAGEPATH = pRODUCT.PRODUCTIMAGEPATH;

                    db.Entry(productToSave).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    SuccessMessagesAdd("Edit Product Berhasil");
                    return RedirectToAction("Index");
                }
                catch (System.Exception ex)
                {
                    ErrorMessagesAdd(ex.Message);
                    return RedirectToAction("Index");
                    throw;
                }

            }
            return View(pRODUCT);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PRODUCT pRODUCT = db.PRODUCT.Find(id);
            db.PRODUCT.Remove(pRODUCT);
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
