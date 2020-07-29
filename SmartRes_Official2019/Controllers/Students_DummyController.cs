using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartRes_Official2019Data;
using SmartRes_Official2019Logic;

namespace SmartRes_Official2019.Controllers
{
    public class Students_DummyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        Students_Dummy_Services students_Dummy_Services = new Students_Dummy_Services();
        // GET: Students_Dummy
        public ActionResult Index()
        {
            return View(students_Dummy_Services.LIstStudents_Dummy());
        }

        // GET: Students_Dummy/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Students_Dummy students_Dummy = students_Dummy_Services.GetStudents_Dummy(id);
            if (students_Dummy == null)
            {
                return HttpNotFound();
            }
            return View(students_Dummy);
        }

        // GET: Students_Dummy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students_Dummy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Students_Dummy students_Dummy)
        {
            if (ModelState.IsValid)
            {

                if (students_Dummy.StudentEmail.ToLower().Contains("@dut4life.ac.za"))
                {
                    students_Dummy_Services.AddStudents_Dummy(students_Dummy);
                    TempData["message"] = "Thank you very much your Details will not be miss used in ay way it's just for our presentation. Thank You ";
                    return RedirectToAction("Thankyou");
                }
                else
                {
                    TempData["message"] = "Only Dut Students can access this system";
                    return View();
                }
              
            }

            return View(students_Dummy);
        }

        public ActionResult Thankyou()
        {
            return View();
        }

        // GET: Students_Dummy/Edit/5
        [Authorize]

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students_Dummy students_Dummy = students_Dummy_Services.GetStudents_Dummy(id);

            if (students_Dummy == null)
            {
                return HttpNotFound();
            }
            return View(students_Dummy);
        }

        // POST: Students_Dummy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Students_Dummy students_Dummy)
        {
            if (ModelState.IsValid)
            {
                students_Dummy_Services.UpdateStudents_Dummy(students_Dummy);
                return RedirectToAction("Index");
            }
            return View(students_Dummy);
        }

        // GET: Students_Dummy/Delete/5
        [Authorize]

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students_Dummy students_Dummy = students_Dummy_Services.GetStudents_Dummy(id);

            if (students_Dummy == null)
            {
                return HttpNotFound();
            }
            return View(students_Dummy);
        }

        // POST: Students_Dummy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Students_Dummy students_Dummy = students_Dummy_Services.GetStudents_Dummy(id);
            students_Dummy_Services.RemoveStudents_Dummy(students_Dummy);
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
