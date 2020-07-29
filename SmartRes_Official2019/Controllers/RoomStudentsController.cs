using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SmartRes_Official2019Logic;
using  SmartRes_Official2019Data;

namespace SmartRes_Official2019.Controllers
{
    public class RoomStudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        BsLogic Bs = new BsLogic();
        // GET: RoomStudents
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var roomStudents = db.RoomStudents;
            if(User.IsInRole("SystemAdmin"))
            {
                return View(Bs.ListRoomStudent());

            }
            else
            {
                return View(Bs.ListRoomStudent().Where(x=>x.StudId==userId));

            }
        }

        // GET: RoomStudents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomStudent roomStudent = db.RoomStudents.Find(id);
            if (roomStudent == null)
            {
                return HttpNotFound();
            }
            return View(roomStudent);
        }

        // GET: RoomStudents/Create
        public ActionResult Create()
        {
            ViewBag.RoomId = new SelectList(db.Rooms.Where(x=>x.Status!="Taken"), "RoomId", "RoomNumber");
            var UserId = User.Identity.GetUserId();
            if(User.IsInRole("SystemAdmin "))
            {
                ViewBag.StudId = new SelectList(db.Students, "StudId", "Name");


            }
            else
            {
                ViewBag.StudId = new SelectList(db.Students.Where(x => x.StudId == UserId), "StudId", "Name");


            }
            return View();
        }

        // POST: RoomStudents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RStudentId,StudId,RoomId,Date_Assigned,em")] RoomStudent roomStudent)
        {
            if (ModelState.IsValid)

            {
                roomStudent.em = User.Identity.GetUserName();
                if (roomStudent.CheckStudent(roomStudent.StudId) == true)
                {
                    roomStudent.updateSpace();
                    roomStudent.checkAvailabilty();
                       
                        roomStudent.Date_Assigned = DateTime.Now.Date;
                        if(roomStudent.checkAvailabilty()==true)
                        {
                         
                            roomStudent.UpdateStatus();
                        Bs.AddRoomStudent(roomStudent);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Room Is Already Taken");

                        }

                    
                    
                } 

                else
                {
                    ModelState.AddModelError("", "Student  Is Already Assigned To Room");

                }



            }

            var UserId = User.Identity.GetUserId();

            ViewBag.RoomId = new SelectList(db.Rooms.Where(x=>x.Status!="Taken"), "RoomId", "RoomNumber", roomStudent.RoomId);
            if(User.IsInRole("SystemAdmin"))
            {
                ViewBag.StudId = new SelectList(db.Students, "StudId", "Name", roomStudent.StudId);


            }
            else
            {
                ViewBag.StudId = new SelectList(db.Students.Where(x => x.Email == UserId), "StudId", "Name", roomStudent.StudId);


            }
            return View(roomStudent);
        }

        // GET: RoomStudents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomStudent roomStudent = db.RoomStudents.Find(id);
            if (roomStudent == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber", roomStudent.RoomId);
            ViewBag.StudId = new SelectList(db.Students, "StudId", "Name", roomStudent.StudId);
            return View(roomStudent);
        }

        // POST: RoomStudents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RStudentId,StudId,RoomId,Date_Assigned")] RoomStudent roomStudent)
        {
            if (ModelState.IsValid)
            {
                Bs.UpdateRoomStudent(roomStudent);
                return RedirectToAction("Index");
            }
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber", roomStudent.RoomId);
            ViewBag.StudId = new SelectList(db.Students, "StudId", "Name", roomStudent.StudId);
            return View(roomStudent);
        }

        // GET: RoomStudents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomStudent roomStudent = Bs.GetTRoomStudent(id);
            if (roomStudent == null)
            {
                return HttpNotFound();
            }
            return View(roomStudent);
        }

        // POST: RoomStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomStudent roomStudent = Bs.GetTRoomStudent(id);

            Bs.RemoveRoomStudent(roomStudent);
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
