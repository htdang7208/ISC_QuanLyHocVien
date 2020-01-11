using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ISC_API.Models
{
    [Table("WORKTRACKS")]
    public class Worktrack
    {
        [Key]
        public int WORKTRACKID { get; set; }
        public int COMPANYID { get; set; }
        public int STUDENTID { get; set; }
        public DateTime STARTDATE { get; set; }
        public Nullable<System.DateTime> CONTRACTDATE { get; set; }
        public byte STATUS { get; set; }
        [MaxLength(1000)]
        public string NOTE { get; set; }

        [ForeignKey("COMPANYID")]
        public virtual Company Company { get; set; }

        [ForeignKey("STUDENTID")]
        public virtual Student Student { get; set; }
    }
}
