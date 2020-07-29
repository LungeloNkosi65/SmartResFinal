using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRes_Official2019Data
{
    public partial class Notification
    {
        [Key]
        [DisplayName("Ref#")]
        public string NotID { get; set; }

        [DisplayName("Subject")]
        public string NotType { get; set; }

        [DisplayName("Message")]
        public string message { get; set; }

        [DisplayName("Ref")]
        public int reference { get; set; }

        [DisplayName("Seen?")]
        public bool seen { get; set; }

        [DisplayName("Date")]
        public DateTime NotDate { get; set; }

        [DisplayName("Date Seen")]
        public Nullable<DateTime> DateSeen { get; set; }

        public string Url { get; set; }
        [DisplayName("Reciever")]
        public string Reciever { get; set; }

    }
}
