using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SmartRes_Official2019Data
{
    public class FoodOrder
    {
        [Key]
        public string cart_item_id { get; set; }
        public string cart_id { get; set; }
        public int item_id { get; set; }
        [Display(Name = "Quantity")]

        public int quantity { get; set; }
        [DataType(DataType.Currency),Display(Name ="Price")]
        public double price { get; set; }
        [Display(Name ="Meal Name")]
        public string ItemName { get; set; }
        [Display(Name = "User Email")]

        public string UserEmail { get; set; }

        [Display(Name = "Picture")]
        //[DataType(DataType.Upload)]
        public byte[] Picture { get; set; }
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }
        [Display(Name ="Order Status")]
        public string OrderStatus { get; set; }
        [Display(Name ="Order Date")]
        public string OrderDate { get; set; }
        [Display(Name ="Day Of Week")]
        public string DayOfWik { get; set; }

        public int? OrderId { get; set; }

        ApplicationDbContext db = new ApplicationDbContext();
        //public void UpdateQuantity()
        //{
        //    var qty = db.Items.Find(item_id);
        //    qty.QuantityInStock = qty.QuantityInStock - 1;
        //    db.Entry(qty).State = EntityState.Modified;
        //    db.SaveChanges();

        //}
    }
    public class FoodDeliveryChoice
    {
        [Key]
        public int  FdeliveryId{ get; set; }
        [Display(Name ="Residence Name")]
        public string ResName { get; set; }
        [Display(Name ="Room Number")]
        public string RoomNumber { get; set; }
        [Display(Name ="Decision Choice")]
        public string Decidionchoice { get; set; }
        public string UserEmail { get; set; }
    }
}