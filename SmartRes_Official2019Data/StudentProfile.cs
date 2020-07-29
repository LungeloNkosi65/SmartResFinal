using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRes_Official2019Data.ViewModels
{
    public class StudentProfile
    {
        [Key]
        public int ProfileId { get; set; }
        [DisplayName("Student Number")]
        public string StudentNumber { get; set; }
        [DisplayName("Initials & Surname")]

        public string InitialSurname { get; set; }
        [DataType(DataType.PhoneNumber), DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        [DisplayName("Id Number")]
        public double IdNumber { get; set; }
        [EmailAddress]

        [DisplayName("Residence Name")]
        public string ResName { get; set; }
    
        [DisplayName("Room Number")]
        public string RooNumber { get; set; }
        public string Email { get; set; }
    }
}