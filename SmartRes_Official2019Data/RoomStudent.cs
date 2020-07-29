using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRes_Official2019Data
{
    public class RoomStudent
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID"), Required]
        public int RStudentId { get; set; }
        [DisplayName("Student Name"), Required]
        [ForeignKey("Student")]
        public string StudId { get; set; }
        public virtual Student Student { get; set; }
        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        [DisplayName("Date Assigned"),DataType(DataType.Date)]
        public DateTime Date_Assigned { get; set; }
            public string em { get; set; }
        ApplicationDbContext db = new ApplicationDbContext();

        public void updateSpace()
        {
            var qtyUpdate = db.Rooms.Find(RoomId);
            qtyUpdate.Space = qtyUpdate.Space - 1;
            if(qtyUpdate.Space<=0)
            {
                qtyUpdate.Status = "Taken";
            }
            db.Entry(qtyUpdate).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

        }

        public bool checkAvailabilty()
        {
            bool result = false;
            var getAva = (from R in db.Rooms
                          where RoomId == R.RoomId
                          select R.Space).FirstOrDefault();
             if(getAva!=0)
            {
                result = true;
            }
            return result;
        }
        public void UpdateStatus()
        {
            var qtyUpdate = db.Rooms.Find(RoomId);
            
            if(qtyUpdate.Space==0)
            {
                qtyUpdate.Status = "Taken";
            }
            
          
            db.Entry(qtyUpdate).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
       

        public bool CheckStatus()
        {
            bool result = false;
            var status = (from R in db.Rooms
                          where (RoomId == R.RoomId)
                          select R.Status).FirstOrDefault();
            if(status=="Taken")
            {
                result = true;
            }
            return result;
        }

        public bool CheckStudent(string Stud)

        {
            var getName = (from S in db.RoomStudents
                           where StudId == S.StudId
                           select S.Student.Name).FirstOrDefault();
            bool resulu = false;
            if(getName==null)
            {
                resulu = true;
            }
            return resulu;
        }

        //public bool cheAccount(string eml)
        //{
        //    bool check = false;

        //    ApplicationDbContext db = new ApplicationDbContext();
        //    var em = (from gh in db.ElectricityAccount
        //              where gh.meterNo == eml
        //              select gh.meterNo).FirstOrDefault();
        //    if (em == null)
        //    {
        //        check = true;
        //    }
        //    return (check);
        //}
    }
}