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
    public class TemporaryBooking
    {
        [Key]
        [DisplayName("Booking ID")]
        public int BookingId { get; set; }
        [DisplayName("Student Number")]
        public string StudentNumber { get; set; }

        public string BookedUniversity { get; set; }
        [DisplayName("ID Number")]
        public double IDNumber { get; set; }


        [ForeignKey("Residence")]
        public int ResId { get; set; }


        [DisplayName("OTP Code")]
        public string OTPCode { get; set; }
        public string Gender { get; set; }

        public Nullable<System.DateTime> BookingDate { get; set; }

        public virtual Residence Residence { get; set; }

        ApplicationDbContext db = new ApplicationDbContext();


        //public bool CheckStudent(string Stud)

        //{
        //    var getIdNumber = (from S in db.TemporaryBookings
        //                   where BookingId == S.BookingId
        //                   select S.IDNumber).ToList();
        //    bool resulu = false;
        //    foreach()
        //    //if (getIdNumber != IDNumber)
        //    //{
        //    //    resulu = true;
        //    //}
        //    return resulu;
        //}
        public string getResName()
        {
            var resName = (from R in db.Residences
                           where ResId == R.ResId
                           select R.ResName).FirstOrDefault();

            return resName.ToString();
        }

        public string generateOTPCode()
        {
            string OTPCodeGenerated = "";

            string firstTwo = (getResName().Substring(0, 2)).ToString();
            string lastTwo = (getResName().Substring((getResName().Length) - 2)).ToString();
            Random ran = new Random();
            int num = ran.Next(1, 101);

            OTPCodeGenerated = (firstTwo + lastTwo).ToUpper() + "-" + (num.ToString());

            return OTPCodeGenerated;
        }

        //public int getBookedSpaces()
        //{
        //    var bookedspaces = (from R in db.Residences
        //                        where ResId == R.ResId
        //                        select R.BookedSpaces).FirstOrDefault();

        //    return Convert.ToInt32(bookedspaces);
        //}

        public int getCapacity()
        {
            var capacity = (from R in db.Residences
                            where ResId == R.ResId
                            select R.Capacity).FirstOrDefault();

            return Convert.ToInt32(capacity);
        }

        public int getAvailableCapacity()
        {
            var availableCapacity = (from R in db.ResAvailabilities
                                     where ResId == R.ResId
                                     select R.NumAvailable).FirstOrDefault();

            return Convert.ToInt32(availableCapacity);
        }

        //public void assignResToStudent()
        //{
        //    var resUpdate = db.Students.Find(StudId);
        //    resUpdate.ResId = ResId;
        //    db.Entry(resUpdate).State = System.Data.Entity.EntityState.Modified;
        //    db.SaveChanges();


        //}

        //public void UpdateBookedSpaces()
        //{
        //    var spaceUpdate = db.ResAvailabilities.Find(ResId);
        //    spaceUpdate.BookedSpaces +=1;
        //    db.Entry(spaceUpdate).State = System.Data.Entity.EntityState.Modified;
        //    db.SaveChanges();
        //}
        public void UpdateCapacity()
        {
            var qtyUpdate = db.ResAvailabilities.Where(x => x.ResId == ResId).FirstOrDefault();
            qtyUpdate.NumAvailable -= 1;
            qtyUpdate.BookedSpaces += 1;

            db.Entry(qtyUpdate).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

    }
}