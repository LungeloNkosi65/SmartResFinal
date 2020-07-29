using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SmartRes_Official2019Logic;
using  SmartRes_Official2019Data;

namespace SmartRes_Official2019.Controllers
{
    public class PDFsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        BsLogic Bs = new BsLogic();
        Logic lo = new Logic();

        // GET: PDFs
        public ActionResult Index()
        {
            var UserName = User.Identity.GetUserName();
            if (User.IsInRole("SystemAdmin"))
            {
                return View(Bs.LIstPdf());

            }
            else
            {
                return View(Bs.LIstPdf().Where(x=>x.Email==UserName));

            }
        }
        public ActionResult ManageFile()
        {
            return View(Bs.LIstPdf());
        }

        // GET: PDFs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PDF pDF = Bs.GetTPDF(id);
            if (pDF == null)
            {
                return HttpNotFound();
            }
            return View(pDF);
        }

        // GET: PDFs/Create
        public ActionResult Create()
        {
            return View();
        }
        public byte[] ConvertToBytes(HttpPostedFileBase files)
        {

            BinaryReader reader = new BinaryReader(files.InputStream);
            return reader.ReadBytes(files.ContentLength);
        }
        // POST: PDFs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "num,location,file,Email")] PDF pDF, HttpPostedFileBase files)
        {
            if (files != null && files.ContentLength > 0)
            {
                var username = User.Identity.GetUserName();
                pDF.location = files.FileName;
                string[] bits = pDF.location.Split('\\')  ;
                //pDF.location =lo.GetResForMaintenance(username).Substring(0,3).ToUpper()+ lo.GetRoomNumber(username)+ bits[bits.Length - 1];
                pDF.Email = username;

                pDF.file = ConvertToBytes(files);
            }
            if (ModelState.IsValid)
            {
                Bs.AddPDF(pDF);
                return RedirectToAction("Create", "Medical_Info");
            }

            return View(pDF);
        }
        public ActionResult Download(int? id)
        {
            //var r = db.PDFs.Find(id);

            //return File(r.file, "application/pdf", r.location);

            //var r = db.PDFs.Find(id);
            //PDF pDF = db.PDFs.Find(id);
            //string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"300px\">";
            //embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            //embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            //embed += "</object>";
            //TempData["Embed"] = string.Format(embed, VirtualPathUtility.ToAbsolute(r.location).ToString());

            MemoryStream ms = null;

            var item = db.PDFs.FirstOrDefault(x => x.num == id);
            if (item != null)
            {
                ms = new MemoryStream(item.file);
            }
            return new FileStreamResult(ms, item.location);


            //return RedirectToAction("Download");
        }

        [HttpPost]
        public ActionResult ViewPDF(int? id)
        {
            var r = db.PDFs.Find(id);
            //PDF pDF = db.PDFs.Find(id);
            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"300px\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";
            TempData["Embed"] = string.Format(embed, VirtualPathUtility.ToAbsolute(  r.location));

            return RedirectToAction("Index");
        }


        // GET: PDFs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PDF pDF = db.PDFs.Find(id);
            if (pDF == null)
            {
                return HttpNotFound();
            }
            return View(pDF);
        }

        // POST: PDFs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "num,location,file,Email")] PDF pDF)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.GetUserName();
                pDF.Email = userName;
                Bs.UpdatePdf(pDF);
                return RedirectToAction("Index");
            }
            return View(pDF);
        }

        // GET: PDFs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PDF pDF = Bs.GetTPDF(id);

            if (pDF == null)
            {
                return HttpNotFound();
            }
            return View(pDF);
        }

        // POST: PDFs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PDF pDF = Bs.GetTPDF(id);
            Bs.RemovePDF(pDF);
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
