using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRes_Official2019Data
{
    public class Check_In
    {
        [Key]
        [DisplayName("Check In ID")]
        public int CheckinId { get; set; }
        [ForeignKey("Student")]
        [DisplayName("Student ID")]
        public string StudId { get; set; }
        public virtual Student Student { get; set; }
        [ForeignKey("Residence")]
        [DisplayName("Res ID")]
        public int ResId { get; set; }
        public virtual Residence Residence { get; set; }
        [Display(Name ="Date In"),DataType(DataType.Date)]
        public DateTime Date_In { get; set; }
        ApplicationDbContext db = new ApplicationDbContext();
        public void UpdateCheckIn()
        {
            var qtyUpdate = db.ResAvailabilities.Find(ResId);

            qtyUpdate.CheckedIN +=1;

            db.Entry(qtyUpdate).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public bool CheckStudent(string Stud)
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

    }



    public class CheckeOut
    {
        [Key]
        public string CheckOutId { get; set; }
        [Display(Name ="Student Number")]

        public string StudentNumber { get; set; }
        [Display(Name ="Student Email")]

        public string StudentEmail { get; set; }
        [Display(Name = "Room Number")]

        public string RoomNumber { get; set; }
        [Display(Name = "Residence")]

        public string ResName { get; set; }
        [Display(Name = "Check Out Date"),DataType(DataType.Date)]

        public DateTime CheckOutDate { get; set; }
        [Display(Name = "Check In Date"), DataType(DataType.Date)]

        public DateTime DateIn { get; set; }
        public string Status { get; set; }

    }
}