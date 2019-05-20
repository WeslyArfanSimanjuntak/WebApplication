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
using EntityState = System.Data.Entity.EntityState;

namespace Web.MainApplication.Controllers
{
    public class NostroBankController : ApiController
    {
        private DB_TritsurEntities db = new DB_TritsurEntities();

        // GET: api/NostroBank
        public IQueryable<NostroBank> GetNostroBank()
        {
            return db.NostroBank;
        }

        // GET: api/NostroBank/5
        [ResponseType(typeof(NostroBank))]
        public IHttpActionResult GetNostroBank(string id)
        {
            NostroBank nostroBank = db.NostroBank.Find(id);
            if (nostroBank == null)
            {
                return NotFound();
            }

            return Ok(nostroBank);
        }

        // PUT: api/NostroBank/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNostroBank(string id, NostroBank nostroBank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nostroBank.CodeNostroBank)
            {
                return BadRequest();
            }

            db.Entry(nostroBank).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NostroBankExists(id))
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

        // POST: api/NostroBank
        [ResponseType(typeof(NostroBank))]
        public IHttpActionResult PostNostroBank(NostroBank nostroBank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.NostroBank.Add(nostroBank);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (NostroBankExists(nostroBank.CodeNostroBank))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = nostroBank.CodeNostroBank }, nostroBank);
        }

        // DELETE: api/NostroBank/5
        [ResponseType(typeof(NostroBank))]
        public IHttpActionResult DeleteNostroBank(string id)
        {
            NostroBank nostroBank = db.NostroBank.Find(id);
            if (nostroBank == null)
            {
                return NotFound();
            }

            db.NostroBank.Remove(nostroBank);
            db.SaveChanges();

            return Ok(nostroBank);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NostroBankExists(string id)
        {
            return db.NostroBank.Count(e => e.CodeNostroBank == id) > 0;
        }
    }
}