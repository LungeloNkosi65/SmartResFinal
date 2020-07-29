using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace SmartRes_Official2019Data
{
    public class SAN_Employee
    {
        [Key]
        [DisplayName("Employee ID")]
        public string EmpID { get; set; }
        [DisplayName("Name"), Required]
        public string Name { get; set; }
        [DisplayName("Surname"), Required]
        public string Surname { get; set; }
        [DisplayName("Phone Number"), DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Eamil { get; set; }
        [DisplayName("Gender"), Required]
        public string Gender { get; set; }
    }
}