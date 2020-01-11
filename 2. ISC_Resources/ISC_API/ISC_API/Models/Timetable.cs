using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models
{
    [Table("TIMETABLES")]
    public class Timetable
    {
        [Key]
        public int TIMETABLEID { get; set; }
        public DateTime STARTDATE { get; set; }
        public DateTime ENDDATE { get; set; }
        public int WEEK { get; set; }
    }
}
