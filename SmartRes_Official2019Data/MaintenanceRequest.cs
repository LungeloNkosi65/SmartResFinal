using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace SmartRes_Official2019Data
{
    public class MaintenanceRequest
    {
        [Key]
        public int MaintenanceId { get; set; }
        [DisplayName("User Email")]

        public string UserName { get; set; }
        [DisplayName("Room Number")]

        public string RoomNumber { get; set; }
        public virtual Room Room { get; set; }
        [Display(Name = "Maintenance Description")]
        public string MainIssue { get; set; }
        public string Status { get; set; }
        [DisplayName("Date Lodged")]

        public string DateLoged { get; set; }

        public byte[] Image { get; set; }

        public string ResidenceName { get; set; }
        [Display(Name = "Employee Email")]

        public string EmployeeEmail { get; set; }
        [Display(Name = "Date Fixed"), DataType(DataType.Date)]

        public string DateFixed { get; set; }

        public string StatusQpproved { get; set; }

        [Display(Name = "Cost")]
        public double IssueCost { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date available for repairs")]
        public DateTime RoomAval { get; set; }
        [DisplayName("Time available for repairs"),DataType(DataType.Time)]
        public DateTime TimeAvailable { get; set; }
        public string Description { get; set; }


       
    }
}
