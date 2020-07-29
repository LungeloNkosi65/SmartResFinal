using System;
using System.Collections.Generic;

using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using  SmartRes_Official2019Data;
using System.Data.Entity;
using SmartRes_Official2019Logic;

namespace SmartRes_Official2019.Controllers
{
    public class MaintenanceSuppliersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        BsLogic Bs = new BsLogic();
        Logic lo = new Logic();

        // GET: MaintenanceSuppliers
        public ActionResult Index(string sortOrder, string searchString)
        {
            var students = from s in db.maintenanceSuppliers
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Supplier.Contains(searchString)
                                       || s.Supplier_Product.Contains(searchString)); 

                return View(students.ToList());

            }
            return View(Bs.ListMaintenanceSupp());
        }

        // GET: MaintenanceSuppliers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceSuppliers maintenanceSuppliers = Bs.GetTMaintenanceSupp(id);
            if (maintenanceSuppliers == null)
            {
                return HttpNotFound();
            }
            return View(maintenanceSuppliers);
        }

        // GET: MaintenanceSuppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MaintenanceSuppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierID,Supplier,Supplier_Product,Product_Cost,Product_Quantity,Supplier_Email")] MaintenanceSuppliers maintenanceSuppliers)
        {
            if (ModelState.IsValid)
            {
                Bs.AddMaintenanceSupp(maintenanceSuppliers);
                
                return RedirectToAction("Index");
            }

            return View(maintenanceSuppliers);
        }

        // GET: MaintenanceSuppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceSuppliers maintenanceSuppliers =Bs.GetTMaintenanceSupp(id);
            if (maintenanceSuppliers == null)
            {
                return HttpNotFound();
            }
            return View(maintenanceSuppliers);
        }

        // POST: MaintenanceSuppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupplierID,Supplier,Supplier_Product,Product_Cost,Product_Quantity,Supplier_Email")] MaintenanceSuppliers maintenanceSuppliers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maintenanceSuppliers).State = EntityState.Modified;
                Bs.AddMaintenanceSupp(maintenanceSuppliers);
                return RedirectToAction("Index");
            }
            return View(maintenanceSuppliers);
        }

        // GET: MaintenanceSuppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceSuppliers maintenanceSuppliers = Bs.GetTMaintenanceSupp(id);
            if (maintenanceSuppliers == null)
            {
                return HttpNotFound();
            }
            return View(maintenanceSuppliers);
        }

        // POST: MaintenanceSuppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MaintenanceSuppliers maintenanceSuppliers = Bs.GetTMaintenanceSupp(id);
            Bs.RemoveMaintenanceSupp(maintenanceSuppliers);
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
