using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Data.Entity;
using SmartRes_Official2019Data;

namespace SmartRes_Official2019Data
{
    public class customer_order
    {
        [Key]
        [DisplayName("Order ID")]
        public int ID { get; set; }
        [DisplayName("Invoice Number")]
        public string invoice_no { get; set; }
        [DisplayName("Order Date")]
        [DataType(DataType.Date)]
        public string order_date { get; set; }
        [DisplayName("Order month")]
        public string order_month { get; set; }
        [DisplayName("Customer ID")]
        public string customer_Id { get; set; }
        [DisplayName("Quanity")]
        public int total_qty { get; set; }
        [DisplayName("Discount")]
        public int discount { get; set; }
        [DisplayName("Tax")]
        public double service_tax { get; set; }
        [DataType(DataType.Currency)]
        [DisplayName("Balance")]
        public double total_paid { get; set; }
        [DataType(DataType.Currency)]
        [DisplayName("Price")]
        public double total_balance { get; set; }
        [DisplayName("Delivery Date")]
        [DataType(DataType.Date)]
        public string delivery_date { get; set; }
        [DisplayName("Remark")]
        public string remarks { get; set; }
        [DisplayName("Order Status")]
        public string order_status { get; set; }

        [DisplayName("Name")]
        public string CustomerName { get; set; }

        [ForeignKey("Cloths")]
        public int ClothsID { get; set; }
        public virtual Cloths Cloths { get; set; }

        [ForeignKey("services")]

        public int ServiceID { get; set; }
        public virtual services services { get; set; }

      

        public double itermprice()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            
            var pric = (from C in db.PriceLists
                       where ServiceID == C.ServiceID
                       select C.price).FirstOrDefault();
            return pric;

        }

     

        public double TotalPrice()

        {

            return total_qty * itermprice();
        }


        //public string DeliveryDate()

        //{

        //    if (order_status == "Done")
        //    {

        //        return DateTime.Now.ToShortDateString();
        //    }
            

        //        return "Pending";

        //}

        public double CustomerOrdersTotal()
        {
            
            IEnumerable<double> SumOrders = new List<double> { total_balance };
            return SumOrders.Sum();
        }

        //public string UpdateStaus()
        //{
          
        //    return "Pending";

        //}


        public string InvoiceNumber()
        {
            int rand, rand2;
            Random random = new Random();
            rand= random.Next(1, 10000);
            rand2= random.Next(10, 100);

            string month_day = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString();

            return month_day + rand + rand2;


        }
        ApplicationDbContext db = new ApplicationDbContext();

        
        public void UpdateList()
        {
            var Odl = db.OrderLists.Find(customer_Id);
            Odl.qty = Odl.qty + total_qty;
            Odl.amt = Odl.amt + total_balance;
            db.Entry(Odl).State = EntityState.Modified;
            db.SaveChanges();
        }




    }
}