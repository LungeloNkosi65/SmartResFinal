using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRes_Official2019Data
{
    public class Maintenance_Check
    {
        [Key]
        public int MianChecId { get; set; }
        public int MaintenanceId { get; set; }
         
        public virtual MaintenanceRequest MaintenanceRequest { get; set; }

        [DisplayName("Employee Username")]

        public string EmployeeUserName { get; set; }

        [DisplayName("Status Done")]


        public string Status_Done { get; set; }
        [DisplayName("Status Approval")]

        public string Status_Check { get; set; }

        [DisplayName("Date Approved")]

        public string Date_Approved { get; set; }

        ApplicationDbContext db = new ApplicationDbContext();
        public void UpdateStatus()
        {
            var ma = db.MaintenanceRequests.Find(MaintenanceId);
            if (Status_Check == "Rejected")
            {
                ma.Status = "Rejected";
            }
            else
            {
                ma.Status = "Fixed";

            }
            db.Entry(ma).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }


    }
}