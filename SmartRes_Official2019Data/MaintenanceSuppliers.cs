using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartRes_Official2019Data
{
    public class MaintenanceSuppliers
    {
        [Key]
        public int SupplierID { get; set; }
        public string Supplier { get; set; }
        [DisplayName("Product")]
        public string Supplier_Product { get; set; }
        [DisplayName("Cost of product")]
        public double Product_Cost { get; set; }
        [DisplayName("Quantity ")]
        public double Product_Quantity { get; set; }
        [EmailAddress]
        [DisplayName("Email")]
        public string Supplier_Email { get; set; }
    }
}