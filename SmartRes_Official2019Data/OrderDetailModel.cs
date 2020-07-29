using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRes_Official2019Data
{
    public class OrderDetailModel
    {
        //public Order order { get; set; }
        public string shipping_method { get; set; }
        public Order_Delivery delivery { get; set; }
        public Shipping_Address address { get; set; }
        [Display(Name = "Payment Method")]
        public string payment_Method { get; set; }
        public Payment payment { get; set; }
        public List<Order_Item> order_items { get; set; }
        [Display(Name = "Order Total")]
        [DataType(DataType.Currency)]
        public decimal order_total { get; set; }
    }


    public class MealOrder
    {
        [Key]
        [Display(Name = "Order Number")]

        public int OrderId { get; set; }
        [Display(Name = "Order Number")]
        public string OrderNumber { get; set; }
        [Display(Name = "User Email")]

        public string UserOrder { get; set; }
        [DataType(DataType.Currency)]
        public double Total { get; set; }
        public string Status { get; set; }
        [Display(Name = "Order Date"),DataType(DataType.Date)]

        public string OrderDate { get; set; }
        public string GenVoucher()
        {

            Guid g = Guid.NewGuid();
            Random rn = new Random();
            string gs = g.ToString();
            int randomInt = rn.Next(10, 15 + 1);
            string voucher = gs.Substring(gs.Length - randomInt - 1, randomInt);
            return voucher;

        }
    }
}
