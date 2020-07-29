using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using  SmartRes_Official2019Data;
using SmartRes_Official2019Logic;
using PagedList;
namespace SmartRes_Official2019.Controllers
{
    public class TemporaryBookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        Logic lo = new Logic();
        BsLogic Bs = new BsLogic();




        // GET: TemporaryBookings
        public ActionResult Index(int? page)
        {
            var userId = User.Identity.GetUserName();
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            var temporaryBookings = db.TemporaryBookings;
            if (User.IsInRole("SystemAdmin"))
            {
                return View(Bs.ListTemporaryBooking());

            }
            else
            {
                return View(Bs.ListTemporaryBooking().Where(x => x.BookedUniversity == userId));

            }
        }

        // GET: TemporaryBookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemporaryBooking temporaryBooking = db.TemporaryBookings.Find(id);
            if (temporaryBooking == null)
            {
                return HttpNotFound();
            }
            return View(temporaryBooking);
        }

        // GET: TemporaryBookings/Create
        public ActionResult Create()
        {
            ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName");
            return View();
        }

        // POST: TemporaryBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( TemporaryBooking temporaryBooking)
        {
          
            if (ModelState.IsValid)
            {
                var Student = db.Students_Dummies.Find(temporaryBooking.StudentNumber);

                if (lo.CheckGender(temporaryBooking.ResId, Student.Gender)==false)
                {
                    ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName", temporaryBooking.ResId);
                    TempData["message"] = "The students gender is not appropriate for this Residence ";
                    return View("Create");
                }
                if (lo.exists(temporaryBooking.StudentNumber))
                {
                    ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName", temporaryBooking.ResId);
                    TempData["message"] = "You can not book twise for the same student.";
                    return View("Create");
                }
                

                if (Student==null)
                {
                    ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName", temporaryBooking.ResId);
                    TempData["message"] = "Invalid Student Number, please check student number";
                    return View("Create");
                }
                var userId = User.Identity.GetUserName();
                temporaryBooking.BookedUniversity = userId;
                temporaryBooking.IDNumber = Student.IdNumber;
                temporaryBooking.Gender = Student.Gender;
                temporaryBooking.OTPCode = temporaryBooking.generateOTPCode();
                temporaryBooking.BookingDate = DateTime.Now;
                var bookings = db.TemporaryBookings.ToList();


                temporaryBooking.UpdateCapacity();
                Bs.AddTemporaryBooking(temporaryBooking);

                EmailService em = new EmailService();
                em.sendOTP(Student.StudentEmail, temporaryBooking.generateOTPCode(), "Verification Code");
                return RedirectToAction("Index");
            }

            ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName", temporaryBooking.ResId);
            return View(temporaryBooking);
        }

        // GET: TemporaryBookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemporaryBooking temporaryBooking = db.TemporaryBookings.Find(id);
            if (temporaryBooking == null)
            {
                return HttpNotFound();
            }
            ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName", temporaryBooking.ResId);
            return View(temporaryBooking);
        }

        // POST: TemporaryBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( TemporaryBooking temporaryBooking)
        {
            if (ModelState.IsValid)
            {
                Bs.UpdateTemporaryBooking(temporaryBooking);
                return RedirectToAction("Index");
            }
            ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName", temporaryBooking.ResId);
            return View(temporaryBooking);
        }

        // GET: TemporaryBookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemporaryBooking temporaryBooking = Bs.GetTemporaryBooking(id);
            if (temporaryBooking == null)
            {
                return HttpNotFound();
            }
            return View(temporaryBooking);
        }

        // POST: TemporaryBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TemporaryBooking temporaryBooking = Bs.GetTemporaryBooking(id);

            Bs.RemoveTemporaryBooking(temporaryBooking);
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
