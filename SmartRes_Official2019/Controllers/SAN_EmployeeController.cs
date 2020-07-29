using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SmartRes_Official2019Logic;
using  SmartRes_Official2019Data;

namespace SmartRes_Official2019.Controllers
{
    public class SAN_EmployeeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        BsLogic Bs = new BsLogic();

        // GET: SAN_Employee
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var username = User.Identity.GetUserName();
            if (User.IsInRole("Employee"))
            {
                return View(Bs.LIstSAN_Employee().Where(x=>x.Eamil==username));

            }
            else
            {
                return View(Bs.LIstSAN_Employee());

            }
        }

        [Authorize]
        public ActionResult AllEmployees()
        {
            return View(db.employees.ToList());
        }
        // GET: SAN_Employee/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SAN_Employee sAN_Employee = db.SAN_Employee.Find(id);
            if (sAN_Employee == null)
            {
                return HttpNotFound();
            }
            return View(sAN_Employee);
        }

        // GET: SAN_Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SAN_Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmpID,Name,Surname,PhoneNumber,Eamil,Gender")] SAN_Employee sAN_Employee)
        {
            if (ModelState.IsValid)
            {
                employee emp = new employee();
                Guid re = new Guid();
                Random R = new Random();
                var userName = User.Identity.GetUserName();
                var userId = User.Identity.GetUserId();
                sAN_Employee.EmpID = userId;
                sAN_Employee.Eamil = userName;
                emp.EmpID = re.ToString() + R.Next(1, 100).ToString();
                emp.Email = userName;
                emp.Name = sAN_Employee.Name;
                emp.Surname = sAN_Employee.Surname;
                emp.Gender = sAN_Employee.Gender;
                emp.EmpTYpe = "SAN Employee";
                db.employees.Add(emp);
                Bs.AddSAN_Employee(sAN_Employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sAN_Employee);
        }

        // GET: SAN_Employee/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SAN_Employee sAN_Employee = Bs.GetTSAN_Employee(id);

            if (sAN_Employee == null)
            {
                return HttpNotFound();
            }
            return View(sAN_Employee);
        }

        // POST: SAN_Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpID,Name,Surname,PhoneNumber,Eamil,Gender")] SAN_Employee sAN_Employee)
        {
            if (ModelState.IsValid)
            {
                Bs.UpdateSAN_Employee(sAN_Employee);
                return RedirectToAction("Index");
            }
            return View(sAN_Employee);
        }

        // GET: SAN_Employee/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SAN_Employee sAN_Employee = Bs.GetTSAN_Employee(id);
            if (sAN_Employee == null)
            {
                return HttpNotFound();
            }
            return View(sAN_Employee);
        }

        // POST: SAN_Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SAN_Employee sAN_Employee = Bs.GetTSAN_Employee(id);

            Bs.RemoveSAN_Employee(sAN_Employee);

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
