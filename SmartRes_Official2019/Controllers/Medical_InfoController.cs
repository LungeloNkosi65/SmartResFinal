using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SmartRes_Official2019Logic;

using Microsoft.AspNet.Identity;
using  SmartRes_Official2019Data;
using System.Data.Entity;

namespace SmartRes_Official2019.Controllers
{
    public class Medical_InfoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        BsLogic Bs = new BsLogic();

        // GET: Medical_Info
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserName();
            if (User.IsInRole("SystemAdmin"))
            {
                return View(Bs.ListMedicInfo());

            }
            else
            {
                return View(Bs.ListMedicInfo().Where(x => x.StudentEmail == userId));

            }
        }

        // GET: Medical_Info/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medical_Info medical_Info = db.Medical_Info.Find(id);
            if (medical_Info == null)
            {
                return HttpNotFound();
            }
            return View(medical_Info);
        }

        // GET: Medical_Info/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Medical_Info/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MedId,StudentEmail,Have_Med,Med_Number,Med_Name,Owner_Name,Illness,Illness_Description,Chronic_Medication,Chronic_Description,Parent_Details,Parent_Name")] Medical_Info medical_Info)
        {
            if (ModelState.IsValid)
            {
                Random r = new Random();
                var userId = User.Identity.GetUserId() + r.Next(1, 100);
                medical_Info.MedId = userId;
                if (medical_Info.Med_Number == "No")
                {
                    medical_Info.Med_Number = "N/A";
                    medical_Info.Med_Name = "N/A";
                    medical_Info.Owner_Name = "N/A";

                }
                if (medical_Info.Illness == "No")
                {
                    medical_Info.Illness_Description = "N/A";
                }
                if (medical_Info.Chronic_Medication == "No")
                {
                    medical_Info.Chronic_Description = "N/A";
                }
                var username = User.Identity.GetUserName();
                medical_Info.StudentEmail = username;
                Bs.AddMedical_Info(medical_Info);
                return RedirectToAction("Index", "Home");
            }

            return View(medical_Info);
        }

        // GET: Medical_Info/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medical_Info medical_Info = db.Medical_Info.Find(id);
            if (medical_Info == null)
            {
                return HttpNotFound();
            }
            return View(medical_Info);
        }

        // POST: Medical_Info/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MedId,StudentEmail,Have_Med,Med_Number,Med_Name,Owner_Name,Illness,Illness_Description,Chronic_Medication,Chronic_Description,Parent_Details,Parent_Name")] Medical_Info medical_Info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medical_Info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medical_Info);
        }

        // GET: Medical_Info/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medical_Info medical_Info = db.Medical_Info.Find(id);
            if (medical_Info == null)
            {
                return HttpNotFound();
            }
            return View(medical_Info);
        }

        // POST: Medical_Info/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Medical_Info medical_Info = db.Medical_Info.Find(id);
            db.Medical_Info.Remove(medical_Info);
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
