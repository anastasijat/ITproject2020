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
using ITproject2020.Models;

namespace ITproject2020.Controllers
{
    public class PerformancesApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PerformancesApi
        public IQueryable<Performance> GetPerformances()
        {
            return db.Performances;
        }

        // GET: api/PerformancesApi/5
        [ResponseType(typeof(Performance))]
        public IHttpActionResult GetPerformance(int id)
        {
            Performance performance = db.Performances.Find(id);
            if (performance == null)
            {
                return NotFound();
            }

            return Ok(performance);
        }

        // PUT: api/PerformancesApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPerformance(int id, Performance performance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != performance.PerformanceId)
            {
                return BadRequest();
            }

            db.Entry(performance).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerformanceExists(id))
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

        // POST: api/PerformancesApi
        [ResponseType(typeof(Performance))]
        public IHttpActionResult PostPerformance(Performance performance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Performances.Add(performance);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = performance.PerformanceId }, performance);
        }

        // DELETE: api/PerformancesApi/5
        [ResponseType(typeof(Performance))]
        public IHttpActionResult DeletePerformance(int id)
        {
            Performance performance = db.Performances.Find(id);
            if (performance == null)
            {
                return NotFound();
            }

            db.Performances.Remove(performance);
            db.SaveChanges();

            return Ok(performance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PerformanceExists(int id)
        {
            return db.Performances.Count(e => e.PerformanceId == id) > 0;
        }
    }
}