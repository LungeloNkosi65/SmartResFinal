using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace SmartRes_Official2019Data
{
    public class OrderList
    {
        [Key]
        [DisplayName("Invoice Number")]
        public string InvoiceNo { get; set; }
        [DisplayName("Date")]
        public DateTime OrderDate { get; set; }
        [DisplayName("Quantity")]
        public int qty { get; set; }
        [DataType(DataType.Currency)]
        [DisplayName("Amount")]
        public double amt { get; set; }
        [DisplayName("Status")]
        public string status { get; set; }
        [DisplayName("Customer Email")]
        public string customerID { get; set; }



        public string  invoiceNum()
        {
            string id = customerID.Substring(0,4);
            string year = (DateTime.Now.Year).ToString();
            year = year.Remove(1, 1);
            Random ran = new Random();
            string number = ran.Next(1000, 99999).ToString();
            return year + number+id;

        }



        public ApplicationDbContext db = new ApplicationDbContext();
        public int Quantiy()
        {

            var q = (from c in db.customer_Orders
                     where customerID == c.customer_Id
                     select c.total_qty).Sum();

            return q;

        }

        public double  SumTotal()
        {

            var q = (from c in db.customer_Orders
                     where customerID == c.customer_Id
                     select c.total_paid).Sum();

            return q;

        }



        public string GenInvoice()
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