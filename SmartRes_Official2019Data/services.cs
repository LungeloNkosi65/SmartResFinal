using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace SmartRes_Official2019Data
{
    public class services
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Service Name")]
        public string service_name { get; set; }
        [DisplayName("Service Code")]
        public string service_code { get; set; }
      
    }
}