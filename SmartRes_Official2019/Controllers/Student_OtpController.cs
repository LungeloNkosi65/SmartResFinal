using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SmartRes_Official2019Logic;
using  SmartRes_Official2019Data;
using System.Data.Entity;

namespace SmartRes_Official2019.Controllers
{
    public class Student_OtpController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        BsLogic Bs = new BsLogic();
        // GET: Student_Otp
        public ActionResult Index()
        {
            return View(Bs.ListStudent_Otp());
        }

        // GET: Student_Otp/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Otp student_Otp = db.Student_Otp.Find(id);
            if (student_Otp == null)
            {
                return HttpNotFound();
            }
            return View(student_Otp);
        }

        // GET: Student_Otp/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student_Otp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,OTP,OTP_Status,Id")] Student_Otp student_Otp)
        {
            if (ModelState.IsValid)
            {
                var bookings = db.TemporaryBookings.ToList();
                Random r = new Random();
                foreach(var item in bookings)
                {
                    if(student_Otp.CheckOTP()==false)
                    {
                       
                        Guid guid = new Guid();
                        var userId = User.Identity.GetUserId();                          
                        student_Otp.StudentId = guid.ToString()+r.Next(1,100) + r.Next(100,500);
                        student_Otp.OTP_Status = "Valid";
                        student_Otp.Id = userId;
                        Bs.AddStudent_Otp(student_Otp);
                        return RedirectToAction("Register1","Account",new { Otp=student_Otp.OTP});
                    }
                    else
                    {
                        TempData["message"] = "Invalid OTP";
                        return RedirectToAction("Create");

                    }
                    
                }
                
            }

            return View(student_Otp);
        }

        // GET: Student_Otp/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Otp student_Otp = db.Student_Otp.Find(id);
            if (student_Otp == null)
            {
                return HttpNotFound();
            }
            return View(student_Otp);
        }

        // POST: Student_Otp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,OTP,OTP_Status")] Student_Otp student_Otp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student_Otp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student_Otp);
        }

        // GET: Student_Otp/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Otp student_Otp = Bs.GetTStudent_Otp(id);
            if (student_Otp == null)
            {
                return HttpNotFound();
            }
            return View(student_Otp);
        }

        // POST: Student_Otp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student_Otp student_Otp = Bs.GetTStudent_Otp(id);
            Bs.RemoveStudent_Otp(student_Otp);
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
