using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Data.Entity;
using  SmartRes_Official2019Data;

namespace SmartRes_Official2019.Controllers
{
    public class expense_typeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: expense_type
        public ActionResult Index()
        {
            return View(db.expense_Types.ToList());
        }

        // GET: expense_type/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            expense_type expense_type = db.expense_Types.Find(id);
            if (expense_type == null)
            {
                return HttpNotFound();
            }
            return View(expense_type);
        }

        // GET: expense_type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: expense_type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "exps_id,exps_type,exps_code")] expense_type expense_type)
        {
            if (ModelState.IsValid)
            {
                db.expense_Types.Add(expense_type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expense_type);
        }

        // GET: expense_type/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            expense_type expense_type = db.expense_Types.Find(id);
            if (expense_type == null)
            {
                return HttpNotFound();
            }
            return View(expense_type);
        }

        // POST: expense_type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "exps_id,exps_type,exps_code")] expense_type expense_type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expense_type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expense_type);
        }

        // GET: expense_type/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            expense_type expense_type = db.expense_Types.Find(id);
            if (expense_type == null)
            {
                return HttpNotFound();
            }
            return View(expense_type);
        }

        // POST: expense_type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            expense_type expense_type = db.expense_Types.Find(id);
            db.expense_Types.Remove(expense_type);
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
