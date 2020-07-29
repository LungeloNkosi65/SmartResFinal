using System;
using System.Collections.Generic;

using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartRes_Official2019Logic;
using  SmartRes_Official2019Data;

namespace SmartRes_Official2019.Controllers
{
    public class ResAvailabilitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        Logic lo = new Logic();
        BsLogic Bs = new BsLogic();

        // GET: ResAvailabilities
        public ActionResult Index(  string sortOrder, string searchString)
        {
            var students = from s in db.ResAvailabilities
                           select s;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Gender.Contains(searchString));
                                   
                return View(students.ToList());

            }
            var resAvailabilities = db.ResAvailabilities;
            return View(Bs.LIstResAvailability());
        }


        public ActionResult UniversityView()
        {

            var universities = db.Universities.ToList();
            return View(universities);
        }


        // GET: ResAvailabilities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResAvailability resAvailability = db.ResAvailabilities.Find(id);
            if (resAvailability == null)
            {
                return HttpNotFound();
            }
            return View(resAvailability);
        }

        // GET: ResAvailabilities/Create
        public ActionResult Create()
        {
            ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName");
            return View();
        }

        // POST: ResAvailabilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResAvsilId,ResId,NumAvailable,CheckedIN,BookedSpaces,Quantity")] ResAvailability resAvailability)
        {
            
            if (ModelState.IsValid)
            {
                resAvailability.Quantity = resAvailability.getCapacity();
                resAvailability.NumAvailable = resAvailability.getSpace();
                resAvailability.BookedSpaces = 0;
                resAvailability.CheckedIN = 0;
                resAvailability.Gender = lo.getResGender(resAvailability.ResId);
                Bs.AddResAvailability(resAvailability);
                return RedirectToAction("Index");
            }

            ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName", resAvailability.ResId);
            return View(resAvailability);
        }

        // GET: ResAvailabilities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResAvailability resAvailability = db.ResAvailabilities.Find(id);
            if (resAvailability == null)
            {
                return HttpNotFound();
            }
            ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName", resAvailability.ResId);
            return View(resAvailability);
        }

        // POST: ResAvailabilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResAvsilId,ResId,NumAvailable,CheckedIN,BookedSpaces,Quantity")] ResAvailability resAvailability)
        {
            if (ModelState.IsValid)
            {
                Bs.UpdateResAvailability(resAvailability);
                return RedirectToAction("Index");
            }
            ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName", resAvailability.ResId);
            return View(resAvailability);
        }

        // GET: ResAvailabilities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResAvailability resAvailability = Bs.GetTResAvailability(id);
            if (resAvailability == null)
            {
                return HttpNotFound();
            }
            return View(resAvailability);
        }

        // POST: ResAvailabilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ResAvailability resAvailability = Bs.GetTResAvailability(id);
            Bs.RemoveResAvailability(resAvailability);
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
