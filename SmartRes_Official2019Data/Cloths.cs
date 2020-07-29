using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
namespace SmartRes_Official2019Data
{
    public class Cloths
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Clothing Type")]
        public string cloth_Type { get; set; }
        [DisplayName("Clothing Code")]
        public string clothCode { get; set; }
        [DisplayName("Clothing Image")]
        public byte[] cloth_image { get; set; }


    }
}