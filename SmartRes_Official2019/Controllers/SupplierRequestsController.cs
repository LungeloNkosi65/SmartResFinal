using System.Linq;
using System.Net;
using System.Web.Mvc;
using  SmartRes_Official2019Data;
using System.Data.Entity;

namespace SmartRes_Official2019.Controllers
{
    public class SupplierRequestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SupplierRequests
        public ActionResult Index()
        {
            return View(db.SupplierRequests.ToList());
        }

        // GET: SupplierRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierRequest supplierRequest = db.SupplierRequests.Find(id);
            if (supplierRequest == null)
            {
                return HttpNotFound();
            }
            return View(supplierRequest);
        }

        // GET: SupplierRequests/Create
        public ActionResult Create()
        {
            ViewBag.SupplierID = new SelectList(db.maintenanceSuppliers, "SupplierID", "Supplier");

            return View();
        }

        // POST: SupplierRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierRequestID,ProductName,ProductQuantity,email,SupplierID")] SupplierRequest supplierRequest)
        {
            if (ModelState.IsValid)
            {
                var email = (from s in db.maintenanceSuppliers
                             where supplierRequest.SupplierID == s.SupplierID
                             select s.Supplier_Email
                   ).FirstOrDefault();
                supplierRequest.email = email;

                db.SupplierRequests.Add(supplierRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SupplierID = new SelectList(db.maintenanceSuppliers, "SupplierID", "Supplier", supplierRequest.SupplierID);

            return View(supplierRequest);
        }

        // GET: SupplierRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierRequest supplierRequest = db.SupplierRequests.Find(id);
            if (supplierRequest == null)
            {
                return HttpNotFound();
            }
            return View(supplierRequest);
        }

        // POST: SupplierRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupplierRequestID,ProductName,ProductQuantity,email")] SupplierRequest supplierRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplierRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplierRequest);
        }

        // GET: SupplierRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierRequest supplierRequest = db.SupplierRequests.Find(id);
            if (supplierRequest == null)
            {
                return HttpNotFound();
            }
            return View(supplierRequest);
        }

        // POST: SupplierRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SupplierRequest supplierRequest = db.SupplierRequests.Find(id);
            db.SupplierRequests.Remove(supplierRequest);
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
        public ActionResult SendEmail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierRequest supplierRequest = db.SupplierRequests.Where(x => x.SupplierRequestID == id).FirstOrDefault();
            var supplierEmail = supplierRequest.email;
            var email = (from s in db.maintenanceSuppliers
                         where supplierRequest.SupplierID == s.SupplierID
                         select s.Supplier_Email
                        ).FirstOrDefault();
            var ItermRequired = supplierRequest.ProductName;
            var quantiy = supplierRequest.ProductQuantity;

            EmailService em = new EmailService();
            em.SendRegMail(email, "*****Request for quote*****", "\n Good Day"+ "\n Please note that the following items are requested" +" "+ "Item" + ItermRequired, "\n Quantity" +" "+ quantiy+ "\n This email was created on the"+" "+System.DateTime.UtcNow+"\n SmartRes System");

            if (supplierRequest == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
