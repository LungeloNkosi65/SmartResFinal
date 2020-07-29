using System.Web.Mvc;
using SmartRes_Official2019Data;
using System.Linq;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SmartRes_Official2019.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<ApplicationUser> UserManager { get; set; }

         public HomeController()
        {
            UserManager = new UserManager<ApplicationUser>(store: new UserStore<ApplicationUser>(context: new ApplicationDbContext()));
        }
        //public ActionResult Index()
        //{
        //    if (Session["user"] != null)
        //    {
        //        return Redirect("/chat");
        //    }

        //    return View();
        //}

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ListAllEmployees()
        {
            return View(db.employees.ToList());
        }
        public ActionResult Index1()
        {
            return View();
        }
        public ActionResult CalenderEvents()
        {
            return View();
        }
        public JsonResult GetEvents()
        {
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                var events = dc.Events.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        [HttpPost]
        public JsonResult SaveEvent(Event e)
        {
            var status = false;
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                if (e.EventID > 0)
                {
                    //Update the event
                    var v = dc.Events.Where(a => a.EventID == e.EventID).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = e.Subject;
                        v.Start = e.Start;
                        v.End = e.End;
                        v.Description = e.Description;
                        v.IsFullDay = e.IsFullDay;
                        v.ThemeColor = e.ThemeColor;
                    }
                }
                else
                {
                    dc.Events.Add(e);
                }

                dc.SaveChanges();
                status = true;

            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                var v = dc.Events.Where(a => a.EventID == eventID).FirstOrDefault();
                if (v != null)
                {
                    dc.Events.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult MyUnisite()
        {
            return View();
        }

        public ActionResult MarksPortal()
        {
            return View();
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Creat(CheckeOut checkout,string id)
        {
           var    username = User.Identity.GetUserName();
            var studentCheckInd = db.StudentCheckIns.Where(x => x.Student.IdForOut == id).FirstOrDefault();
            var student = db.Students.Where(x => x.IdForOut == id).FirstOrDefault();
            checkout.CheckOutId = studentCheckInd.CheckIn_Id;
            checkout.StudentEmail = student.Email;
            checkout.ResName = student.ResName;
            checkout.RoomNumber = student.RooNumber;
            checkout.Status = "True";
            DateTime now = DateTime.UtcNow;
            checkout.CheckOutDate = now;
            checkout.DateIn = studentCheckInd.Date_In;

            //student.IdForOut = User.Identity.GetUserId();
            //db.Entry(student).State = EntityState.Modified;

            var uId = User.Identity.GetUserId();
            var emp = UserManager.RemoveFromRole(id, "Student");

            db.CheckOuts.Add(checkout);
            db.SaveChanges();




            return RedirectToAction("Index1");

            //return RedirectToAction("RemoveUser","Roles", new { userId = User.Identity.GetUserId(),});
        }

        [Authorize(Roles="SAN")]
        public ActionResult CheckBackIn(string id)
        {
            DateTime now = DateTime.UtcNow;
            var student =db.Students.Find(id);

            var CheckedOut = db.CheckOuts.Where(x => x.StudentEmail.Equals(student.Email)).FirstOrDefault();
            CheckedOut.Status = "False";
            CheckedOut.DateIn = now;
            var emp = UserManager.AddToRole(student.IdForOut, "Student");
            db.Entry(CheckedOut).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index1");
        }

      


    }
}