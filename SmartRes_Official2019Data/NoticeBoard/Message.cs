using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRes_Official2019Data
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Subject { get; set; }
         [Required]
        public string MessageToPost { get; set; }
        public string From { get; set; }
        [DataType(DataType.Date)]
        public DateTime DatePosted { get; set; }
        public string Residence { get; set; }
        public string StudentEmail { get; set; }

        public string To { get; set; }




    }
}
