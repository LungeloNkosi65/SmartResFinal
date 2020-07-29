using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRes_Official2019Data
{
    public class University
    {

        [Key]
        [DisplayName("University ID")]
        public int UnivbersityId { get; set; }
        [DisplayName("University Name"),Required]
        public string UniversityName { get; set; }
        [DataType(DataType.EmailAddress),Required]
        public string Email { get; set; }
        [DisplayName("Phone Number")]
        [DataType(DataType.PhoneNumber),Required]
        public string PhoneNumber { get; set; }
        [DisplayName("University Logo")]
        public byte[] UniversityLogo { get; set; }
    }
}