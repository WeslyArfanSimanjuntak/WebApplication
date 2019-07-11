using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Repository.Application.DataModel;

namespace Web.MainApplication.Controllers
{
    public class ParameterSetupController : ApiController
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();



        // GET: api/ParameterSetup
        public IQueryable<ParameterSetup> GetParameterSetup()
        {
            return db.ParameterSetup;
        }

        // GET: api/ParameterSetup/5
        [ResponseType(typeof(ParameterSetup))]
        public IHttpActionResult GetParameterSetup(string id)
        {
            ParameterSetup parameterSetup = db.ParameterSetup.Find(id);
            if (parameterSetup == null)
            {
                return NotFound();
            }

            return Ok(parameterSetup);
        }

        // PUT: api/ParameterSetup/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParameterSetup(string id, ParameterSetup parameterSetup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parameterSetup.Name)
            {
                return BadRequest();
            }

            db.Entry(parameterSetup).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParameterSetupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ParameterSetup
        [ResponseType(typeof(ParameterSetup))]
        public IHttpActionResult PostParameterSetup(ParameterSetup parameterSetup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ParameterSetup.Add(parameterSetup);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ParameterSetupExists(parameterSetup.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = parameterSetup.Name }, parameterSetup);
        }

        // DELETE: api/ParameterSetup/5
        [ResponseType(typeof(ParameterSetup))]
        public IHttpActionResult DeleteParameterSetup(string id)
        {
            ParameterSetup parameterSetup = db.ParameterSetup.Find(id);
            if (parameterSetup == null)
            {
                return NotFound();
            }

            db.ParameterSetup.Remove(parameterSetup);
            db.SaveChanges();

            return Ok(parameterSetup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParameterSetupExists(string id)
        {
            return db.ParameterSetup.Count(e => e.Name == id) > 0;
        }
    }
}