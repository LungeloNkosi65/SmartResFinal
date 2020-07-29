using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace SmartRes_Official2019Data
{
    public class customerSt
    {
        [Key]
        [DisplayName("Customer ID")]
        public string customer_id { get; set; }
        //[DisplayName("Join Date")]
        //[DataType(DataType.DateTime)]
        //public DateTime join_date { get; set; }
        [DisplayName("First Name")]
        public string NameIdentifier { get; set; }
        [DisplayName("Last Name")]
        public string last_name { get; set; }
        [DisplayName("Profile picture")]
        public byte[] UserPhoto { get; set; }
        [DisplayName("Mobile")]
        public string mobile { get; set; }
        [DisplayName("Email ID")]
        [DataType(DataType.EmailAddress)]
        public string email_id { get; set; }
        [DisplayName("Address")]
        public string address { get; set; }
        //[DisplayName("Birth Date")]
        //[DataType(DataType.DateTime)]
        //public DateTime birth_date { get; set; }
        [DisplayName("Gender")]
        public string gender { get; set; }
        [DisplayName("Status")]
        public string status { get; set; }

       
       
     
    }
}