using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartRes_Official2019Logic;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Collections;
using System.Web.Helpers;
using  SmartRes_Official2019Data;

namespace SmartRes_Official2019.Controllers
{
    public class MaintenanceRequestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        BsLogic Bs = new BsLogic();
        Logic lo = new Logic();

        public ActionResult Calender()
        {
            return View();
        }
       
        // GET: MaintenanceRequests
        public ViewResult Index(string sortOrder, string searchString)
        {
            var students = from s in db.MaintenanceRequests
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Status.Contains(searchString)
                                       || s.DateFixed.Contains(searchString) || s.DateLoged.Contains(searchString) || s.RoomNumber.Contains(searchString)|| s.MainIssue.Contains(searchString));
                return View(students);

            }
            var usernname = User.Identity.GetUserName();
            var res = lo.GetResForMaintenance(usernname);
            if (User.IsInRole("Student"))
            {
                //if (!String.IsNullOrEmpty(searchString))
                //{
                //    return View(Bs.ListMaintenanceRequest().Where(x => x.ResidenceName == res && x.RoomNumber.Contains(searchString)).ToList());

                //}else
                //{
                    return View(Bs.ListMaintenanceRequest().Where(x => x.ResidenceName == res && x.Status == "Awaiting.." ));

                //}

            }
            else
            {
                return View(Bs.ListMaintenanceRequest().Where(x=>x.Status== "Awaiting.."));

            }
           
           
        }
        public ViewResult ViewAll(string sortOrder, string searchString)
        {
            var students = from s in db.MaintenanceRequests
                           select s;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    students = students.Where(s => s.Status.Contains(searchString)
            //                           || s.DateFixed.Contains(searchString) || s.DateLoged.Contains(searchString) || s.RoomNumber.Contains(searchString) || s.MainIssue.Contains(searchString));
            //    return View(students);

            //}
            var usernname = User.Identity.GetUserName();
            var res = lo.GetResForMaintenance(usernname);
            if (User.IsInRole("Student"))
            {
                //if (!String.IsNullOrEmpty(searchString))
                //{
                //    return View(Bs.ListMaintenanceRequest().Where(x => x.ResidenceName == res && x.RoomNumber.Contains(searchString)).ToList());

                //}else
                //{
                return View(Bs.ListMaintenanceRequest().Where(x => x.ResidenceName == res));

                //}

            }
            else
            {
                return View(Bs.ListMaintenanceRequest());

            }
         

        }




        // GET: MaintenanceRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceRequest maintenanceRequest = db.MaintenanceRequests.Find(id);
            if (maintenanceRequest == null)
            {
                return HttpNotFound();
            }
            return View(maintenanceRequest);
        }

        // GET: MaintenanceRequests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MaintenanceRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( MaintenanceRequest maintenanceRequest, HttpPostedFileBase filelist)
        {
            if (ModelState.IsValid)
            {
                if (maintenanceRequest.RoomAval < DateTime.Now.Date)
                {
                    TempData["message"] = "Date can not be of the past";
                    RedirectToAction("Create", maintenanceRequest);

                }
                else
                {
                    if (filelist != null && filelist.ContentLength > 0)
                    {
                        maintenanceRequest.Image = ConvertToBytes(filelist);
                    }
                    MaintenanceReservation b = new MaintenanceReservation();
                    //b.ReservationID = 1;
                    b.ReservationDate = maintenanceRequest.RoomAval;
                    b.ReservationDescription = maintenanceRequest.MainIssue;

                    var userName = User.Identity.GetUserName();
                    maintenanceRequest.UserName = userName;
                    maintenanceRequest.DateLoged = DateTime.UtcNow.ToLongDateString();
                    maintenanceRequest.UserName = userName;
                    maintenanceRequest.Status = "Awaiting..";

                    if (User.IsInRole("Student"))
                    {
                        maintenanceRequest.RoomNumber = lo.GetRoomNumber(userName);
                        maintenanceRequest.ResidenceName = lo.GetResForMaintenance(userName);

                    }

                    Event e = new Event();
                    maintenanceRequest.MainIssue = e.Description;
                    maintenanceRequest.RoomNumber = e.Subject;
                    maintenanceRequest.RoomAval = e.Start;
                    maintenanceRequest.RoomAval = e.End;
                    Bs.AddEvents(e);
                    Bs.AddMaintenanceRequest(maintenanceRequest);
                    db.MaintenanceReservations.Add(b);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
               
            }

            return View(maintenanceRequest);
        }




        public ActionResult Approve(int id)
        {
            var apprv = db.MaintenanceRequests.Find(id);

            apprv.Status = "Approved";
            db.Entry(apprv).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "MaintenanceRequests");

        }

        public ActionResult Dissapprov(int id)
        {
            var apprv = db.MaintenanceRequests.Find(id);

            apprv.Status = "Dissapproved";
            db.Entry(apprv).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Edit2");
        }

        public ActionResult Finished(int id)
        {
            var apprv = db.MaintenanceRequests.Find(id);

            apprv.Status = "Done";
            apprv.EmployeeEmail = User.Identity.GetUserName();
            apprv.DateFixed = DateTime.UtcNow.ToLongDateString();
            db.Entry(apprv).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "MaintenanceRequests");
        }

 public byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            BinaryReader reader = new BinaryReader(file.InputStream);
            return reader.ReadBytes((int)file.ContentLength);
        }

        // GET: MaintenanceRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            var userName = User.Identity.GetUserName();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceRequest maintenanceRequest = Bs.GetTMaintenanceRequest(id);
           if (maintenanceRequest.UserName==userName)
            {
              



                if (maintenanceRequest == null)
                {
                    return HttpNotFound();
                }
                return View(maintenanceRequest);

              

                
            }
            else
            {
                TempData["message"] = "You cannot Edit someone else's report.";

                return View("Index",db.MaintenanceRequests.ToList());
            }
        }

        // POST: MaintenanceRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( MaintenanceRequest maintenanceRequest)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.GetUserName();
                //if (maintenanceRequest.UserName == userName)
                //{
                    Bs.UpdateMaintenanceRequest(maintenanceRequest);

                //}
                //else
                //{
                //    TempData["message"] = "You cannot Delete someone else's report.";

                //    return View("Edit");

                //}
                return RedirectToAction("Index");


            }
            return View(maintenanceRequest);
        }

        // GET: MaintenanceRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userName = User.Identity.GetUserName();

            MaintenanceRequest maintenanceRequest = Bs.GetTMaintenanceRequest(id);
            if (maintenanceRequest.UserName == userName)
            {
                if (maintenanceRequest == null)
                {
                    return HttpNotFound();
                }
                return View(maintenanceRequest);

            }

            else
            {
                TempData["message"] = "You cannot Delete someone else's report.";

                return View("Index",db.MaintenanceRequests.ToList());


            }

        }

        // POST: MaintenanceRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MaintenanceRequest maintenanceRequest = Bs.GetTMaintenanceRequest(id);
            Bs.RemoveMaintenanceRequest(maintenanceRequest);
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
        public ActionResult Charter()
        {
            ArrayList xValue = new ArrayList();
            ArrayList YValue = new ArrayList();

            var results = (from c in db.MaintenanceRequests select c);
            results.ToList().ForEach(rs => xValue.Add(rs.IssueCost));
            var results1 = (from c in db.MaintenanceRequests select c);
            results1.ToList().ForEach(rs => YValue.Add(rs.MainIssue));

            new Chart(width: 600, height: 400, theme: ChartTheme.Blue)
                .AddTitle("Costing")
                .AddSeries("Default", chartType: "Column", xField: "Requests", yFields: "Estimated cost", xValue: YValue, yValues: xValue)
                .SetXAxis(title: "Maintenance Requests")
                .SetYAxis(title: "Cost Of Request")
                .Write("bmp");

            return null;
        }
        public ActionResult CostStats(string sortOrder, string searchString)
        {
            var students = from s in db.MaintenanceRequests
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Status.Contains(searchString)
                                       || s.DateFixed.Contains(searchString) || s.DateLoged.Contains(searchString) || s.RoomNumber.Contains(searchString)|| s.MainIssue.Contains(searchString));
                return View(students);

            }
            var usernname = User.Identity.GetUserName();
            var res = lo.GetResForMaintenance(usernname);
           // if (User.IsInRole("Student"))
            //{
             //   return View(Bs.ListMaintenanceRequest().Where(x => x.ResidenceName == res && x.Status == "In Progress"));

          //  }
           // else
           // {
                return View(Bs.ListMaintenanceRequest());

           // }
        }
        public ActionResult Cost()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Cost(int ? id, MaintenanceRequest maintenanceRequest)
        {
            var apprv = db.MaintenanceRequests.Where(x=>x.MaintenanceId==id).FirstOrDefault();
            apprv.IssueCost = maintenanceRequest.IssueCost;
            apprv.EmployeeEmail = User.Identity.GetUserName();

            apprv.DateFixed = DateTime.UtcNow.ToLongDateString();
            apprv.Status = "Done";
            db.Entry(apprv).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");


        }
        public ActionResult Edit2(int? id, MaintenanceRequest maintenanceRequest)
        {
            var apprv = db.MaintenanceRequests.Find(id);

            apprv.Status = "Dissapproved";
            apprv.Description = maintenanceRequest.Description;
            db.Entry(apprv).State = EntityState.Modified;
            db.SaveChanges();
            var userName = User.Identity.GetUserName();

           
            return View("Edit2");
        }
        

        //// POST: MaintenanceRequests/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit2(int? id,MaintenanceRequest maintenanceRequest)
        //{

        //    //if (ModelState.IsValid)
        //    {

        //        var userName = User.Identity.GetUserName();

        //        //if (maintenanceRequest.UserName == userName)
        //        //{
        //       var updat= Bs.GetTMaintenanceRequest(maintenanceRequest.MaintenanceId);
        //        updat.Description = maintenanceRequest.Description;
        //        Bs.UpdateMaintenanceRequest(maintenanceRequest);
        //        db.Entry(updat).State = EntityState.Modified;
        //        db.SaveChanges();
        //        //}
        //        //else
        //        //{
        //        //    TempData["message"] = "You cannot Delete someone else's report.";

        //        //    return View("Edit");

        //        //}
        //        return RedirectToAction("Index");


        //    }
        //    return View(maintenanceRequest);
        //}
        public ActionResult AddDescription(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceRequest maintenanceRequest = db.MaintenanceRequests.Find(id);
            if (maintenanceRequest == null)
            {
                return HttpNotFound();
            }
            return View(maintenanceRequest);
        }



        public ActionResult Hom()
        {
            return PartialView("_MaintenanceRequest", db.MaintenanceRequests.ToList());
        }

    }
}
