using System;
using System.Collections.Generic;
//using @model SmartRes_Official2019Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using  SmartRes_Official2019Data;
using System.Data.Entity;

namespace SmartRes_Official2019.Controllers
{
    public class customer_orderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: customer_order
        public ActionResult Index(string sortOrder, string searchString)
        {

            int coubt = db.PriceLists.Count();
            var students = from s in db.customer_Orders
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.CustomerName.Contains(searchString)
                                       || s.customer_Id.Contains(searchString) || s.services.service_name.Contains(searchString) || s.order_date.Contains(searchString)); 
                return View(students.ToList());

            }

            var customer_Orders = db.customer_Orders.Include(c => c.Cloths).Include(c => c.services);
            //Diferiantiate acording to roles
            if (User.IsInRole("SAN"))
            {
                return View(customer_Orders.ToList().Where(x => x.order_status == null));
            }
            //He can only see his order 
            customer_order co = new customer_order();
            //return View(customer_Orders.ToList().Where(x=>x.order_status==null && co.customer_Id==User.Identity.GetUserId()));
            return View(customer_Orders.ToList().Where(x => x.customer_Id == User.Identity.GetUserId()));
        }

        // GET: customer_order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer_order customer_order = db.customer_Orders.Find(id);
            if (customer_order == null)
            {
                return HttpNotFound();
            }
            return View(customer_order);
        }

        // GET: customer_order/Create
        public ActionResult Create()
        {

            ViewBag.ClothsID = new SelectList(db.cloths, "ID", "cloth_Type");
            ViewBag.ServiceID = new SelectList(db.Services, "ID", "service_name");
            return View();
        }

     

        public bool CheckID(string id )
        {
            bool results = false;
            var c = db.OrderLists.ToList();
            foreach (var iterm in c)
            {
                if (id== iterm.customerID)
                {

                    results = true;
                }
            }
            return results;
        }

        [HttpPost]
        public JsonResult CheckTime()
        {
                    return new JsonResult{Data=new { staus="The laundry is only operational between 09:00 amd 17:00"} };


        }

        // POST: customer_order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( customer_order customer_order)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var UserID = User.Identity.GetUserId();
            var username = User.Identity.GetUserName();


            DateTime checkDate = DateTime.Now.AddHours(3);

        
            if (ModelState.IsValid)
            {

                if (customer_order.total_qty<0)
                {
                    ModelState.AddModelError("", "they can not put a value as yet");
                }
                else
                {

                    customer_order.invoice_no = customer_order.InvoiceNumber();

                    customer_order.customer_Id = UserID;

                    customer_order.order_status = null;
                    customer_order.total_balance = customer_order.itermprice();
                    customer_order.total_paid = customer_order.TotalPrice();
                    customer_order.order_date = System.DateTime.Now.ToShortDateString();
                    customer_order.order_month = System.DateTime.Now.Month.ToString();



                    if (CheckID(username) == true)
                    {
                        var Odl = db.OrderLists.Where(x => x.customerID == username).FirstOrDefault();
                        Odl.qty = Odl.qty + customer_order.total_qty;
                        Odl.amt = Odl.amt + customer_order.total_paid;
                        Odl.status = "In Progress";
                        Odl.OrderDate = DateTime.Now.Date;

                        db.Entry(Odl).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                    else
                    {
                     
                        OrderList od = new OrderList();
                        od.InvoiceNo = od.GenInvoice();
                        od.OrderDate = DateTime.Now.Date;
                        od.customerID = User.Identity.GetUserName();
                        od.qty = customer_order.total_qty;
                        od.amt = customer_order.total_paid;
                        od.status = "In progress";
                        db.OrderLists.Add(od);
                        db.SaveChanges();
                    }
                    db.customer_Orders.Add(customer_order);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.ClothsID = new SelectList(db.cloths, "ID", "cloth_Type", customer_order.ClothsID);
            ViewBag.ServiceID = new SelectList(db.Services, "ID", "service_name", customer_order.ServiceID);
            return View(customer_order);
        }

        // GET: customer_order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer_order customer_order = db.customer_Orders.Find(id);
            if (customer_order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClothsID = new SelectList(db.cloths, "ID", "cloth_Type", customer_order.ClothsID);
            ViewBag.ServiceID = new SelectList(db.Services, "ID", "service_name", customer_order.ServiceID);
            return View(customer_order);
        }

        // POST: customer_order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,invoice_no,order_date,order_month,customer_Id,total_qty,discount,service_tax,total_paid,total_balance,delivery_date,remarks,order_status,CustomerName,ClothsID,ServiceID")] customer_order customer_order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer_order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClothsID = new SelectList(db.cloths, "ID", "cloth_Type", customer_order.ClothsID);
            ViewBag.ServiceID = new SelectList(db.Services, "ID", "service_name", customer_order.ServiceID);
            return View(customer_order);
        }

        // GET: customer_order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer_order customer_order = db.customer_Orders.Find(id);
            if (customer_order == null)
            {
                return HttpNotFound();
            }
            return View(customer_order);
        }

        // POST: customer_order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            customer_order customer_order = db.customer_Orders.Find(id);
            db.customer_Orders.Remove(customer_order);
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
        public string gettime ()
        {
        string hello = "The laundry is only operational between 09:00 amd 17:00 ";
        return hello;
    }
}
}
