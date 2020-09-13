using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ITproject2020.Models;

namespace ITproject2020.Controllers
{
    public class PerformancesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Performances
        [AllowAnonymous]
        public ActionResult Index()
        {
            var performances = db.Performances.Include(p => p.Building);
            return View(performances.ToList());
        }

        // GET: Performances/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Performance performance = db.Performances.Find(id);
            var performance = db.Performances.Include(p => p.Building).Where(p => p.PerformanceId == id).Single();
            if (performance == null)
            {
                return HttpNotFound();
            }
            return View(performance);
        }

        // GET: Performances/Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            ViewBag.BuildingId = new SelectList(db.Buildings, "BuildingId", "BuildingName");
            return View();
        }

        // POST: Performances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "PerformanceId,PerformanceName,Description,BuildingId,Price,PerformanceDateTime,ImageURL")] Performance performance)
        {
            if (ModelState.IsValid)
            {
                db.Performances.Add(performance);
                db.SaveChanges();

                var performanceAndBuilding = db.Performances.Include(p => p.Building).Where(p => p.PerformanceId == performance.PerformanceId).Single();

                //int num = performance.Building.NumberOfSeats;

                IList<Seat> seatList = new List<Seat>();



                for (int i = 0; i < performanceAndBuilding.Building.NumberOfSeats; i++)
                {
                    seatList.Add(new Seat(i, performance.PerformanceId, performance));
                }

                //seatList.Add(new Seat() { PerformanceId = 1, SeatNumber = 1, status = false, Performance = performance });
                db.Seats.AddRange(seatList);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.BuildingId = new SelectList(db.Buildings, "BuildingId", "BuildingName", performance.BuildingId);

            return View(performance);
        }

        // GET: Performances/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = db.Performances.Find(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuildingId = new SelectList(db.Buildings, "BuildingId", "BuildingName", performance.BuildingId);
            return View(performance);
        }

        // POST: Performances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "PerformanceId,PerformanceName,Description,BuildingId,Price,PerformanceDateTime,ImageURL")] Performance performance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(performance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuildingId = new SelectList(db.Buildings, "BuildingId", "BuildingName", performance.BuildingId);
            return View(performance);
        }

        // GET: Performances/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = db.Performances.Find(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            return View(performance);
        }

        // POST: Performances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Performance performance = db.Performances.Find(id);
            db.Performances.Remove(performance);
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
