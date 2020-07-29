using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRes_Official2019Data
{
    public class Student_Otp
    {
        [Key]
        public string StudentId { get; set; }
        [DisplayName("One Time Pin"),Required]

        public string OTP { get; set; }
        [DisplayName("OTP Status")]
        public string OTP_Status { get; set; }
        [DisplayName("Id Number")]
        public string Id { get; set; }


        ApplicationDbContext db = new ApplicationDbContext();
        
        public bool CheckOTP()
        {
            bool result = false;
            var otps = db.Student_Otp.ToList();
            foreach(var item in otps)
            {
                if (OTP == item.OTP)
                {
                    result = true;
                }
            }
            return result;

        }

    }
}