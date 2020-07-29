using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using  SmartRes_Official2019Data;
using SmartRes_Official2019Logic;
using System.Data.Entity;

namespace SmartRes_Official2019.Controllers
{
    public class UniversityEmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        BsLogic Bs = new BsLogic();

        // GET: UniversityEmployees
        public ActionResult Index(string sortOrder, string searchString)
        {
            var students = from s in db.UniversityEmployees
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString)
                                       || s.Surname.Contains(searchString) || s.EmpTYpe.Contains(searchString));
                return View(students.ToList());

            }
            var userName = User.Identity.GetUserName();

            var universityEmployees = db.UniversityEmployees;
            if (User.IsInRole("SuperAdmin"))
            {
                return View(universityEmployees.ToList());

            }
            else
            {
                return View(universityEmployees.ToList().Where(x=>x.Email==userName));

            }
        }

        // GET: UniversityEmployees/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UniversityEmployee universityEmployee = db.UniversityEmployees.Find(id);
            if (universityEmployee == null)
            {
                return HttpNotFound();
            }
            return View(universityEmployee);
        }

        // GET: UniversityEmployees/Create
        public ActionResult Create()
        {
            ViewBag.UnivbersityId = new SelectList(db.Universities, "UnivbersityId", "UniversityName");
            return View();
        }

        // POST: UniversityEmployees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create( UniversityEmployee universityEmployee, HttpPostedFileBase filelist)
        {
            byte[] data = null;
            data = new byte[filelist.ContentLength];
            filelist.InputStream.Read(data, 0, filelist.ContentLength);
            if (ModelState.IsValid)
            {
               
                employee emp = new employee();
                Guid re = new Guid();
                Random R = new Random();
                var userName = User.Identity.GetUserName();
                var userId = User.Identity.GetUserId();
                universityEmployee.UserPhoto = data;
                emp.EmpID = re.ToString() + R.Next(1, 100).ToString();
                universityEmployee.EmpUId = re.ToString() + R.Next(1, 100).ToString();
                universityEmployee.EmpUId = userName;
                emp.Email = userName;
                emp.Name = universityEmployee.Name;
                emp.Surname = universityEmployee.Surname;
                emp.Gender = universityEmployee.Gender;
                emp.EmpTYpe = "University Employee";
                db.employees.Add(emp);
                Bs.AddUniversityEmployee(universityEmployee);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.UnivbersityId = new SelectList(db.Universities, "UnivbersityId", "UniversityName", universityEmployee.UnivbersityId);
            return View(universityEmployee);
        }

        // GET: UniversityEmployees/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UniversityEmployee universityEmployee = db.UniversityEmployees.Find(id);
            if (universityEmployee == null)
            {
                return HttpNotFound();
            }
            ViewBag.UnivbersityId = new SelectList(db.Universities, "UnivbersityId", "UniversityName", universityEmployee.UnivbersityId);
            return View(universityEmployee);
        }

        // POST: UniversityEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpUId,UnivbersityId,Name,Surname,Email,Gender,EmpTYpe")] UniversityEmployee universityEmployee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(universityEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UnivbersityId = new SelectList(db.Universities, "UnivbersityId", "UniversityName", universityEmployee.UnivbersityId);
            return View(universityEmployee);
        }

        // GET: UniversityEmployees/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UniversityEmployee universityEmployee = db.UniversityEmployees.Find(id);
            if (universityEmployee == null)
            {
                return HttpNotFound();
            }
            return View(universityEmployee);
        }

        // POST: UniversityEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UniversityEmployee universityEmployee = db.UniversityEmployees.Find(id);
            db.UniversityEmployees.Remove(universityEmployee);
            db.SaveChanges();
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
