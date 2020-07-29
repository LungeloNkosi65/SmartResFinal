using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SmartRes_Official2019Data
{
    public class RoomType
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Room Type ID")]
        public int RtypeId { get; set; }
        [DisplayName("Room Type"),Required]
        public string RmType { get; set; }
        [DisplayName("Number of Occupants"),Required]
        public int NumbOcupants { get; set; }
    }
}