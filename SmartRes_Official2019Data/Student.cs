using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;




namespace SmartRes_Official2019Data
{
    public class Student
    {
        [Key]
        [DisplayName("Student ID")]
        public string StudId { get; set; }
        //[StringLength(8, MinimumLength = 8), RegularExpression("^[0-9]*$", ErrorMessage = "only numeric value accepted starting from 1")]
        [DisplayName("Student Number")]
        public string StudentNumber { get; set; }
        public string Name { get; set; }
        [DisplayName("Initila & Surname")]
        public string Surname { get; set; }
        [DataType(DataType.PhoneNumber), DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        [DisplayName("Id Number")]
        public double IdNumber { get; set; }
        [DisplayName("Gender")]
        public string Gender { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Residence Name")]
        public string ResName { get; set; }

        [DisplayName("Room Number")]
        public string RooNumber { get; set; }
        [DisplayName("Profile picture")]
        public byte[] UserPhoto { get; set; }


        public string IdForOut { get; set; }


    }
}