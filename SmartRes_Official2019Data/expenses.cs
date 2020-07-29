using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel;

namespace SmartRes_Official2019Data
{
    public class expenses
    {
        [Key]
        [DisplayName("ID")]
         public int exp_id { get; set; }
        [ForeignKey("expense_type")]

        public string exps_id { get; set; }
        public virtual expense_type expense_type { get; set; }
        [DisplayName("Employee Name")]
        public string emp_name { get; set; }
        [DisplayName("Date")]
        [DataType(DataType.DateTime)]
        public DateTime exps_date { get; set; }
        public string exp_payee_name { get; set; }
        [DisplayName("Expense Type")]
        public string exp_type { get; set; }
        public string exp_amt { get; set; }
        [DisplayName("Reason")]
        public string exp_remarks { get; set; }


      
    }
}