using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using  SmartRes_Official2019Data;
using System.Data.Entity;

namespace SmartRes_Official2019.Controllers
{
    public class ClothsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cloths
        public ActionResult Index(string sortOrder, string searchString)
        {

            var students = from s in db.cloths
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.cloth_Type.Contains(searchString)
                                       || s.clothCode.Contains(searchString));
                return View(students);

            }
            return View(db.cloths.ToList());
        }

        // GET: Cloths/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cloths cloths = db.cloths.Find(id);
            if (cloths == null)
            {
                return HttpNotFound();
            }
            return View(cloths);
        }

        // GET: Cloths/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cloths/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,cloth_Type,clothCode,cloth_image")] Cloths cloths, HttpPostedFileBase filelist)
        {
            if (ModelState.IsValid)
            {


                if (filelist != null && filelist.ContentLength > 0)
                {
                    cloths.cloth_image = ConvertToBytes(filelist);
                }


                db.cloths.Add(cloths);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cloths);
        }

        public byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            BinaryReader reader = new BinaryReader(file.InputStream);
            return reader.ReadBytes((int)file.ContentLength);
        }

        // GET: Cloths/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cloths cloths = db.cloths.Find(id);
            if (cloths == null)
            {
                return HttpNotFound();
            }
            return View(cloths);
        }

        // POST: Cloths/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,cloth_Type,clothCode,cloth_image")] Cloths cloths)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cloths).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cloths);
        }

        // GET: Cloths/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cloths cloths = db.cloths.Find(id);
            if (cloths == null)
            {
                return HttpNotFound();
            }
            return View(cloths);
        }

        // POST: Cloths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cloths cloths = db.cloths.Find(id);
            db.cloths.Remove(cloths);
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
