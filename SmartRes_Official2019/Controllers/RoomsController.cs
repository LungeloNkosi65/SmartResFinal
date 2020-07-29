using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SmartRes_Official2019Logic;
using SmartRes_Official2019Data;
using System;

namespace SmartRes_Official2019.Controllers
{
    public class RoomsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        Logic lo = new Logic();
        BsLogic Bs = new BsLogic();

        // GET: Rooms
        public ActionResult Index(int? id, string sortOrder, string searchString)
        {
            var students = from s in db.Rooms
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Status.Contains(searchString)||s.RoomNumber.Contains(searchString));
                                       
                return View(students.ToList());

            }
            List<Room> room = db.Rooms.Where(x => x.ResId == id).ToList();

            //var rooms = db.Rooms.Include(r => r.Residence).Include(r => r.RoomType);
            return View(Bs.LIstRooms().Where(x=>x.ResId==id).ToList());
        }

        public ActionResult RoomView()
        {

            var residence = db.Residences.ToList();
            return View(residence);
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName");
            ViewBag.RtypeId = new SelectList(db.RoomTypes, "RtypeId", "RmType");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoomId,ResId,RtypeId,RoomNumber,Floor,Status,Space")] Room room)
        {
          
            if (ModelState.IsValid) 
            {
                if (lo.RoomNoExists(room.RoomNumber,room.ResId))
                {
                    ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName", room.ResId);
                    ViewBag.RtypeId = new SelectList(db.RoomTypes, "RtypeId", "RmType", room.RtypeId);
                    TempData["message"] = "Room Number already exists.";
                    return View("Create");
                }
                if (room.Floor>=0)
                {
                   
                    room.Status = "Available";
                    room.Space = room.getSpace();
                    Bs.AddRoom(room);
                    return RedirectToAction("RoomView");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Floor Number");

                }

            }

            ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName", room.ResId);
            ViewBag.RtypeId = new SelectList(db.RoomTypes, "RtypeId", "RmType", room.RtypeId);
            return View(room);
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = Bs.GetTRoom(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName", room.ResId);
            ViewBag.RtypeId = new SelectList(db.RoomTypes, "RtypeId", "RmType", room.RtypeId);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RoomId,ResId,RtypeId,RoomNumber,Floor,Status,Space")] Room room)
        {
            if (ModelState.IsValid)
            {

                Bs.UpdateRoom(room);
                return RedirectToAction("Index");
            }
            ViewBag.ResId = new SelectList(db.Residences, "ResId", "ResName", room.ResId);
            ViewBag.RtypeId = new SelectList(db.RoomTypes, "RtypeId", "RmType", room.RtypeId);
            return View(room);
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = Bs.GetTRoom(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Room room = Bs.GetTRoom(id);

            Bs.RemoveRoom(room);
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
