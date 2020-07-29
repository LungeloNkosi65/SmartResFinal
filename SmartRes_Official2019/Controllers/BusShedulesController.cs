using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using  SmartRes_Official2019Data;
using System.Data.Entity;

namespace SmartRes_Official2019.Controllers
{
    public class BusShedulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BusSchedules
        public ActionResult Index(int? id)
        {

            return View(db.BusShedules.Where(x => x.WkdId == id).OrderBy(x => x.CurrDayofWeek).ToList());
        }
        public ActionResult WeekView()
        {
            return View(db.WeekDays.OrderBy(x => x.CurrDayofWeek).ToList());
        }
        public ActionResult ScheduleView()
        {

            //var d = DayOfWeek.;
            string dt = DateTime.Now.DayOfWeek.ToString();
            BusShedule bs = new BusShedule();
            if (dt == "Monday")
            {
                return View(db.BusShedules.Where(x => x.CurrDayofWeek == "Monday").ToList());
            }
            else if (dt == "Tuesday")
            {
                return View(db.BusShedules.Where(x => x.CurrDayofWeek == "Tuesday").ToList());
            }
            else if (dt == "Wednesday")
            {
                return View(db.BusShedules.Where(x => x.CurrDayofWeek == "Wednesday").ToList());
            }

            else if (dt == "Thursday")
            {
                return View(db.BusShedules.Where(x => x.CurrDayofWeek == "Thursday").ToList());
            }
            else if (dt == "Friday")
            {
                return View(db.BusShedules.Where(x => x.CurrDayofWeek == "Friday").ToList());
            }
            else if (dt == "Saturday")
            {
                return View(db.BusShedules.Where(x => x.CurrDayofWeek == "Saturday").ToList());
            }
            else if (dt == "Sunday")
            {
                return View(db.BusShedules.Where(x => x.CurrDayofWeek == "Sunday").ToList());
            }
            else
            {

                ModelState.AddModelError("", "No Buses Available Today");

                return View();

            }







        }
        // GET: BusSchedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusShedule busSchedule = db.BusShedules.Find(id);
            if (busSchedule == null)
            {
                return HttpNotFound();
            }
            return View(busSchedule);
        }

        // GET: BusSchedules/Create
        public ActionResult Create()
        {
            ViewBag.WkdId = new SelectList(db.WeekDays, "WkdId", "CurrDayofWeek");

            return View();
        }

        // POST: BusSchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BusShedule busSchedule)
        {
            if (ModelState.IsValid)
            {
                var tm = busSchedule.TimeODay.Split(';');

                foreach (var item in tm)
                {
                    busSchedule.CurrDayofWeek = busSchedule.getDay();

                    busSchedule.TimeODay = item;
                    db.BusShedules.Add(busSchedule);
                    db.SaveChanges();
                }

                return RedirectToAction("ScheduleView");
            }
            ViewBag.WkdId = new SelectList(db.WeekDays, "WkdId", "CurrDayofWeek", busSchedule.WkdId);

            return View(busSchedule);
        }

        // GET: BusSchedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusShedule busSchedule = db.BusShedules.Find(id);
            if (busSchedule == null)
            {
                return HttpNotFound();
            }
            return View(busSchedule);
        }

        // POST: BusSchedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ScheduleId,CurrDayofWeek,BusNumber,TimeODay,Destination")] BusShedule busSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(busSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(busSchedule);
        }

        // GET: BusSchedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusShedule busSchedule = db.BusShedules.Find(id);
            if (busSchedule == null)
            {
                return HttpNotFound();
            }
            return View(busSchedule);
        }

        // POST: BusSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BusShedule busSchedule = db.BusShedules.Find(id);
            db.BusShedules.Remove(busSchedule);
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
