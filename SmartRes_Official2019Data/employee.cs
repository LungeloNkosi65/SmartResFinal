using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SmartRes_Official2019Data
{
    public class employee
    {

        [Key]
        public string EmpID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        [Display(Name = "Employee Type")]
        public string EmpTYpe { get; set; }

    }
}