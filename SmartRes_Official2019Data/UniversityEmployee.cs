using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace SmartRes_Official2019Data
{
    public class UniversityEmployee
    {
        [Key]
        public string EmpUId { get; set; }
        [ForeignKey("University")]
        public int UnivbersityId { get; set; }
        public virtual University University { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        [Display(Name = "Employee Type")]
        public string EmpTYpe { get; set; }

        public byte[] UserPhoto { get; set; }


    }
}