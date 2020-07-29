


using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SmartRes_Official2019Logic;
using  SmartRes_Official2019Data;

namespace SmartRes_Official2019.Controllers
{
    public class StudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        Logic lo = new Logic();
        BsLogic Bs = new BsLogic();
        // GET: Students
        public ActionResult Index(string sortOrder, string searchString)
        {
            var students = from s in db.Students
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString)
                                       || s.PhoneNumber.Contains(searchString) || s.StudentNumber.Contains(searchString) || s.StudId.Contains(searchString)); 
                return View(students);

            }
            var userId = User.Identity.GetUserName();
            if (User.IsInRole("SAN") || User.IsInRole("SystemAdmin"))
            {
                return View(Bs.LIstStudent());


            }
            else
            {
                //
                return View(Bs.LIstStudent().Where(x => x.Email == userId));


            }
        }

        //public ActionResult StudentDetails()
        //{
        //    return View(Bs.LIstStudent());
        //}
        public ActionResult StudentDetails(string id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Student student = Bs.GetTStudent(id);
            //if (student == null)
            //{
            //    return HttpNotFound();
            //}
            return View(student);
        }
        public ActionResult AllStudents()
        {
            var userId = User.Identity.GetUserName();
            if (User.IsInRole("SAN") || User.IsInRole("SystemAdmin"))
            {
                return View(Bs.LIstStudent());


            }
            else
            {
                
                return View(Bs.LIstStudent().Where(x => x.Email == userId));


            }
        }

        // GET: Students/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        public ActionResult IndexErro()
        {
            var userId = User.Identity.GetUserId();
            if (User.IsInRole("Student"))
            {
                return View(Bs.LIstStudent().Where(x => x.Email == userId));

            }
            else
            {
                return View(Bs.LIstStudent());

            }
        }

        // GET: Students/Create
        [Authorize]
        public ActionResult Create()
        {
         
            var userName = User.Identity.GetUserName();
            var useridfind = db.Students.Where(x=>x.Email== userName).FirstOrDefault();
            if (useridfind != null)
            {
                if (User.IsInRole("SystemAdmin"))
                {
                    return View();
                }
                else
                {
                    TempData["message"] = "You have already completed your profile.";
                    return View("Index", db.Students.ToList().Where(x=>x.Email==userName));
                }



            }
            else
            {
                return View();

            }

        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student, HttpPostedFileBase filelist)
        {
            byte[] data = null;
            data = new byte[filelist.ContentLength];
            filelist.InputStream.Read(data, 0, filelist.ContentLength);
            var studentDummy = db.Students_Dummies.Where(x=>x.StudentNumber==student.StudentNumber).FirstOrDefault();

            if (ModelState.IsValid)
            {

                if (lo.StudentNoExists(student.StudentNumber))
                {
                    TempData["message"] = "Student Number already exists.";
                    return View("Create");
                }
                if(studentDummy==null)
                {
                    TempData["message"] = "Invalid student number please check student number";
                    return View("Index", db.Students.ToList());
                }

                



               
                
                    Random r = new Random();
                    customerSt cs = new customerSt();
                    var userName = User.Identity.GetUserName();
                    var userId = User.Identity.GetUserId();
                    student.Email = userName;
                    student.StudId = userId+ r.Next(1,100);
                    //student.IdNumber = lo.getId(student.StudentNumber);
                    //student.Gender = lo.getStudentGender(student.StudentNumber);
                    student.RooNumber = lo.GetRoomNumber(userName);
                    student.ResName = lo.getResName(student.StudentNumber);
                    student.UserPhoto = data;

                    cs.email_id = userName;
                    cs.customer_id = userId+r.Next(1,100);
                    cs.NameIdentifier = student.Name;
                    
                    cs.last_name = student.Surname;
                    cs.gender = student.Gender;
                    db.custom.Add(cs);



                //Get Details From Dummy Table

                student.Surname = studentDummy.Name.Substring(0, 1) + "." + studentDummy.Surname;
                //student.Surname = studentDummy.Surname;
                student.Gender = studentDummy.Gender;
                student.IdNumber = studentDummy.IdNumber;
                student.PhoneNumber = studentDummy.PhoneNumber;
                student.IdForOut = userId;
                db.Students.Add(student);
                    //Bs.AddStudent(student);
                    db.SaveChanges();

                    return RedirectToAction("Create", "StudentCheckIns");
                //
              
            }

            return View(student);
        }

        //public byte[] ConvertToBytes(HttpPostedFileBase file)
        //{
        //    BinaryReader reader = new BinaryReader(file.InputStream);
        //    return reader.ReadBytes((int)file.ContentLength);
        //}

        // GET: Students/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudId,StudentNumber,Name,Surname,PhoneNumber,IdNumber,Gender,Email")] Student student)
        {
            if (ModelState.IsValid)
            {

                Bs.UpdateStudent(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = Bs.GetTStudent(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student student = Bs.GetTStudent(id);

            Bs.RemoveStudent(student);
            return RedirectToAction("Index");
        }

        public ActionResult StudentProfile()
        {
            
            return View(Bs.StudentProfiles());
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
