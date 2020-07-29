using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SendGrid.SmtpApi;
using SendGrid;
using System.Net;
using System.Net.Mail;

using Microsoft.AspNet.Identity;
using SendGrid.Helpers.Mail;
using PagedList;
using System.IO;
using SmartRes_Official2019Data;
using System.Data.Entity;

using SmartRes_Official2019Logic;

namespace SmartRes_Official2019.Controllers
{
    public class MessageController : ApplicationBaseController
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();
        private ApplicationDbContext db = new ApplicationDbContext();


        Logic lo = new Logic();


    


        public ActionResult NoticeIndex(int? page)
        {
            employee em = new employee();

            var username = User.Identity.GetUserName();
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            if (User.IsInRole("Employee") || User.IsInRole("SAN"))
            {
                return View(dbContext.Messages.ToList().ToPagedList(pageNumber, pageSize));

            }
            
            else
            {
                return View(dbContext.Messages.ToList().Where(x => x.Residence == lo.getResNameWith(username) || x.StudentEmail==username).ToPagedList(pageNumber, pageSize));


            }
        }













        [HttpPost]
        public ActionResult PostMessage(MessageReplyViewModel vm, HttpPostedFileBase filelist)
        {
            DateTime now = DateTime.UtcNow;

            var username = User.Identity.GetUserName();
            string fullName = "";
            int msgid = 0;
            Message messagetoPost = new Message();


            if (!string.IsNullOrEmpty(username))
            {
                //var user = dbContext.Users.SingleOrDefault(u => u.UserName == username);
                //var user1 = dbContext.employees.SingleOrDefault(u => u.Email == username);
                var user = dbContext.employees.ToList().SingleOrDefault(u => u.Email == username);

                fullName = string.Concat(new string[] { user.Name, " ", user.Surname });
            }
            Notification not = new Notification();

            if (vm.Message.Subject != string.Empty && vm.Message.MessageToPost != string.Empty)
            {
                if (User.IsInRole("Employee"))
                {
                    messagetoPost.Residence = lo.getResForNotification(username);
                }
                messagetoPost.DatePosted = now.Date;
                messagetoPost.Subject = vm.Message.Subject;
                messagetoPost.MessageToPost = vm.Message.MessageToPost;
                messagetoPost.From = fullName;
                messagetoPost.StudentEmail = vm.StudentEmail;
                messagetoPost.Residence = vm.Residence;
                messagetoPost.To = vm.To;
                dbContext.Messages.Add(messagetoPost);
                dbContext.SaveChanges();
                msgid = messagetoPost.Id;
                //Populate Notification
              

            }

            return RedirectToAction("MessageTo");
        }
      
        public ActionResult Create(HttpPostedFileBase filelist)
        {
            var username = User.Identity.GetUserName();

            MessageReplyViewModel vm = new MessageReplyViewModel();

            ViewBag.Residence = new SelectList(db.Residences, "ResName", "ResName",vm.Residence);
            ViewBag.StudentEmail = new SelectList(db.Students, "Email", "Email", vm.StudentEmail);

            return View(vm);
        }




        public ActionResult MessageTo()
        {
            MessageReplyViewModel vm = new MessageReplyViewModel();
            int ms = db.Messages.Select(x => x.Id).Max();
            var check = db.Messages.Find(ms);
            if (check.To == "Student")
            {
                //ViewBag.Residence = null;

                ViewBag.StudentEmail = new SelectList(db.Students, "Email", "Email", vm.StudentEmail);
            }
            else
            {
                //ViewBag.StudentEmail = null;

                ViewBag.Residence = new SelectList(db.Residences, "ResName", "ResName", vm.Residence);

            }
            ViewBag.Residence = new SelectList(db.Residences, "ResName", "ResName", vm.Residence);
            ViewBag.StudentEmail = new SelectList(db.Students, "Email", "Email", vm.StudentEmail);

            return View();
        }
        [HttpPost]

        public ActionResult MessageTo1(MessageReplyViewModel message)
        {
            DateTime now = DateTime.UtcNow;


            int ms = db.Messages.Select(x=>x.Id).Max();
            var mmsg = db.Messages.ToList().FindAll(x => x.Id == ms).FirstOrDefault();
            Notification not = new Notification();
            EmailService email = new EmailService();
            Guid d = new Guid();
            Random r = new Random();
           
            var students = db.Students.ToList().Where(x => x.ResName == message.Residence);
          

            if (mmsg.To == "Student")
            {
                not.reference = ms;
                not.NotID = d.ToString() + r.Next(1, 100);
                not.Reciever = message.StudentEmail;
                not.message = message.StudentEmail + " you have a new n otification " + " \n" + mmsg.MessageToPost;
                not.NotType = mmsg.Subject;
                //ViewBag.Residence = null;
                not.NotDate = DateTime.Now;

                not.seen = false;


                db.Notifications.Add(not);
                ViewBag.StudentEmail = new SelectList(db.Students, "Email", "Email", message.StudentEmail);
                mmsg.StudentEmail = message.StudentEmail;
            }
            else
            {
                //mmsg.StudentEmail = null;
                ViewBag.Residence = new SelectList(db.Residences, "ResName", "ResName", message.Residence);
                mmsg.Residence = message.Residence;
                foreach (var item in students)
                {
                    email.sendNoticeEmail(item.Email, mmsg.MessageToPost, DateTime.Now.Date.ToLongDateString(), DateTime.Now.TimeOfDay.ToString(), mmsg.Subject);
                }

            }
            db.Entry(mmsg).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index1","Home");
        }

        public ActionResult DeleteMessage(int id)
        {
            Message _messageToDelete = db.Messages.Find(id);
            db.Messages.Remove(_messageToDelete);
            db.SaveChanges();

          

            return RedirectToAction("NoticeIndex");

        }




        public ActionResult MessageDetails(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            Notification not = db.Notifications.ToList().Find(x => x.reference == id);
            not.seen = true;
            db.Entry(not).State = EntityState.Modified;
            db.SaveChanges();
            return View(message);
        }
        BsLogic l = new BsLogic();
        public ActionResult AllNotices()
        {
            var username = User.Identity.GetUserName();
            return View(db.Notifications.Where(x=>x.Reciever==username).ToList());
        }
    }
}
