using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models
{
    [Table("TIMETABLEDETAILS")]
    public class TimetableDetail
    {
        [Key]
        public int DETAILID { get; set; }
        public int TIMETABLEID { get; set; }
        public int CLASSID { get; set; }
        public int ROOMID { get; set; }
        public DateTime STARTTIME { get; set; }
        public DateTime ENDTIME { get; set; }
        public DateTime DAY { get; set; }

        [ForeignKey("TIMETABLEID")]
        public virtual Timetable Timetable { get; set; }

        [ForeignKey("CLASSID")]
        public virtual Class Class { get; set; }

        [ForeignKey("ROOMID")]
        public virtual Room Room { get; set; }
    }
}
