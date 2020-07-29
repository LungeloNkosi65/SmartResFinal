using System;
using System.Collections.Generic;

using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using  SmartRes_Official2019Data;
using System.Data.Entity;

namespace SmartRes_Official2019.Controllers
{
    public class OrderListsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderLists
        public ActionResult Index(string sortOrder, string searchString)
        {
            var students = from s in db.OrderLists
                           select s;

            var username = User.Identity.GetUserName();
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.InvoiceNo.Contains(searchString)
                                       || s.status.Contains(searchString)); 
                return View(students.ToList());

            }
            if (User.IsInRole("Student"))
            {
                return View(db.OrderLists.ToList().Where(x => x.customerID == username));

            }
            else
            {
                return View(db.OrderLists.ToList());

            }
        }
        public ActionResult AllIndex(string sortOrder, string searchString)
        {
            return View(db.customer_Orders.ToList());
        }


        public ActionResult updateStatus(string id)
        {

            var apprv = db.OrderLists.Find(id);

            apprv.status = "Approved";
            db.Entry(apprv).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "OrderLists");
        }
        public ActionResult Done(string id)
        {
            EmailService em = new EmailService();
            var apprv = db.OrderLists.Find(id);
            
            apprv.status = "Done";
            db.Entry(apprv).State = EntityState.Modified;
            db.SaveChanges();
            em.sendLuandryEmail(apprv.customerID, "Luandry is done please collect at luandry room", "Laundy Collection");

            return RedirectToAction("Index", "OrderLists");
        }

        public ActionResult Recieved(string id)
        {
            EmailService em = new EmailService();
            var apprv = db.OrderLists.Find(id);

            apprv.status = "Recieved";
            db.Entry(apprv).State = EntityState.Modified;
            db.SaveChanges();
            em.sendLuandryEmail(apprv.customerID, "Luandry is done please collect at luandry room", "Laundy Collection");

            return RedirectToAction("Index", "OrderLists");
        }
        // GET: OrderLists/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderList orderList = db.OrderLists.Find(id);
            if (orderList == null)
            {
                return HttpNotFound();
            }
            return View(orderList);
        }

        // GET: OrderLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceNo,OrderDate,qty,amt,status,customerID")] OrderList orderList)
        {
            if (ModelState.IsValid)
            {
                orderList.customerID = User.Identity.GetUserId();
                db.OrderLists.Add(orderList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderList);
        }

        // GET: OrderLists/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderList orderList = db.OrderLists.Find(id);
            if (orderList == null)
            {
                return HttpNotFound();
            }
            return View(orderList);
        }

        // POST: OrderLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceNo,OrderDate,qty,amt,status,customerID")] OrderList orderList)
        {
            if (ModelState.IsValid)
            {
                var UserID = User.Identity.GetUserId();

                var listorders = db.customer_Orders.ToList().Where(x => x.customer_Id == UserID);

                //var customerEmail = db.Students.ToList().Where(x => x. == orderList.customerID);
                //                    customerEmail.Select(x => x.email_id);

                foreach (var item in listorders)
                {
                    item.order_status = "Done";
                    db.Entry(item).State = EntityState.Modified;
                }
                orderList.amt = 0;
                orderList.qty = 0;
                orderList.amt = 0;


                db.Entry(orderList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderList);
        }

        // GET: OrderLists/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderList orderList = db.OrderLists.Find(id);
            if (orderList == null)
            {
                return HttpNotFound();
            }
            return View(orderList);
        }

        // POST: OrderLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            OrderList orderList = db.OrderLists.Find(id);
            db.OrderLists.Remove(orderList);
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
