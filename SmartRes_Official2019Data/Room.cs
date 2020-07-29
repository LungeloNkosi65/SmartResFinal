using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRes_Official2019Data
{
    public class Room
    {
        [Key]
        [DisplayName("Room ID"),Required]
        public int RoomId { get; set; }
        [ForeignKey("Residence")]
        [DisplayName("Res Name"), Required]
        public int ResId { get; set; }
        public virtual Residence Residence { get; set; }
        [DisplayName("Room Type"), Required]
        [ForeignKey("RoomType")]
        public int RtypeId { get; set; }
        public virtual RoomType RoomType { get; set; }
        [DisplayName("Room Number")]
        public string RoomNumber { get; set; }
        public int Floor { get; set; }
        public string Status { get; set; }
        public int Space { get; set; }
        ApplicationDbContext db = new ApplicationDbContext();

        public int getSpace()
        {
            var sp = (from Rt in db.RoomTypes
                      where RtypeId == Rt.RtypeId
                      select Rt.NumbOcupants).FirstOrDefault();
            return sp;
        }

     


    }
}