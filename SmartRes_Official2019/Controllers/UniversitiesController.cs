using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using SmartRes_Official2019Data;
using SmartRes_Official2019Logic;
using System.Linq;
using System;

namespace SmartRes_Official2019.Controllers
{
    public class UniversitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        BsLogic Bs = new BsLogic();

        // GET: Universities
        public ActionResult Index(string sortOrder, string searchString)
        {
            var students = from s in db.Universities
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.UniversityName.Contains(searchString));
                                       
                return View(students.ToList());
            }
                return View(Bs.ListUniversity());
        }

        // GET: Universities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            University university = db.Universities.Find(id);
            if (university == null)
            {
                return HttpNotFound();
            }
            return View(university);
        }

        // GET: Universities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Universities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UnivbersityId,UniversityName,Email,PhoneNumber,UniversityLogo")] University university, HttpPostedFileBase filelist)
        {
            if (ModelState.IsValid)
            {


                if (filelist != null && filelist.ContentLength > 0)
                {
                    university.UniversityLogo = ConvertToBytes(filelist);
                }

                Bs.AddUniversity(university);
                return RedirectToAction("Index");
            }

            return View(university);
        }

        public byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            BinaryReader reader = new BinaryReader(file.InputStream);
            return reader.ReadBytes((int)file.ContentLength);
        }

        // GET: Universities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            University university = db.Universities.Find(id);
            if (university == null)
            {
                return HttpNotFound();
            }
            return View(university);
        }

        // POST: Universities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UnivbersityId,UniversityName,Email,PhoneNumber")] University university)
        {
            if (ModelState.IsValid)
            {
                Bs.UpdateUniversity(university);
                return RedirectToAction("Index");
            }
            return View(university);
        }

        // GET: Universities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            University university = Bs.GetTUniversity(id);
            if (university == null)
            {
                return HttpNotFound();
            }
            return View(university);
        }

        // POST: Universities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            University university = Bs.GetTUniversity(id);

            Bs.RemoveUniversity(university);
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
