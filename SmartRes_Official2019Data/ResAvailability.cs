using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRes_Official2019Data
{
    public class ResAvailability
    {
        [Key]
        public int ResAvsilId { get; set; }
        [ForeignKey("Residence")]
        public int ResId { get; set; }
        public virtual Residence Residence { get; set; }
        [DisplayName("Space Available")]
        public double NumAvailable { get; set; }
        [DisplayName("Checked In")]
        public double CheckedIN { get; set; }
        [DisplayName("Booked Spaces")]
        public double BookedSpaces { get; set; }
        [DisplayName("Capacity")]
        public double Quantity { get; set; }
        public string Gender { get; set; }
        ApplicationDbContext db = new ApplicationDbContext();

        public double getSpace()
        {
            var capa = (from r in db.Residences
                            where ResId == r.ResId
                            select r.Capacity).FirstOrDefault();
            return capa;
        }

        public double getSpace11()
        {
            var capa = (from r in db.Residences
                        where ResId == r.ResId
                        select r.Capacity).FirstOrDefault();
            return capa;
        }

        public double getCapacity()
        {
            var cap = (from r in db.Residences
                       where ResId == r.ResId
                       select r.Capacity).FirstOrDefault();
            return cap;
        }
    }
}