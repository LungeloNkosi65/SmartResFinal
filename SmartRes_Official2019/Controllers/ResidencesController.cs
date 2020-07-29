using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartRes_Official2019Logic;
using System.IO;
using SmartRes_Official2019Data;
using System;
using System.Collections.Generic;
using System.Linq;
namespace SmartRes_Official2019.Controllers
{
    public class ResidencesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        BsLogic Bs = new BsLogic();
        // GET: Residences
        public ActionResult Index(string sortOrder, string searchString)
        {
            var students = from s in db.Residences
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.ResName.Contains(searchString)
                                       || s.ResAddress.Contains(searchString));
                return View(students.ToList());

            }
            var residences = db.Residences;
            return View(Bs.LIstResidences());
        }

        // GET: Residences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Residence residence = db.Residences.Find(id);
            if (residence == null)
            {
                return HttpNotFound();
            }
            return View(residence);
        }

        // GET: Residences/Create
        public ActionResult Create()
        {
            ViewBag.UnivbersityId = new SelectList(db.Universities, "UnivbersityId", "UniversityName");
            return View();
        }

        // POST: Residences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResId,UnivbersityId,ResName,ResAddress,Capacity,Gender_Allocation,ResidentPhoto")] Residence residence, HttpPostedFileBase filelist)
        {
            if (ModelState.IsValid)
            {
                if (residence.Capacity >= 0 && residence.Capacity<=2500)
                {
                    ResAvailability re = new ResAvailability();
                    re.ResId = residence.ResId;
                    re.Quantity = residence.Capacity;
                    re.NumAvailable = residence.Capacity;
                    re.BookedSpaces = 0;
                    re.CheckedIN = 0;
                    re.Gender = residence.Gender_Allocation;



                    if (filelist != null && filelist.ContentLength > 0)
                    {
                        residence.ResidentPhoto = ConvertToBytes(filelist);
                    }



                    //db.ResAvailabilities.Add(re);
                    Bs.AddResAvailability(re);
                    Bs.AddResidence(residence);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Residence Capacity");
                }
            }

            ViewBag.UnivbersityId = new SelectList(db.Universities, "UnivbersityId", "UniversityName", residence.UnivbersityId);
            return View(residence);
        }


        public byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            BinaryReader reader = new BinaryReader(file.InputStream);
            return reader.ReadBytes((int)file.ContentLength);
        }

        // GET: Residences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Residence residence = db.Residences.Find(id);
            if (residence == null)
            {
                return HttpNotFound();
            }
            ViewBag.UnivbersityId = new SelectList(db.Universities, "UnivbersityId", "UniversityName", residence.UnivbersityId);
            return View(residence);
        }

        // POST: Residences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResId,UnivbersityId,ResName,ResAddress,Capacity,Gender_Allocation")] Residence residence)
        {
            if (ModelState.IsValid)
            {
                Bs.UpdateResidence(residence);
                return RedirectToAction("Index");
            }
            ViewBag.UnivbersityId = new SelectList(db.Universities, "UnivbersityId", "UniversityName", residence.UnivbersityId);
            return View(residence);
        }

        // GET: Residences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Residence residence = Bs.GetTResidence(id);

            if (residence == null)
            {
                return HttpNotFound();
            }
            return View(residence);
        }

        // POST: Residences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Residence residence = Bs.GetTResidence(id);
            Bs.RemoveResidence(residence);
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
