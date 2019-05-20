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
    builder.EntitySet<DeliveryRequest>("DeliveryRequestsOData");
    builder.EntitySet<CONTRACT>("CONTRACT"); 
    builder.EntitySet<DeliveryRequestProductDetailTransaction>("DeliveryRequestProductDetailTransaction"); 
    builder.EntitySet<DeliveryOrder>("DeliveryOrder"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class DeliveryRequestsODataController : ODataController
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();

        // GET: odata/DeliveryRequestsOData
        [EnableQuery]
        public IQueryable<DeliveryRequest> GetDeliveryRequestsOData()
        {
            return db.DeliveryRequest;
        }

        // GET: odata/DeliveryRequestsOData(5)
        [EnableQuery]
        public SingleResult<DeliveryRequest> GetDeliveryRequest([FromODataUri] long key)
        {
            return SingleResult.Create(db.DeliveryRequest.Where(deliveryRequest => deliveryRequest.DeliveryRequestId == key));
        }

        // PUT: odata/DeliveryRequestsOData(5)
        public IHttpActionResult Put([FromODataUri] long key, Delta<DeliveryRequest> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DeliveryRequest deliveryRequest = db.DeliveryRequest.Find(key);
            if (deliveryRequest == null)
            {
                return NotFound();
            }

            patch.Put(deliveryRequest);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryRequestExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(deliveryRequest);
        }

        // POST: odata/DeliveryRequestsOData
        public IHttpActionResult Post(DeliveryRequest deliveryRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DeliveryRequest.Add(deliveryRequest);
            db.SaveChanges();

            return Created(deliveryRequest);
        }

        // PATCH: odata/DeliveryRequestsOData(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] long key, Delta<DeliveryRequest> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DeliveryRequest deliveryRequest = db.DeliveryRequest.Find(key);
            if (deliveryRequest == null)
            {
                return NotFound();
            }

            patch.Patch(deliveryRequest);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryRequestExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(deliveryRequest);
        }

        // DELETE: odata/DeliveryRequestsOData(5)
        public IHttpActionResult Delete([FromODataUri] long key)
        {
            DeliveryRequest deliveryRequest = db.DeliveryRequest.Find(key);
            if (deliveryRequest == null)
            {
                return NotFound();
            }

            db.DeliveryRequest.Remove(deliveryRequest);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/DeliveryRequestsOData(5)/CONTRACT
        [EnableQuery]
        public SingleResult<CONTRACT> GetCONTRACT([FromODataUri] long key)
        {
            return SingleResult.Create(db.DeliveryRequest.Where(m => m.DeliveryRequestId == key).Select(m => m.CONTRACT));
        }

        // GET: odata/DeliveryRequestsOData(5)/DeliveryRequestProductDetailTransaction
        [EnableQuery]
        public IQueryable<DeliveryRequestProductDetailTransaction> GetDeliveryRequestProductDetailTransaction([FromODataUri] long key)
        {
            return db.DeliveryRequest.Where(m => m.DeliveryRequestId == key).SelectMany(m => m.DeliveryRequestProductDetailTransaction);
        }

        // GET: odata/DeliveryRequestsOData(5)/DeliveryOrder
        [EnableQuery]
        public IQueryable<DeliveryOrder> GetDeliveryOrder([FromODataUri] long key)
        {
            return db.DeliveryRequest.Where(m => m.DeliveryRequestId == key).SelectMany(m => m.DeliveryOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeliveryRequestExists(long key)
        {
            return db.DeliveryRequest.Count(e => e.DeliveryRequestId == key) > 0;
        }
    }
}
