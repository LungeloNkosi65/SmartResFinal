using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace SmartRes_Official2019Data

{
    public class Medical_Info
    {
        [Key]
        public string MedId { get; set; }
        [Display(Name ="Student Email")]
        public string StudentEmail { get; set; }
        [DisplayName("Do you have medical aid?")]
        public string Have_Med { get; set; }
        
        [DisplayName("Medical Aid Numer")]
        public string Med_Number { get; set; }
        [DisplayName("Medical Aid Name")]
        public string Med_Name { get; set; }
        [DisplayName("Medical Aid Owner")]
        public string Owner_Name { get; set; }
        [DisplayName("Do you have any ilnesses?")]
        public string Illness { get; set; }
        [DisplayName("Illness Description")]
        public string Illness_Description { get; set; }
        [DisplayName("Do you take any chronic medication?")]
        public string Chronic_Medication { get; set; }
        [DisplayName("Chronical Medication Description")]
        public string Chronic_Description { get; set; }
        [DisplayName("Parent/Guadian Contact Number")]
        public string Parent_Details { get; set; }
        [DisplayName("Parent/Guadian Contact Name")]
        public string Parent_Name { get; set; }
     
        
    }

}