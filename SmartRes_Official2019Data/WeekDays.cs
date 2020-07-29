using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SmartRes_Official2019Data
{
    public class WeekDays
    {
        [Key]
        public int WkdId { get; set; }
        [DisplayName("Day Of Week"), Required]
        public string CurrDayofWeek { get; set; }
    }
}