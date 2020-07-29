using System;
using System.Collections.Generic;

using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using  SmartRes_Official2019Data;

namespace SmartRes_Official2019.Controllers
{//
    public class expensesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: expenses
        public ActionResult Index()
        {
            //
            var expenses = db.expenses.Include(e => e.expense_type);
            return View(expenses.ToList());
        }

        // GET: expenses/Details/8
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            expenses expenses = db.expenses.Find(id);
            if (expenses == null)
            {
                return HttpNotFound();
            }
            return View(expenses);
        }

        // GET: expenses/Create
        public ActionResult Create()
        {
            ViewBag.exps_id = new SelectList(db.expense_Types, "exps_id", "exps_type");
            return View();
        }

        // POST: expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "exp_id,exps_id,emp_name,exps_date,exp_payee_name,exp_type,exp_amt,exp_remarks")] expenses expenses)
        {
            if (ModelState.IsValid)
            {
                db.expenses.Add(expenses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.exps_id = new SelectList(db.expense_Types, "exps_id", "exps_type", expenses.exps_id);
            return View(expenses);
        }

        // GET: expenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            expenses expenses = db.expenses.Find(id);
            if (expenses == null)
            {
                return HttpNotFound();
            }
            ViewBag.exps_id = new SelectList(db.expense_Types, "exps_id", "exps_type", expenses.exps_id);
            return View(expenses);
        }

        // POST: expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "exp_id,exps_id,emp_name,exps_date,exp_payee_name,exp_type,exp_amt,exp_remarks")] expenses expenses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expenses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.exps_id = new SelectList(db.expense_Types, "exps_id", "exps_type", expenses.exps_id);
            return View(expenses);
        }

        // GET: expenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            expenses expenses = db.expenses.Find(id);
            if (expenses == null)
            {
                return HttpNotFound();
            }
            return View(expenses);
        }

        // POST: expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            expenses expenses = db.expenses.Find(id);
            db.expenses.Remove(expenses);
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
