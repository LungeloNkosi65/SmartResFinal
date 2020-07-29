using System.Linq;
using System.Net;
using System.Web.Mvc;
using  SmartRes_Official2019Data;
using System.Data.Entity;

namespace SmartRes_Official2019.Controllers
{
    public class PriceListsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PriceLists
        public ActionResult Index()
        {
            var priceLists = db.PriceLists;
            return View(priceLists.ToList());
        }

        // GET: PriceLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PriceList priceList = db.PriceLists.Find(id);
            if (priceList == null)
            {
                return HttpNotFound();
            }
            return View(priceList);
        }

        // GET: PriceLists/Create
        public ActionResult Create()
        {
            ViewBag.ClothsID = new SelectList(db.cloths, "ID", "cloth_Type");
            ViewBag.ServiceID = new SelectList(db.Services, "ID", "service_name");
            return View();
        }

        // POST: PriceLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ClothsID,ServiceID,price")] PriceList priceList)
        {
            if (ModelState.IsValid)
            {
                db.PriceLists.Add(priceList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClothsID = new SelectList(db.cloths, "ID", "cloth_Type", priceList.ClothsID);
            ViewBag.ServiceID = new SelectList(db.Services, "ID", "service_name", priceList.ServiceID);
            return View(priceList);
        }

        // GET: PriceLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PriceList priceList = db.PriceLists.Find(id);
            if (priceList == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClothsID = new SelectList(db.cloths, "ID", "cloth_Type", priceList.ClothsID);
            ViewBag.ServiceID = new SelectList(db.Services, "ID", "service_name", priceList.ServiceID);
            return View(priceList);
        }

        // POST: PriceLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClothsID,ServiceID,price")] PriceList priceList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(priceList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClothsID = new SelectList(db.cloths, "ID", "cloth_Type", priceList.ClothsID);
            ViewBag.ServiceID = new SelectList(db.Services, "ID", "service_name", priceList.ServiceID);
            return View(priceList);
        }

        // GET: PriceLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PriceList priceList = db.PriceLists.Find(id);
            if (priceList == null)
            {
                return HttpNotFound();
            }
            return View(priceList);
        }

        // POST: PriceLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PriceList priceList = db.PriceLists.Find(id);
            db.PriceLists.Remove(priceList);
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
