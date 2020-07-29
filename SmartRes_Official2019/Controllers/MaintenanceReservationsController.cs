using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartRes_Official2019Data;
using Microsoft.AspNet.Identity;

namespace SmartRes_Official2019.Controllers
{
    public class MaintenanceReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        Logic lo = new Logic();

        // GET: MaintenanceReservations
        public ActionResult Index()
        {
            return View(db.MaintenanceReservations.ToList());
        }

        // GET: MaintenanceReservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceReservation maintenanceReservation = db.MaintenanceReservations.Find(id);
            if (maintenanceReservation == null)
            {
                return HttpNotFound();
            }
            return View(maintenanceReservation);
        }

        // GET: MaintenanceReservations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MaintenanceReservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationID,ReservationDescription,ReservationDate")] MaintenanceReservation maintenanceReservation)
        {
            if (ModelState.IsValid)
            {


                var MainIssue = (from s in db.MaintenanceRequests
                             where maintenanceReservation.MaintenanceId == s.MaintenanceId
                             select s.MainIssue
                   ).FirstOrDefault();
                var Date = (from s in db.MaintenanceRequests
                            where maintenanceReservation.MaintenanceId == s.MaintenanceId
                            select s.RoomAval
                   ).FirstOrDefault();
                var Rooms = (from s in db.MaintenanceRequests
                            where maintenanceReservation.MaintenanceId == s.MaintenanceId
                            select s.RoomNumber
                   ).FirstOrDefault();
                var userName = User.Identity.GetUserName();
                //maintenanceReservation.ReservationDate = Date;
                //maintenanceReservation.ReservationDescription = MainIssue;
                //maintenanceReservation.Rooms = lo.GetRoomNumber(userName);
                //maintenanceReservation.Room = Rooms;
                db.MaintenanceReservations.Add(maintenanceReservation);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(maintenanceReservation);
        }

        // GET: MaintenanceReservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceReservation maintenanceReservation = db.MaintenanceReservations.Find(id);
            if (maintenanceReservation == null)
            {
                return HttpNotFound();
            }
            return View(maintenanceReservation);
        }

        // POST: MaintenanceReservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationID,ReservationDescription,ReservationDate")] MaintenanceReservation maintenanceReservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maintenanceReservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(maintenanceReservation);
        }

        // GET: MaintenanceReservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceReservation maintenanceReservation = db.MaintenanceReservations.Find(id);
            if (maintenanceReservation == null)
            {
                return HttpNotFound();
            }
            return View(maintenanceReservation);
        }

        // POST: MaintenanceReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MaintenanceReservation maintenanceReservation = db.MaintenanceReservations.Find(id);
            db.MaintenanceReservations.Remove(maintenanceReservation);
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
