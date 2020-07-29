using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace SmartRes_Official2019Data
{
  public  class Students_Dummy
    {
        [Key,DisplayName("Student Number")]
        [StringLength(8, MinimumLength = 8), RegularExpression("^[0-9]*$", ErrorMessage = "only numeric value accepted starting from 1")]


        public string StudentNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Gender { get; set; }
        [DisplayName("Phone Number"),DataType(DataType.PhoneNumber),Required]

        public string PhoneNumber { get; set; }
        [DisplayName("Student Email"),DataType(DataType.EmailAddress),Required]

        public string StudentEmail { get; set; }
        [DisplayName("Id Number"),Required]
        public double IdNumber { get; set; }
        [Display(Name ="Student Type"),Required]
        public string Student_Type { get; set; }
    }
}
