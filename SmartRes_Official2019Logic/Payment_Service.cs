using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartRes_Official2019Data;

namespace SmartRes_Official2019Logic
{
    public class Payment_Service
    {
        private ApplicationDbContext dataContext;

        public Payment_Service()
        {
            this.dataContext = new ApplicationDbContext();
        }

        public List<Payment> GetPayments()
        {
            return dataContext.Payments.ToList();
        }
        public Payment GetOrderPayment(string order_Id)
        {
            return GetPayments().FirstOrDefault(x => x.Order_ID == order_Id);
        }
    }
}