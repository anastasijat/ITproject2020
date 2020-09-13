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
    public class BuildingsApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/BuildingsApi
        public IQueryable<Building> GetBuildings()
        {
            return db.Buildings;
        }

        // GET: api/BuildingsApi/5
        [ResponseType(typeof(Building))]
        public IHttpActionResult GetBuilding(int id)
        {
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return NotFound();
            }

            return Ok(building);
        }

        // PUT: api/BuildingsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBuilding(int id, Building building)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != building.BuildingId)
            {
                return BadRequest();
            }

            db.Entry(building).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingExists(id))
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

        // POST: api/BuildingsApi
        [ResponseType(typeof(Building))]
        public IHttpActionResult PostBuilding(Building building)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Buildings.Add(building);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = building.BuildingId }, building);
        }

        // DELETE: api/BuildingsApi/5
        [ResponseType(typeof(Building))]
        public IHttpActionResult DeleteBuilding(int id)
        {
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return NotFound();
            }

            db.Buildings.Remove(building);
            db.SaveChanges();

            return Ok(building);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BuildingExists(int id)
        {
            return db.Buildings.Count(e => e.BuildingId == id) > 0;
        }
    }
}