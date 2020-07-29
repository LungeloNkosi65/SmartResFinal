using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SmartRes_Official2019Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public System.Data.Entity.DbSet<Student> Students { get; set; }
        public System.Data.Entity.DbSet<BusShedule> BusShedules { get; set; }
        public System.Data.Entity.DbSet<WeekDays> WeekDays { get; set; }

        //public System.Data.Entity.DbSet<SmartRes_Official2019.Models.Category> CategoriesN { get; set; }
        //public System.Data.Entity.DbSet<SmartRes_Official2019.Models.OrderDetails> OrderDetails { get; set; }
        //public System.Data.Entity.DbSet<SmartRes_Official2019.Models.Order> OrdersN { get; set; }
       public DbSet<Event> Events { get; set; }

        public System.Data.Entity.DbSet<SAN_Employee> SAN_Employee { get; set; }

        public System.Data.Entity.DbSet<University> Universities { get; set; }

        public System.Data.Entity.DbSet<Residence> Residences { get; set; }
        //Role Management
        public DbSet<IdentityUserRole> UserInRole { get; set; }
        public DbSet<Students_Dummy> Students_Dummies { get; set; }
        public DbSet<ApplicationRole> appRoles { get; set; }

        public System.Data.Entity.DbSet<RoomType> RoomTypes { get; set; }

        public System.Data.Entity.DbSet<Room> Rooms { get; set; }

        public System.Data.Entity.DbSet<CheckeOut> CheckOuts { get; set; }
        public System.Data.Entity.DbSet<Check_In> Check_In { get; set; }


        public System.Data.Entity.DbSet<RoomStudent> RoomStudents { get; set; }
        public System.Data.Entity.DbSet<PDF> PDFs { get; set; }
        public System.Data.Entity.DbSet<UniversityEmployee> UniversityEmployees { get; set; }
        public System.Data.Entity.DbSet<ResAvailability> ResAvailabilities { get; set; }

        public System.Data.Entity.DbSet<TemporaryBooking> TemporaryBookings { get; set; }

        public System.Data.Entity.DbSet<Student_Otp> Student_Otp { get; set; }

        public System.Data.Entity.DbSet<StudentCheckIn> StudentCheckIns { get; set; }

        public System.Data.Entity.DbSet<Medical_Info> Medical_Info { get; set; }

        public System.Data.Entity.DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }


        public DbSet<expense_type> expense_Types { get; set; }
        public DbSet<expenses> expenses { get; set; }
        public DbSet<customerSt> custom { get; set; }
        public DbSet<PriceList> PriceLists { get; set; }
        public DbSet<services> Services { get; set; }
        public DbSet<Cloths> cloths { get; set; }
        public DbSet<employee> employees { get; set; }
        public DbSet<customer_order> customer_Orders { get; set; }
        public DbSet<OrderList> OrderLists { get; set; }
        public DbSet<FoodOrder> FoodOrders { get; set; }

        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        //public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Shipping_Address> Shipping_Addresses { get; set; }
        public DbSet<MealOrder> MealOrders { get; set; }
        public DbSet<Order_Item> Order_Items { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Cart_Item> Cart_Items { get; set; }
        public DbSet<Cart> Carts { get; set; }
     
        public DbSet<Supplier> Suppliers { get; set; }
      
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<FoodDeliveryChoice> FoodDeliveryChoice { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public DbSet<MaintenanceSuppliers> maintenanceSuppliers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<SupplierRequest> SupplierRequests { get; set; }
        public DbSet<MaintenanceReservation> MaintenanceReservations { get; set; }




    }
}