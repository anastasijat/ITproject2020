using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ITproject2020.Models;
using Microsoft.AspNet.Identity;

namespace ITproject2020.Controllers
{
    public class ReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = "Admin")]
        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(u => u.User).Include(u=>u.Seat.Performance).ToList();
            return View(reservations.ToList());
        }

        // GET: Reservations/Create
        [Authorize(Roles = "User,Admin")]
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Performance performance = db.Performances.Find(id);
            var performance = db.Performances.Include(p => p.Seats).Include(p => p.Building).Where(p => p.PerformanceId == id).Single();
            SeatListModel model = new SeatListModel();
            model.Performance = performance;
            model.availableSeats = performance.Seats.Where(s=>s.status==false).ToList();
            if (performance == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin")]
        public ActionResult Create(SeatListModel model)
        {
            if (ModelState.IsValid)
            {

                var nowSelected = new List<Reservation>();
                IList<int> seats = model.selectedSeats;
                var userId = User.Identity.GetUserId();
               
                for (int i = 0; i < seats.Count; i++)
                {
                    Reservation reservation = new Reservation();
                    reservation.SeatId = seats[i];
                    var s = db.Seats.Find(seats[i]);
                    s.status = true;
                    reservation.Seat = db.Seats.Find(seats[i]);
                    reservation.User = db.Users.Find(userId);

                    nowSelected.Add(reservation);
                    db.Reservations.Add(reservation);
                    db.SaveChanges();
                }

               
       
                return RedirectToAction("SuccessfulReservation");
            }
            return RedirectToAction("Index");
        }
        public ActionResult SuccessfulReservation()
        {
            return View();
        }

        public ActionResult UserReservations()
        {
            var userId = User.Identity.GetUserId();
            var reservations = db.Reservations.Include(u=>u.Seat).Include(u=>u.Seat.Performance).Where(u => u.User.Id == userId).ToList();
            return View(reservations);
        }

        
        // GET: Reservations/Delete/5
        [Authorize(Roles = "User,Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Reservation reservation = db.Reservations.Find(id);
            var reservation = db.Reservations.Include(s => s.Seat).Include(s=>s.Seat.Performance).Where(s => s.ReservationId == id).Single();
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Seats.Find(reservation.SeatId).status = false;
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("UserReservations");
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
