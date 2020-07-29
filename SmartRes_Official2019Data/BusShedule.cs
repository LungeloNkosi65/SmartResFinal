using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SmartRes_Official2019Data
{
    public class BusShedule
    {
        [Key]
        public int ScheduleId { get; set; }
        public int WkdId { get; set; }
        //public virtual WeekDays WeekDays { get; set; }
        [DisplayName("Day Of Week")]

        public string CurrDayofWeek { get; set; }
        [Display(Name = "Bus Number"), Required]
        public string BusNumber { get; set; }
        [/*DataType(DataType.Time),*/Required]
        public string TimeODay { get; set; }
        public string Destination { get; set; }

        ApplicationDbContext db = new ApplicationDbContext();

        public string getDay()
        {
            var dy = (from C in db.WeekDays
                      where (WkdId == C.WkdId
                      )

                      select C.CurrDayofWeek).FirstOrDefault();
            return dy;

        }

    }
}
