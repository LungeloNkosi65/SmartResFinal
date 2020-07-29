using System.Linq;
using System.Net;
using System.Web.Mvc;
using  SmartRes_Official2019Data;
using System.Data.Entity;

namespace SmartRes_Official2019.Controllers
{
    public class WeekDaysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        Logic BsLogic = new Logic();
        // GET: WeekDays
        public ActionResult Index()
        {
            return View(db.WeekDays.ToList());
        }

        // GET: WeekDays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeekDays weekDays = db.WeekDays.Find(id);
            if (weekDays == null)
            {
                return HttpNotFound();
            }
            return View(weekDays);
        }

        // GET: WeekDays/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WeekDays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WkdId,CurrDayofWeek")] WeekDays weekDays)
        {
            if (ModelState.IsValid)
            {
                if (BsLogic.CheckWeekDays(weekDays.CurrDayofWeek))
                {
                    ModelState.AddModelError("", "A week Only has 7 Days");

                }
                else
                {
                    db.WeekDays.Add(weekDays);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(weekDays);
        }

        // GET: WeekDays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeekDays weekDays = db.WeekDays.Find(id);
            if (weekDays == null)
            {
                return HttpNotFound();
            }
            return View(weekDays);
        }

        // POST: WeekDays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WkdId,CurrDayofWeek")] WeekDays weekDays)
        {
            if (ModelState.IsValid)
            {
                db.Entry(weekDays).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(weekDays);
        }

        // GET: WeekDays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeekDays weekDays = db.WeekDays.Find(id);
            if (weekDays == null)
            {
                return HttpNotFound();
            }
            return View(weekDays);
        }

        // POST: WeekDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WeekDays weekDays = db.WeekDays.Find(id);
            db.WeekDays.Remove(weekDays);
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
