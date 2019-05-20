using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Repository.Application.DataModel;

namespace Web.MainApplication.Controllers.OData
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Repository.Application.DataModel;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<TransactionProduct>("TransactionProductsOdata");
    builder.EntitySet<PRODUCT>("PRODUCT"); 
    builder.EntitySet<ProductAdjustment>("ProductAdjustment"); 
    builder.EntitySet<ProductConvertion>("ProductConvertion"); 
    builder.EntitySet<RITASE>("RITASE"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class TransactionProductsOdataController : ODataController
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();

        // GET: odata/TransactionProductsOdata
        [EnableQuery]
        public IQueryable<TransactionProduct> GetTransactionProductsOdata()
        {
            return db.TransactionProduct;
        }

        // GET: odata/TransactionProductsOdata(5)
        [EnableQuery]
        public SingleResult<TransactionProduct> GetTransactionProduct([FromODataUri] long key)
        {
            return SingleResult.Create(db.TransactionProduct.Where(transactionProduct => transactionProduct.Id == key));
        }

        // PUT: odata/TransactionProductsOdata(5)
        public IHttpActionResult Put([FromODataUri] long key, Delta<TransactionProduct> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TransactionProduct transactionProduct = db.TransactionProduct.Find(key);
            if (transactionProduct == null)
            {
                return NotFound();
            }

            patch.Put(transactionProduct);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(transactionProduct);
        }

        // POST: odata/TransactionProductsOdata
        public IHttpActionResult Post(TransactionProduct transactionProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TransactionProduct.Add(transactionProduct);
            db.SaveChanges();

            return Created(transactionProduct);
        }

        // PATCH: odata/TransactionProductsOdata(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] long key, Delta<TransactionProduct> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TransactionProduct transactionProduct = db.TransactionProduct.Find(key);
            if (transactionProduct == null)
            {
                return NotFound();
            }

            patch.Patch(transactionProduct);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(transactionProduct);
        }

        // DELETE: odata/TransactionProductsOdata(5)
        public IHttpActionResult Delete([FromODataUri] long key)
        {
            TransactionProduct transactionProduct = db.TransactionProduct.Find(key);
            if (transactionProduct == null)
            {
                return NotFound();
            }

            db.TransactionProduct.Remove(transactionProduct);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/TransactionProductsOdata(5)/PRODUCT
        [EnableQuery]
        public SingleResult<PRODUCT> GetPRODUCT([FromODataUri] long key)
        {
            return SingleResult.Create(db.TransactionProduct.Where(m => m.Id == key).Select(m => m.PRODUCT));
        }

        // GET: odata/TransactionProductsOdata(5)/ProductAdjustment
        [EnableQuery]
        public SingleResult<ProductAdjustment> GetProductAdjustment([FromODataUri] long key)
        {
            return SingleResult.Create(db.TransactionProduct.Where(m => m.Id == key).Select(m => m.ProductAdjustment));
        }

        // GET: odata/TransactionProductsOdata(5)/ProductConvertion
        [EnableQuery]
        public SingleResult<ProductConvertion> GetProductConvertion([FromODataUri] long key)
        {
            return SingleResult.Create(db.TransactionProduct.Where(m => m.Id == key).Select(m => m.ProductConvertion));
        }

        // GET: odata/TransactionProductsOdata(5)/RITASE
        [EnableQuery]
        public SingleResult<RITASE> GetRITASE([FromODataUri] long key)
        {
            return SingleResult.Create(db.TransactionProduct.Where(m => m.Id == key).Select(m => m.RITASE));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransactionProductExists(long key)
        {
            return db.TransactionProduct.Count(e => e.Id == key) > 0;
        }
    }
}
