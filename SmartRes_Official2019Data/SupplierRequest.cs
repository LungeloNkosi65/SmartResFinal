using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRes_Official2019Data
{
    public class SupplierRequest
    {
        [Key]
        public int SupplierRequestID { get; set; }
        [ForeignKey("MaintenanceSuppliers")]
        public int SupplierID { get; set; }
        public virtual MaintenanceSuppliers MaintenanceSuppliers { get; set; }
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [DisplayName("Product Quantity")]
        public int ProductQuantity { get; set; }
        public string email { get; set; }
    }
}
