using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SmartRes_Official2019Logic;
using SmartRes_Official2019Data;

namespace SmartRes_Official2019.Controllers
{
    public class Check_InController : Controller
    {
       
        private ApplicationDbContext db = new ApplicationDbContext();
        Logic lo = new Logic();
        BsLogic Bs = new BsLogic();


        // GET: Check_In
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var check_In = db.Check_In;
            if(User.IsInRole("Student"))
            {
                return View(Bs.LIstChk().Where(x=>x.StudId==userId));

            }
            else
            {
                return View(Bs.LIstChk());

            }
        }

        // GET: Check_In/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Check_In check_In = db.Check_In.Find(id);
            if (check_In == null)
            {
                return HttpNotFound();
            }
            return View(check_In);
        }

        // GET: Check_In/Create
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            if (User.IsInRole("Student"))
            {
                ViewBag.StudId = new SelectList(db.Students.Where(x=>x.Email==userId), "StudId", "Name");

            }
            else
            {
                ViewBag.StudId = new SelectList(db.Students, "StudId", "Name");

            }
            ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName");
            return View();
        }

        // POST: Check_In/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CheckinId,StudId,ResId,Date_In")] Check_In check_In)
        {
            var userId = User.Identity.GetUserId();
            var student = db.Students.ToList().Find(x => x.Email == User.Identity.GetUserName());
            if (ModelState.IsValid)
            {
                var resid = lo.getStudentRes((student.IdNumber));
                if (check_In.CheckStudent(check_In.StudId) == true)
                {
                    
                    check_In.ResId = resid;
                    check_In.UpdateCheckIn();
                    check_In.Date_In = DateTime.Now.Date;
                    Bs.AddCheckIn(check_In);

                    return RedirectToAction("Create", "RoomStudents");
                }
                else
                {
                    ModelState.AddModelError("", "Student is already checked in");
                }
            }
            if(User.IsInRole("Student"))
            {
                ViewBag.StudId = new SelectList(db.Students.Where(x=>x.Email==userId), "StudId", "Name", check_In.StudId);

            }
            else
            {
                ViewBag.StudId = new SelectList(db.Students, "StudId", "Name", check_In.StudId);

            }
            ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName", check_In.ResId);
            return View(check_In);
        }

        // GET: Check_In/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Check_In check_In = db.Check_In.Find(id);
            if (check_In == null)
            {
                return HttpNotFound();
            }
            ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName", check_In.ResId);
            ViewBag.StudId = new SelectList(db.Students, "StudId", "Name", check_In.StudId);
            return View(check_In);
        }

        // POST: Check_In/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Check_In check_In)
        {
            if (ModelState.IsValid)
            {
                Bs.UpdateCheckIn(check_In);
                return RedirectToAction("Index");
            }
            ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName", check_In.ResId);
            ViewBag.StudId = new SelectList(db.Students, "StudId", "Name", check_In.StudId);
            return View(check_In);
        }

        // GET: Check_In/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Check_In check_In = db.Check_In.Find(id);
            if (check_In == null)
            {
                return HttpNotFound();
            }
            return View(check_In);
        }

        // POST: Check_In/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Check_In check_In = db.Check_In.Find(id);
            Bs.RemoveCheck_In(check_In);
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
