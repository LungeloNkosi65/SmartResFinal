using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel;

namespace SmartRes_Official2019Data
{
    public class PriceList
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Cloths")]
        [ForeignKey("Cloths")]
        public int ClothsID { get; set; }
        public virtual Cloths Cloths { get; set; }
        [ForeignKey("services")]

        public int ServiceID { get; set; }
        public virtual services services { get; set; }
        [DisplayName("Price"),DataType(DataType.Currency)]
        public double price { get; set; }
        
    }
}