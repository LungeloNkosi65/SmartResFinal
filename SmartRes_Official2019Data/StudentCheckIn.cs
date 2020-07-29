using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartRes_Official2019Data;

namespace SmartRes_Official2019Data
{
    public class StudentCheckIn
    {
        [Key]
        public string CheckIn_Id { get; set; }

        [ForeignKey("Student")]
        [DisplayName("Student ID")]
        public string StudId { get; set; }
        public virtual Student Student { get; set; }
        [DisplayName("Res ID")]
        public int ResId { get; set; }
        public virtual Residence Residence { get; set; }

        [DisplayName("Residence Name")]
        public string ResName { get; set; }
        [Display(Name = "Date In"), DataType(DataType.Date)]
        public DateTime Date_In { get; set; }
        public int RoomId { get; set; }
        [DisplayName("Room Number")]
        public string RooNumber { get; set; }
        ApplicationDbContext db = new ApplicationDbContext();
        public String GetRoomNumber()
        {
            var room = (from rm in db.Rooms
                        where RoomId == rm.RoomId
                        select rm.RoomNumber).FirstOrDefault();
            return room;
        }
        public bool CheckRoomGender(string StEmail)
        {
            bool result = false;
            var roomFromCheckin = (from r in db.Rooms
                                   where RoomId == r.RoomId
                                   select r.RoomNumber).FirstOrDefault();

            var ResidenceFromCheckin = (from r in db.Rooms
                                   where RoomId == r.RoomId
                                   select r.Residence.ResName).FirstOrDefault();

            var getGenderinRoom = (from s in db.Students
                                   where roomFromCheckin==s.RooNumber
                                   select s.Gender).FirstOrDefault();

            var CheckingINGender = (from s in db.Students
                                    where StEmail == s.Email
                                    select s.Gender).FirstOrDefault();

            if (CheckingINGender == getGenderinRoom  || getGenderinRoom==null)
            {
                result = true;
            }



            
            return result;
        }

        public void UpdateCheckIn()
        {
            var qtyUpdate = db.ResAvailabilities.Where(x => x.ResId == ResId).FirstOrDefault();
            qtyUpdate.CheckedIN += 1;
            db.Entry(qtyUpdate).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }



        public void UpdateProfile()
        {
            var qtyUpdate = db.Students.Find(StudId);
            qtyUpdate.RooNumber = RooNumber;

            db.Entry(qtyUpdate).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }


        public bool CheckStudent()
        {
            var getName = (from S in db.RoomStudents
                           where StudId == S.StudId
                           select S.Student.Name).FirstOrDefault();
            bool resulu = false;
            if (getName == null)
            {
                resulu = true;
            }
            return resulu;
        }

        public bool checkRoomStatus()
        {
            var status = (from rm in db.Rooms
                          where RoomId == rm.RoomId
                          select rm.Status).FirstOrDefault();
            bool result = true;
            if (status == "Taken")
            {
                result = false;
            }
            return result;
        }
        public void UpdateStatus()
        {
            var qtyUpdate = db.Rooms.Find(RoomId);

            if (qtyUpdate.Space == 0)
            {
                qtyUpdate.Status = "Taken";
            }


            db.Entry(qtyUpdate).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public void updateSpace()
        {
            var qtyUpdate = db.Rooms.Find(RoomId);
            qtyUpdate.Space = qtyUpdate.Space - 1;
            if (qtyUpdate.Space <= 0)
            {
                qtyUpdate.Status = "Taken";
            }
            db.Entry(qtyUpdate).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

        }

        public string getStudentNUmber()
        {
            Student st = new Student();
            StudentCheckIn stchk = new StudentCheckIn();
            var stnum = (from Sta in db.Students
                         where (stchk.StudId == st.StudId
                         )
                         select st.StudentNumber).FirstOrDefault();
            return stnum;
        }


    }
}