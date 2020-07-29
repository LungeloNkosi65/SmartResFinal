using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;




namespace SmartRes_Official2019Data
{

    public class Residence
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Check In ID"),Required]
        public int ResId { get; set; }
        [ForeignKey("University")]
        [DisplayName("University ID"),Required]
        public int UnivbersityId { get; set; }
        public virtual University University { get; set; }
        [DisplayName("Residence Name"),Required]
        public string ResName { get; set; }
        [DisplayName("Address"),Required]
        public string ResAddress { get; set; }
        [Required]
        public double Capacity { get; set; }
        [DisplayName("Gender Allocation"),Required]
        public string Gender_Allocation { get; set; }
        [DisplayName("Building Photo")]
        public byte[] ResidentPhoto { get; set; }


    }
}