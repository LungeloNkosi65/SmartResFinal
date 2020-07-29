using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SmartRes_Official2019Data
{
    public class expense_type
    {
       [Key]
       [DisplayName("ID")]
       public string exps_id { get; set; }

       [DisplayName("Expense Type")]
       public string exps_type { get; set; }
       [DisplayName("Code")]
       public string exps_code { get; set; }


     

    }
}