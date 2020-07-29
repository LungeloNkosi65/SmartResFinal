using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartRes_Official2019Data;
namespace SmartRes_Official2019Logic
{
    public class Address_Service
    {
        private ApplicationDbContext dataContext;

        public Address_Service()
        {
            this.dataContext = new ApplicationDbContext();
        }
        public List<Shipping_Address> GetOrderAddresses()
        {
            return dataContext.Shipping_Addresses.ToList();
        }
        public void AddShippingAddress(Shipping_Address order_Address)
        {
            try
            {
                dataContext.Shipping_Addresses.Add(order_Address);
                dataContext.SaveChanges();
            }
            catch (Exception ex) { }
        }
        public Shipping_Address GetShippingAddress(int address_id)
        {
            return dataContext.Shipping_Addresses.FirstOrDefault(x => x.Address_ID == address_id);
        }
        public Shipping_Address GetShippingAddress(string order_id)
        {
            return dataContext.Shipping_Addresses.FirstOrDefault(x => x.Order_ID == order_id);
        }
    }
}
