using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SmartRes_Official2019Data
{
   public class MaintenanceReservation
    {
        [Key]
        public int ReservationID { get; set; }
        public int MaintenanceId { get; set; }
        public string Rooms { get; set;}
      
        public string ReservationDescription { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date room will be available for repairs")]
        public DateTime ReservationDate { get; set; }
    }
}
