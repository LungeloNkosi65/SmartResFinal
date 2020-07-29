using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SmartRes_Official2019Logic;
using  SmartRes_Official2019Data;

namespace SmartRes_Official2019.Controllers
{
    public class StudentCheckInsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        Logic lo = new Logic();
        BsLogic Bs = new BsLogic();

        // GET: StudentCheckIns
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var stud = db.Students.ToList();
            var studentCheckIns = db.StudentCheckIns;
            if (User.IsInRole("SystemAdmin") || User.IsInRole("SAN") ||User.IsInRole("Employee"))
            {
                return View(Bs.ListStudentCheckIn());

            }
            else
            {
                return View(Bs.ListStudentCheckIn().Where(x=>x.StudId==userId));

            }
        }
        public ActionResult Home2()
        {
            return View();
        }

       
        // GET: StudentCheckIns/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentCheckIn studentCheckIn = db.StudentCheckIns.Find(id);
            if (studentCheckIn == null)
            {
                return HttpNotFound();
            }
            return View(studentCheckIn);
        }

        // GET: StudentCheckIns/Create
        [Authorize(Roles ="Student")]
        public ActionResult Create()
        {
            DateTime now = DateTime.UtcNow;

            StudentCheckIn studentCheckIn = new StudentCheckIn();
            var userId = User.Identity.GetUserName();
            var roomResult = studentCheckIn.checkRoomStatus();
            var student = db.Students.ToList().Find(x => x.Email == User.Identity.GetUserName());
            Student stdent = new Student();
            var resid = lo.getStudentRes((student.IdNumber));
            var id = stdent.IdNumber;
           
            if (lo.ChechIdForCheckIn(lo.getId(id.ToString()))==false)
            {
                if (lo.checkForCheckIn(userId))
                {
                    TempData["message"] = "You have already checked in";
                    return RedirectToAction("Index", "Students");
                }
                else
                {
                    ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName");
                    ViewBag.RoomId = new SelectList(db.Rooms.Where(x => x.Status == "Available" && x.Residence.ResId == resid), "RoomId", "RoomNumber");
                    if (User.IsInRole("SAN"))
                    {
                        ViewBag.StudId = new SelectList(db.Students, "StudId", "StudentNumber");

                    }
                    else if (User.IsInRole("Student"))
                    {
                        ViewBag.StudId = new SelectList(db.Students.Where(x => x.Email == userId), "StudId", "StudentNumber");


                    }

                    return View();


                }

            }
            else
            {
                TempData["message"] = "You have not been temporarily booked for";
                return View("Home2");

            }
         

        }

        // POST: StudentCheckIns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "CheckIn_Id,StudId,ResId,ResName,Date_In,RoomId")] StudentCheckIn studentCheckIn)
        {
            DateTime now = DateTime.UtcNow;

            var userId = User.Identity.GetUserId();
            var username = User.Identity.GetUserName();

            Guid guid = new Guid();
            Random r = new Random();

            if (ModelState.IsValid)
            {
                 if (studentCheckIn.CheckStudent() == true)
                    {
                        if (studentCheckIn.checkRoomStatus() == true)
                        {
                            var student = db.Students.ToList().Find(x => x.Email == User.Identity.GetUserName());
                            studentCheckIn.StudId = student.StudId;
                            var resid = lo.getStudentRes((student.IdNumber));
                            if (lo.canCheckIn(student.Email, studentCheckIn.RooNumber) == false)
                            {
                                TempData["message"] = "Cannot checkin into a " + lo.getGender(studentCheckIn.RooNumber) + "'s room.";
                                return RedirectToAction("Create");
                            }
                        if (studentCheckIn.CheckRoomGender(username) ==false)
                        {
                            TempData["message"] = "Cannot checkin into a " + lo.getGender(studentCheckIn.RooNumber) + "'s room.";

                            return RedirectToAction("Create");
                        }

                        studentCheckIn.ResId = resid;
                            
                            // studentCheckIn.UpdateCheckIn();
                            var qtyUpdate = db.ResAvailabilities.Where(x=>x.ResId==resid).FirstOrDefault();
                            qtyUpdate.CheckedIN += 1;
                            db.Entry(qtyUpdate).State = System.Data.Entity.EntityState.Modified;
                            studentCheckIn.UpdateStatus();
                            studentCheckIn.CheckIn_Id = guid.ToString() + r.Next(2, 10000).ToString();
                            studentCheckIn.ResName = lo.getResName(studentCheckIn.getStudentNUmber());
                            studentCheckIn.Date_In = now.Date;
                            studentCheckIn.RooNumber = studentCheckIn.GetRoomNumber();
                        //studentCheckIn.RoomId=studentCheckIn.RoomId
                        studentCheckIn.UpdateProfile();
                        studentCheckIn.updateSpace();
                        Bs.AddStudentCheckIn(studentCheckIn);
                        db.SaveChanges();

                        return RedirectToAction("Create", "PDFs");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Room is Already Taken");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Student is already checked in");

                    }
            }

            ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName", studentCheckIn.ResId);
            ViewBag.RoomId = new SelectList(db.Rooms.Where(x => x.Space!= 0 && x.Residence.ResId == studentCheckIn.ResId), "RoomId", "RoomNumber", studentCheckIn.RoomId);
            if (User.IsInRole("SystemAdmin"))
            {
            ViewBag.StudId = new SelectList(db.Students, "StudId", "StudentNumber", studentCheckIn.StudId);


            }
            else if (User.IsInRole("Student"))
            {
            ViewBag.StudId = new SelectList(db.Students.Where(x=>x.Email==userId), "StudId", "StudentNumber", studentCheckIn.StudId);


            }
            return View(studentCheckIn);
        }

        // GET: StudentCheckIns/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentCheckIn studentCheckIn = db.StudentCheckIns.Find(id);
            if (studentCheckIn == null)
            {
                return HttpNotFound();
            }
            ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName", studentCheckIn.ResId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber", studentCheckIn.RoomId);
            ViewBag.StudId = new SelectList(db.Students, "StudId", "StudentNumber", studentCheckIn.StudId);
            return View(studentCheckIn);
        }

        // POST: StudentCheckIns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CheckIn_Id,StudId,ResId,ResName,Date_In,RoomId")] StudentCheckIn studentCheckIn)
        {
            if (ModelState.IsValid)
            {
                Bs.UpdateStudentCheckIn(studentCheckIn);
                return RedirectToAction("Index");
            }
            ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName", studentCheckIn.ResId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber", studentCheckIn.RoomId);
            ViewBag.StudId = new SelectList(db.Students, "StudId", "StudentNumber", studentCheckIn.StudId);
            return View(studentCheckIn);
        }

        // GET: StudentCheckIns/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentCheckIn studentCheckIn = db.StudentCheckIns.Find(id);
            if (studentCheckIn == null)
            {
                return HttpNotFound();
            }
            return View(studentCheckIn);
        }

        // POST: StudentCheckIns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            //StudentCheckIn studentCheckIn = db.StudentCheckIns.Find(id);
            StudentCheckIn studentCheckIn = Bs.GetStudentCheckIn(id);

            Bs.RemoveStudentCheckIn(studentCheckIn);
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
